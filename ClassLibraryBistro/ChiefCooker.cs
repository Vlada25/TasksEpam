﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryBistro
{
    public class ChiefCooker
    {
        public struct Product
        {
            public string Name;
            public double TotalPrice;
            public Product(string name, double price)
            {
                Name = name;
                TotalPrice = price;
            }
        }
        public enum CookOperations
        {
            Add,
            Cut,
            Grate,
            Mix,
            Fry,
            Boil,
            Bake,
            Squeeze
        }
        public enum KitchenDevices
        {
            Pan,
            Saucepan,
            Kettle,
            CoffeeMachine,
            Grill
        }
        const int PAN_CAPACITY = 2,
            GRILL_CAPACITY = 3,
            COUNT_OF_DISHES_IN_OVEN = 2;
        static bool _alreadyExist = false;
        List<Product> products = new List<Product>();
        List<Recipe> recipes = new List<Recipe>();
        public Recipe currentRecipe;
        ClientOrder clientOrder = null;
        int counterOfDishes;

        double freeSpaceInPan, 
            freeSpaceInGrill;
        public ChiefCooker()
        {
            if (_alreadyExist)
            {
                throw new Exception("There can only be one chief-cooker");
            }
            _alreadyExist = true;
        }
        public void CompleteTheOrder(string clientNumber)
        {
            bool isNumberExist = false;
            foreach (ClientOrder order in Manager.ClientOrdersList)
            {
                if (clientNumber.Equals(order.ClientNumber))
                {
                    isNumberExist = true;
                    order.OrderInProgress = true;
                    clientOrder = order;
                    counterOfDishes = 0;
                    break;
                }
            }
            if (!isNumberExist)
            {
                throw new Exception("This order is not exist");
            }
        }
        public void AddProducts(params Product[] products)
        {
            this.products.AddRange(products);
        }

        public void CookTheDish(string name, Manager.Menu type, int countOfPortions)
        {
            if (clientOrder == null)
            {
                throw new Exception("No orders");
            }

            Recipe currentDish = DefineCurrentDish(name, type);

            CheckDishInOrder(name, countOfPortions);
            CheckExistenceOfAllProducts(currentDish);

            freeSpaceInPan = PAN_CAPACITY;
            freeSpaceInGrill = GRILL_CAPACITY;

            foreach (Recipe.KitchenDirections direction in currentDish.Directions)
            {
                switch (direction.CookOperation)
                {
                    case CookOperations.Fry:
                        double commonWeight = 0;
                        foreach (string nameOfIngredient in direction.NamesOfIngredients)
                        {
                            foreach (Recipe.Ingredient ingredient in currentDish.Ingredients)
                            {
                                if (nameOfIngredient.Equals(ingredient.Name))
                                {
                                    commonWeight += ingredient.Weight;
                                }
                            }
                        }
                        switch (direction.Device)
                        {
                            case KitchenDevices.Pan:
                                currentRecipe.PriceOfDish += 0.1 * direction.Minutes;
                                freeSpaceInPan -= commonWeight * countOfPortions;
                                if (freeSpaceInPan < 0)
                                {
                                    throw new Exception($"Too many portions of {currentDish.Name}");
                                }
                                break;
                            case KitchenDevices.Grill:
                                currentRecipe.PriceOfDish += 0.2 * direction.Minutes;
                                freeSpaceInGrill -= commonWeight * countOfPortions;
                                if (freeSpaceInGrill < 0)
                                {
                                    throw new Exception($"Too many portions of {currentDish.Name}");
                                }
                                break;
                        }
                        break;
                    case CookOperations.Cut:
                        if (direction.NamesOfIngredients.Count > 5)
                        {
                            throw new Exception("You can't cut more than 5 ingredients at the same time");
                        }
                        break;
                    case CookOperations.Bake:
                        currentRecipe.PriceOfDish += 0.1 * direction.Minutes;
                        if (countOfPortions > COUNT_OF_DISHES_IN_OVEN)
                        {
                            throw new Exception("You can't bake more than 2 dishes at the same time");
                        }
                        break;
                    case CookOperations.Boil:
                        switch (direction.Device)
                        {
                            case KitchenDevices.Saucepan:
                                currentRecipe.PriceOfDish += 0.1 * direction.Minutes;
                                break;
                            case KitchenDevices.Kettle:
                                currentRecipe.PriceOfDish += 0.05 * direction.Minutes;
                                break;
                            case KitchenDevices.CoffeeMachine:
                                currentRecipe.PriceOfDish += 0.3 * direction.Minutes;
                                break;
                        }
                        break;
                }
            }

            foreach (Dish dish in clientOrder.Dishes)
            {
                if (name.Equals(dish.Name))
                {
                    dish.NeedPortions -= countOfPortions;
                    if (dish.NeedPortions == 0)
                    {
                        dish.IsDishDone = true;
                        counterOfDishes++;
                    }
                    break;
                }
            }

            clientOrder.FinalBill += currentDish.PriceOfDish * countOfPortions;
            clientOrder.SpentMinutes += currentDish.SpentMinutes;

            if (counterOfDishes == clientOrder.Dishes.Count)
            {
                clientOrder.IsDone = true;
            }
        }
        private Recipe DefineCurrentDish(string name, Manager.Menu type)
        {
            Recipe selectedRecipe = new Recipe();
            bool isRecipeExist = false;
            foreach (Recipe recipe in recipes)
            {
                if (recipe.Name == name && recipe.IsRecipeCompleted &&
                    recipe.DishType == type)
                {
                    selectedRecipe = recipe;
                    isRecipeExist = true;
                    break;
                }
            }
            if (!isRecipeExist)
            {
                throw new Exception("There is no such recipe, you can add it");
            }
            
            return selectedRecipe;
        }
        private void CheckDishInOrder(string name, int countOfPortions)
        {
            bool isDishExist = false;
            foreach (Dish dish in clientOrder.Dishes)
            {
                if (name.Equals(dish.Name))
                {
                    if (dish.NeedPortions < countOfPortions)
                    {
                        throw new Exception("So many portions were not ordered");
                    }
                        isDishExist = true;
                    break;
                }
            }
            if (!isDishExist)
            {
                throw new Exception("There is no such dish in order");
            }
        }
        private void CheckExistenceOfAllProducts(Recipe currentDish)
        {
            foreach (Recipe.Ingredient ingredient in currentDish.Ingredients)
            {
                bool isProductExist = false;
                foreach (Product product in products)
                {
                    if (product.Name.Equals(ingredient.Name))
                    {
                        isProductExist = true;
                        break;
                    }
                }
                if (!isProductExist)
                {
                    throw new Exception("Not all the products you need");
                }
            }
        }
        private double CountPriceOfIngredients(List<Recipe.Ingredient> ingredients)
        {
            double price = 0;
            foreach (Recipe.Ingredient ingredient in ingredients)
            {
                foreach (Product product in products)
                {
                    if (product.Name.Equals(ingredient.Name))
                    {
                        price += product.TotalPrice * ingredient.Weight;
                        break;
                    }
                }
            }
            return price;
        }


        // Functions to recipe creation
        public void CreateRecipe(Recipe recipe)
        {
            if (recipes.Count != 0)
            {
                if (!currentRecipe.IsRecipeCompleted)
                {
                    throw new Exception("The previous recipe is incomplete");
                }
            }
            recipes.Add(recipe);
            currentRecipe = recipe;
        }
        public void IdentifyIngredients(params Recipe.Ingredient[] ingredients)
        {
            currentRecipe.Ingredients.AddRange(ingredients);
            currentRecipe.PriceOfDish = CountPriceOfIngredients(currentRecipe.Ingredients);

            currentRecipe.WrittenRecipe += "\nNecessary ingredients: ";
            foreach (Recipe.Ingredient product in ingredients)
            {
                currentRecipe.WrittenRecipe += $"\n\t{product.Name} - {product.Weight} kg; ";
            }
        }
        public void AddDirection(params string[] ingredientNames)
        {
            currentRecipe.CountOfOperations++;
            currentRecipe.SpentMinutes += 1;

            Recipe.KitchenDirections direction = new Recipe.KitchenDirections();
            direction.NamesOfIngredients = new List<string>();
            direction.CookOperation = CookOperations.Add;
            direction.NamesOfIngredients.AddRange(ingredientNames);
            currentRecipe.Directions.Add(direction);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Add";
            foreach (string ingredientName in ingredientNames)
            {
                currentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void MixDirection(params string[] ingredientNames)
        {
            currentRecipe.CountOfOperations++;
            currentRecipe.SpentMinutes += 2;

            Recipe.KitchenDirections direction = new Recipe.KitchenDirections();
            direction.NamesOfIngredients = new List<string>();
            direction.CookOperation = CookOperations.Mix;
            direction.NamesOfIngredients.AddRange(ingredientNames);
            currentRecipe.Directions.Add(direction);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Mix";
            foreach (string ingredientName in ingredientNames)
            {
                currentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void CutDirection(int startSize, int endSize, params string[] ingredientNames)
        {
            currentRecipe.CountOfOperations++;
            currentRecipe.SpentMinutes += 2;

            Recipe.KitchenDirections direction = new Recipe.KitchenDirections();
            direction.NamesOfIngredients = new List<string>();
            direction.CookOperation = CookOperations.Cut;
            direction.NamesOfIngredients.AddRange(ingredientNames);
            currentRecipe.Directions.Add(direction);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Cut into {startSize}-{endSize} mm pieces:";
            foreach (string ingredientName in ingredientNames)
            {
                currentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void GrateDirection(string ingredientName)
        {
            currentRecipe.CountOfOperations++;
            currentRecipe.SpentMinutes += 2;

            Recipe.KitchenDirections direction = new Recipe.KitchenDirections();
            direction.NamesOfIngredients = new List<string>();
            direction.CookOperation = CookOperations.Grate;
            direction.NamesOfIngredients.Add(ingredientName);
            currentRecipe.Directions.Add(direction);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Grate {ingredientName}";
        }
        public void SqueezeDirection(string ingredientName)
        {
            currentRecipe.CountOfOperations++;
            currentRecipe.SpentMinutes += 2;

            Recipe.KitchenDirections direction = new Recipe.KitchenDirections();
            direction.NamesOfIngredients = new List<string>();
            direction.CookOperation = CookOperations.Squeeze;
            direction.NamesOfIngredients.Add(ingredientName);
            currentRecipe.Directions.Add(direction);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Squeeze juice from {ingredientName}";
        }
        public void MixAllDirection()
        {
            currentRecipe.CountOfOperations++;
            currentRecipe.SpentMinutes += 1;

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) Mix all ingredients";
        }
        public void FryDirection(int minutes, KitchenDevices device, params string[] ingredientNames)
        {
            currentRecipe.CountOfOperations++;
            currentRecipe.SpentMinutes += minutes;

            Recipe.KitchenDirections direction = new Recipe.KitchenDirections();
            direction.NamesOfIngredients = new List<string>();
            direction.CookOperation = CookOperations.Fry;
            direction.Device = device;
            direction.Minutes = minutes;
            direction.NamesOfIngredients.AddRange(ingredientNames);
            currentRecipe.Directions.Add(direction);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Fry {minutes} min";
            foreach (string ingredientName in ingredientNames)
            {
                currentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void BoilDirection(int minutes, KitchenDevices device, params string[] ingredientNames)
        {
            currentRecipe.CountOfOperations++;
            currentRecipe.SpentMinutes += minutes;

            Recipe.KitchenDirections direction = new Recipe.KitchenDirections();
            direction.NamesOfIngredients = new List<string>();
            direction.CookOperation = CookOperations.Boil;
            direction.Device = device;
            direction.Minutes = minutes;
            direction.NamesOfIngredients.AddRange(ingredientNames);
            currentRecipe.Directions.Add(direction);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Boil {minutes} min";
            foreach (string ingredientName in ingredientNames)
            {
                currentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void BakeDirection(int minutes, params string[] ingredientNames)
        {
            currentRecipe.CountOfOperations++;
            currentRecipe.SpentMinutes += minutes;

            Recipe.KitchenDirections direction = new Recipe.KitchenDirections();
            direction.NamesOfIngredients = new List<string>();
            direction.CookOperation = CookOperations.Bake;
            direction.NamesOfIngredients.AddRange(ingredientNames);
            currentRecipe.Directions.Add(direction);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Bake {minutes} min";
            foreach (string ingredientName in ingredientNames)
            {
                currentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void BakeDirection(int minutes)
        {
            currentRecipe.CountOfOperations++;
            currentRecipe.SpentMinutes += minutes;

            Recipe.KitchenDirections direction = new Recipe.KitchenDirections();
            direction.NamesOfIngredients = new List<string>();
            direction.CookOperation = CookOperations.Bake;
            direction.NamesOfIngredients.Add("All");
            currentRecipe.Directions.Add(direction);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Bake all {minutes} min";
        }
        public void CompleteRecipeCreation()
        {
            currentRecipe.WrittenRecipe += $"\nTime: {currentRecipe.SpentMinutes} minutes";
            currentRecipe.WrittenRecipe += $"\nPrice of ingredients: {Math.Round(currentRecipe.PriceOfDish, 2)}$";

            currentRecipe.IsRecipeCompleted = true;
        }

        // Functions to view necessary information
        public string ViewAllIngredients()
        {
            string result = $"\nAll products:";
            foreach (Product product in products)
            {
                result += $"\n{product.Name} - {product.TotalPrice}$";
            }
            return result;
        }
        public string ViewCurrentOrder()
        {
            string result = "\nCurrent order #";
            result += clientOrder?.ClientNumber ?? "???\nNo orders in progress";
            if (clientOrder != null)
            {
                foreach (Dish dish in clientOrder.Dishes)
                {
                    result += "\n" + dish.ToString();
                }
            }
            return result;
        }
        public string ViewRecipe(string name)
        {
            foreach (Recipe recipe in recipes)
            {
                if (recipe.Name == name)
                {
                    return recipe.ToString();
                }
            }
            return "Recipe not found";
        }
        // пока заказ выполняется, его нельзя допонить
    }
}
