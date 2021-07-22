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
        public enum KitchenUtensils
        {
            Pan,
            Saucepan,
            Oven,
            Grill,
            Bowl
        }
        const int PAN_CAPACITY = 2;
        private static bool _alreadyExist = false;
        List<Product> products = new List<Product>();
        List<Recipe> recipes = new List<Recipe>();
        public Recipe currentRecipe;
        ClientOrder clientOrder;
        double costOfIngredients;
        double freeSpaceInPan;
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
                    costOfIngredients = 0;
                    freeSpaceInPan = PAN_CAPACITY;
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

        public void CookTheDish(string name, Manager.Menu type)
        {
            Recipe currentDish = new Recipe();
            
            bool isRecipeExist = false;
            foreach (Recipe recipe in recipes)
            {
                if (recipe.Name == name && recipe.IsRecipeCompleted &&
                    recipe.DishType == type)
                {
                    currentDish = recipe;
                    isRecipeExist = true;
                    break;
                }
            }
            if (!isRecipeExist)
            {
                throw new Exception("There is no such recipe, you can add it");
            }

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

            Console.WriteLine(currentDish.ToString());
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
            Recipe.KitchenDirections direction = new Recipe.KitchenDirections();
            direction.NamesOfIngredients = new List<string>();
            direction.CookOperations = CookOperations.Cut;
            direction.NamesOfIngredients.Add(ingredientName);
            currentRecipe.Directions.Add(direction);
            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Cut {ingredientName} into {startSize}-{endSize} mm pieces";
        }
        public void GrateDirection(string ingredientName)
        {
            currentRecipe.CountOfOperations++;
            Recipe.KitchenDirections direction = new Recipe.KitchenDirections();
            direction.NamesOfIngredients = new List<string>();
            direction.CookOperations = CookOperations.Grate;
            direction.NamesOfIngredients.Add(ingredientName);
            currentRecipe.Directions.Add(direction);
            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) " +
                $"Grate {ingredientName}";
        }
        public void MixAllDirection()
        {
            currentRecipe.CountOfOperations++;
            currentRecipe.WrittenRecipe += $"\n{currentRecipe.CountOfOperations}) Mix all ingredients";
        }
        public void FryDirection(int minutes, params string[] ingredientNames)
        {
            currentRecipe.CountOfOperations++;
            Recipe.KitchenDirections direction = new Recipe.KitchenDirections();
            direction.NamesOfIngredients = new List<string>();
            direction.CookOperations = CookOperations.Fry;
            direction.NamesOfIngredients.AddRange(ingredientNames);
            direction.SpentMinutes = minutes;
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
