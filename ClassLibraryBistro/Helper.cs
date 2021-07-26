using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryBistro
{
    public static class Helper
    {
        public static string SetTime(string time)
        {
            // Bistro is open from 9:00 to 23:00
            Exception ex = new Exception("Invalid value of time");
            if (time.Length != 5)
            {
                throw ex;
            }
            else if (!Int32.TryParse(Convert.ToString(time[0]) + Convert.ToString(time[1]), out int hours)
                    || !Int32.TryParse(Convert.ToString(time[3]) + Convert.ToString(time[4]), out int minutes))
            {
                throw ex;
            }
            else if (hours < 9 || hours > 22 || minutes < 0 || minutes > 59)
            {
                throw ex;
            }
            return time;
        }
        public static bool IsTimeInRange(string time, string startTime, string endTime)
        {
            startTime = SetTime(startTime);
            endTime = SetTime(endTime);

            int timeHours = Convert.ToInt32(time.Substring(0, 2));
            int timeMins = Convert.ToInt32(time.Substring(3, 2));
            int startTimeHours = Convert.ToInt32(startTime.Substring(0, 2));
            int endTimeHours = Convert.ToInt32(endTime.Substring(0, 2));
            int startTimeMins = Convert.ToInt32(startTime.Substring(3, 2));
            int endTimeMins = Convert.ToInt32(endTime.Substring(3, 2));

            if (startTimeHours <= timeHours && endTimeHours >= timeHours &&
                startTimeMins <= timeMins && endTimeMins >= timeMins)
            {
                return true;
            }
            return false;
        }
        public static double CountPriceOfIngredients(List<Recipe.Ingredient> ingredients, List<ChiefCooker.Product> products)
        {
            double price = 0;
            foreach (Recipe.Ingredient ingredient in ingredients)
            {
                foreach (ChiefCooker.Product product in products)
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
        public static void CheckExistenceOfAllProducts(Recipe currentDish, List<ChiefCooker.Product> products)
        {
            foreach (Recipe.Ingredient ingredient in currentDish.Ingredients)
            {
                bool isProductExist = false;
                foreach (ChiefCooker.Product product in products)
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
        public static void CheckDishInOrder(string name, int countOfPortions, ClientOrder clientOrder)
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
        public static Recipe DefineCurrentDish(string name, Manager.Menu type, List<Recipe> recipes)
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
    }
}
