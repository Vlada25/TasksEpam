using System;
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
            public StorageConditions Conditions;
            public Product(string name, double price, StorageConditions conditions)
            {
                Name = name;
                TotalPrice = price;
                Conditions = conditions;
            }
        }
        public struct StorageConditions
        {
            public int StartTemperature;
            public int EndTemperature;
            public StorageConditions(int startTemperature, int endTemperature)
            {
                StartTemperature = startTemperature;
                EndTemperature = endTemperature;
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
            SAUCEPAN_CAPACITY = 5,
            GRILL_CAPACITY = 3,
            COUNT_OF_DISHES_IN_OVEN = 2;
        static bool _alreadyExist = false;
        List<Product> products = new List<Product>();
        List<Recipe> recipes = new List<Recipe>();
        public Recipe currentRecipe;
        ClientOrder clientOrder = null;
        int counterOfDishes;

        double freeSpaceInPan,
            freeSpaceInSaucepan,
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

            Recipe currentDish = Helper.DefineCurrentDish(name, type, recipes);

            Helper.CheckDishInOrder(name, countOfPortions, clientOrder);
            Helper.CheckExistenceOfAllProducts(currentDish, products);

            freeSpaceInPan = PAN_CAPACITY;
            freeSpaceInSaucepan = SAUCEPAN_CAPACITY;
            freeSpaceInGrill = GRILL_CAPACITY;

            foreach (Recipe.KitchenActions action in currentDish.Actions)
            {
                switch (action.CookOperation)
                {
                    case CookOperations.Fry:
                        double commonWeight = 0;
                        foreach (string nameOfIngredient in action.NamesOfIngredients)
                        {
                            foreach (Recipe.Ingredient ingredient in currentDish.Ingredients)
                            {
                                if (nameOfIngredient.Equals(ingredient.Name))
                                {
                                    commonWeight += ingredient.Weight;
                                }
                            }
                        }
                        switch (action.Device)
                        {
                            case KitchenDevices.Pan:
                                currentRecipe.PriceOfDish += 0.1 * action.Minutes;
                                freeSpaceInPan -= commonWeight * countOfPortions;
                                if (freeSpaceInPan < 0)
                                {
                                    throw new Exception($"Too many portions of {currentDish.Name}");
                                }
                                break;
                            case KitchenDevices.Grill:
                                currentRecipe.PriceOfDish += 0.2 * action.Minutes;
                                freeSpaceInGrill -= commonWeight * countOfPortions;
                                if (freeSpaceInGrill < 0)
                                {
                                    throw new Exception($"Too many portions of {currentDish.Name}");
                                }
                                break;
                        }
                        break;
                    case CookOperations.Cut:
                        if (action.NamesOfIngredients.Count > 5)
                        {
                            throw new Exception("You can't cut more than 5 ingredients at the same time");
                        }
                        break;
                    case CookOperations.Bake:
                        currentRecipe.PriceOfDish += 0.1 * action.Minutes;
                        if (countOfPortions > COUNT_OF_DISHES_IN_OVEN)
                        {
                            throw new Exception("You can't bake more than 2 dishes at the same time");
                        }
                        break;
                    case CookOperations.Boil:
                        commonWeight = 0;
                        foreach (string nameOfIngredient in action.NamesOfIngredients)
                        {
                            foreach (Recipe.Ingredient ingredient in currentDish.Ingredients)
                            {
                                if (nameOfIngredient.Equals(ingredient.Name))
                                {
                                    commonWeight += ingredient.Weight;
                                }
                            }
                        }
                        freeSpaceInSaucepan -= commonWeight * countOfPortions;

                        if (freeSpaceInSaucepan < 0)
                        {
                            throw new Exception($"Too many portions of {currentDish.Name}");
                        }

                        switch (action.Device)
                        {
                            case KitchenDevices.Saucepan:
                                currentRecipe.PriceOfDish += 0.1 * action.Minutes;
                                break;
                            case KitchenDevices.Kettle:
                                currentRecipe.PriceOfDish += 0.05 * action.Minutes;
                                break;
                            case KitchenDevices.CoffeeMachine:
                                currentRecipe.PriceOfDish += 0.3 * action.Minutes;
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
            currentRecipe.PriceOfDish = Helper.CountPriceOfIngredients(currentRecipe.Ingredients, products);

            currentRecipe.WrittenRecipe += "\nNecessary ingredients: ";
            foreach (Recipe.Ingredient product in ingredients)
            {
                currentRecipe.WrittenRecipe += $"\n\t{product.Name} - {product.Weight} kg; ";
            }
        }
        private void TakeAnAction(Recipe.KitchenActions action, CookOperations operation, int spentMinutes, string[] ingredientNames)
        {
            currentRecipe.CountOfOperations++;
            currentRecipe.SpentMinutes += spentMinutes;

            action.NamesOfIngredients = new List<string>();
            action.CookOperation = operation;
            action.NamesOfIngredients.AddRange(ingredientNames);
            currentRecipe.Actions.Add(action);
        }
        public void Add(params string[] ingredientNames)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            TakeAnAction(action, CookOperations.Add, 1, ingredientNames);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Add";
            foreach (string ingredientName in ingredientNames)
            {
                currentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void Mix(params string[] ingredientNames)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            TakeAnAction(action, CookOperations.Mix, 2, ingredientNames);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Mix";
            foreach (string ingredientName in ingredientNames)
            {
                currentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void Cut(int startSize, int endSize, params string[] ingredientNames)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            TakeAnAction(action, CookOperations.Cut, 2, ingredientNames);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Cut into {startSize}-{endSize} mm pieces:";
            foreach (string ingredientName in ingredientNames)
            {
                currentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void Grate(params string[] ingredientNames)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            TakeAnAction(action, CookOperations.Grate, 2, ingredientNames);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Grate";
            foreach (string ingredientName in ingredientNames)
            {
                currentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void Squeze(params string[] ingredientNames)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            TakeAnAction(action, CookOperations.Squeeze, 2, ingredientNames);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Squeeze juice from";
            foreach (string ingredientName in ingredientNames)
            {
                currentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void MixAll()
        {
            currentRecipe.CountOfOperations++;
            currentRecipe.SpentMinutes += 1;

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) Mix all ingredients";
        }
        public void Fry(int minutes, KitchenDevices device, params string[] ingredientNames)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            action.Device = device;
            action.Minutes = minutes;

            TakeAnAction(action, CookOperations.Fry, minutes, ingredientNames);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Fry {minutes} min";
            foreach (string ingredientName in ingredientNames)
            {
                currentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void Boil(int minutes, KitchenDevices device, params string[] ingredientNames)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            action.Device = device;
            action.Minutes = minutes;

            TakeAnAction(action, CookOperations.Boil, minutes, ingredientNames);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Boil {minutes} min";
            foreach (string ingredientName in ingredientNames)
            {
                currentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void Bake(int minutes, params string[] ingredientNames)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            TakeAnAction(action, CookOperations.Bake, minutes, ingredientNames);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Bake {minutes} min";
            foreach (string ingredientName in ingredientNames)
            {
                currentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void Bake(int minutes)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            TakeAnAction(action, CookOperations.Bake, minutes, new string[] { "All"});

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
        public string ViewAllProductionCapacity()
        {
            string result = $"\nAll production capacity:";
            result += $"\nPan - {PAN_CAPACITY} kg";
            result += $"\nSaucepan - {SAUCEPAN_CAPACITY} kg";
            result += $"\nGrill - {GRILL_CAPACITY} kg";
            result += $"\nCount of dishes in oven - {COUNT_OF_DISHES_IN_OVEN}";
            return result;
        }
        public string ViewAllIngredients()
        {
            string result = $"\nAll products:";
            foreach (Product product in products)
            {
                result += $"\n{product.Name} - {product.TotalPrice}$";
            }
            return result;
        }
        public string FindIngredientsByStorageConditions(StorageConditions storageConditions)
        {
            string result = "";
            foreach (Product product in products)
            {
                if (product.Conditions.StartTemperature >= storageConditions.StartTemperature &&
                    product.Conditions.EndTemperature <= storageConditions.EndTemperature)
                {
                    result += $"\n{product.Name} - {product.TotalPrice}$";
                    result += $"({product.Conditions.StartTemperature}-{product.Conditions.EndTemperature} deg)";
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
    }
}
