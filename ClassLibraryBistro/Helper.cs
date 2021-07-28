using System;
using System.Collections.Generic;
using System.Text;
using static ClassLibraryBistro.ChiefCooker;

namespace ClassLibraryBistro
{
    public static class Helper
    {
        public static string SetTime(string time)
        {
            // Bistro is open from 9:00 to 23:00
            const int OPEN_TIME_HOURS = 9,
            CLOSE_TIME_HOURS = 22,
            MAX_VALUE_OF_MINUTES = 59;

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
            else if (hours < OPEN_TIME_HOURS || hours > CLOSE_TIME_HOURS || minutes < 0 || minutes > MAX_VALUE_OF_MINUTES)
            {
                throw ex;
            }
            return time;
        }
        public static bool IsTimeInRange(string time, string startTime, string endTime)
        {
            const int HOURS_POS = 0,
                MINUTES_POS = 3;

            startTime = SetTime(startTime);
            endTime = SetTime(endTime);

            int timeHours = Convert.ToInt32(time.Substring(HOURS_POS, 2));
            int timeMins = Convert.ToInt32(time.Substring(MINUTES_POS, 2));
            int startTimeHours = Convert.ToInt32(startTime.Substring(HOURS_POS, 2));
            int endTimeHours = Convert.ToInt32(endTime.Substring(HOURS_POS, 2));
            int startTimeMins = Convert.ToInt32(startTime.Substring(MINUTES_POS, 2));
            int endTimeMins = Convert.ToInt32(endTime.Substring(MINUTES_POS, 2));

            if (startTimeHours <= timeHours && endTimeHours >= timeHours &&
                startTimeMins <= timeMins && endTimeMins >= timeMins)
            {
                return true;
            }
            return false;
        }
        public static double CountPriceOfIngredients(List<Recipe.Ingredient> ingredients, List<Product> products)
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
        public static void CheckExistenceOfAllProducts(Recipe currentDish, List<Product> products)
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
                if (recipe.Name == name && recipe.IsCompleted &&
                    recipe.Type == type)
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
        public static void CountNumOfUsesForProducts(Recipe recipe, int countOfPortions, List<Product> products)
        {
            foreach (Recipe.Ingredient ingredient in recipe.Ingredients)
            {
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].Name.Equals(ingredient.Name))
                    {
                        Product tmp = products[i];
                        tmp.NumberOfUses += countOfPortions;
                        products[i] = tmp;
                        break;
                    }
                }
            }
        }
        public static double SetCommonWeight(Recipe.KitchenActions action, Recipe currentDish)
        {
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
            return commonWeight;
        }
        public static double TrySetFreeSpaceInDevice(double freeSpaceInDevice, double weight, int countOfPortions, string name)
        {
            freeSpaceInDevice -= weight * countOfPortions;
            if (freeSpaceInDevice < 0)
            {
                throw new Exception($"Too many portions of {name}");
            }
            return freeSpaceInDevice;
        }
        public static ProcessingProcedure FindLongestProcessingProcedure(ProcessingProcedure _longestProcedure, Recipe.KitchenActions action)
        {
            if (_longestProcedure.Minutes < action.Minutes)
            {
                _longestProcedure.Operation = action.CookOperation;
                _longestProcedure.Device = action.Device;
                _longestProcedure.Minutes = action.Minutes;
            }
            return _longestProcedure;
        }
    }
}
