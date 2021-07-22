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
            Stew,
            Bake
        }
        public enum KitchenDevices
        {
            Pan,
            Saucepan,
            Oven,
            Grill,
            Bowl
        }
        const int PAN_CAPACITY = 2,
            GRILL_CAPACITY = 3;
        static bool _alreadyExist = false;
        List<Product> products = new List<Product>();
        List<Recipe> recipes = new List<Recipe>();
        public Recipe currentRecipe;
        ClientOrder clientOrder = null;
        //double costOfIngredients;
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
                    clientOrder = order;
                    //costOfIngredients = 0;
                    freeSpaceInPan = PAN_CAPACITY;
                    freeSpaceInGrill = GRILL_CAPACITY;
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
                        freeSpaceInPan -= commonWeight * countOfPortions;
                        if (freeSpaceInPan < 0)
                        {
                            throw new Exception("Too many portions");
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
                    }
                    break;
                }
            }

            clientOrder.FinalBill += CountPriceOfIngredients(currentDish, countOfPortions);
            clientOrder.SpentMinutes += currentDish.SpentMinutes;
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
        private double CountPriceOfIngredients(Recipe currentDish, int countOfPortions)
        {
            double price = 0;
            foreach (Recipe.Ingredient ingredient in currentDish.Ingredients)
            {
                foreach (Product product in products)
                {
                    if (product.Name.Equals(ingredient.Name))
                    {
                        price += product.TotalPrice * ingredient.Weight * countOfPortions;
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
            currentRecipe.WrittenRecipe += "\nNecessary ingredients: ";
            foreach (Recipe.Ingredient product in ingredients)
            {
                currentRecipe.WrittenRecipe += $"\n\t{product.Name} - {product.Weight} kg; ";
            }
        }
        public void CutDirection(int startSize, int endSize, string ingredientName)
        {
            currentRecipe.CountOfOperations++;
            currentRecipe.SpentMinutes += 2;

            Recipe.KitchenDirections direction = new Recipe.KitchenDirections();
            direction.NamesOfIngredients = new List<string>();
            direction.CookOperation = CookOperations.Cut;
            direction.NamesOfIngredients.Add(ingredientName);
            currentRecipe.Directions.Add(direction);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Cut {ingredientName} into {startSize}-{endSize} mm pieces";
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
            direction.NamesOfIngredients.AddRange(ingredientNames);
            currentRecipe.Directions.Add(direction);

            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Fry {minutes} min";
            foreach (string ingredientName in ingredientNames)
            {
                currentRecipe.WrittenRecipe += $" {ingredientName};";
            }
        }
        public void CompleteRecipeCreation()
        {
            currentRecipe.WrittenRecipe += $"\nTime: {currentRecipe.SpentMinutes} minutes";
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
        // пока заказ выполняется, его нельзя допонить
        // когда завершится приготовление блюда, сдать менеджеру на проверку того, готовы ли все блюда из заказа
    }
}
