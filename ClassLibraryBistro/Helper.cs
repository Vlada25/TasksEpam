using System;
using System.Collections.Generic;
using System.Text;
using static ClassLibraryBistro.ChiefCooker;

namespace ClassLibraryBistro
{
    public static class Helper
    {
        /// <summary>
        /// Setting the time. If transmitted time value is incorrect, throws exeption
        /// </summary>
        /// <param name="time"> String with value of time </param>
        /// <returns> Time </returns>
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

        /// <summary>
        /// Checking time belongs to a certain period
        /// </summary>
        /// <param name="time"> Current time </param>
        /// <param name="startTime"> Start of time span </param>
        /// <param name="endTime"> End of time span </param>
        /// <returns> Is selected time in range </returns>
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

            if (startTimeHours <= timeHours && endTimeHours >= timeHours)
            {
                if (startTimeHours == timeHours && startTimeMins <= timeMins)
                {
                    return true;
                }
                else if (endTimeHours == timeHours && endTimeMins >= timeMins)
                {
                    return true;
                }
                else if (endTimeHours != timeHours && startTimeHours != timeHours)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Counting price of all necessary ingredients for a specific dish
        /// </summary>
        /// <param name="ingredients"> List of ingredients </param>
        /// <param name="products"> List of products (to check price) </param>
        /// <returns> Price of all ingredients for the dish </returns>
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

        /// <summary>
        /// Verification of the existence of all necessary products for a specific dish
        /// </summary>
        /// <param name="currentDish"> Current dish </param>
        /// <param name="products"> List of products </param>
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

        /// <summary>
        /// Verification of the existence of selected dish in order
        /// </summary>
        /// <param name="name"> Name of dish </param>
        /// <param name="countOfPortions"> Count of portions </param>
        /// <param name="clientOrder"> Current order </param>
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

        /// <summary>
        /// Definition of the current dish (currently running recipe)
        /// </summary>
        /// <param name="name"> Name of dish </param>
        /// <param name="type"> Type of dish </param>
        /// <param name="recipes"> List of recipes </param>
        /// <returns> Current recipe </returns>
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

        /// <summary>
        /// Count how many times product was used
        /// </summary>
        /// <param name="recipe"> Recipe(dish) </param>
        /// <param name="countOfPortions"> Cout of portions </param>
        /// <param name="products"> List of products </param>
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

        /// <summary>
        /// Setting common weight of all ingredients in one action
        /// </summary>
        /// <param name="action"> Selected action </param>
        /// <param name="currentDish"> Current dish </param>
        /// <returns> Common weight of products </returns>
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

        /// <summary>
        /// Trying to set new value of free space in device
        /// </summary>
        /// <param name="freeSpaceInDevice"> Current value of free space in device </param>
        /// <param name="weight"> Weight of new products </param>
        /// <param name="countOfPortions"> Count of portions </param>
        /// <param name="name"> Name of the dish</param>
        /// <returns> Free space in device </returns>
        public static double TrySetFreeSpaceInDevice(double freeSpaceInDevice, double weight, int countOfPortions, string name)
        {
            freeSpaceInDevice -= weight * countOfPortions;
            if (freeSpaceInDevice < 0)
            {
                throw new Exception($"Too many portions of {name}");
            }
            return freeSpaceInDevice;
        }

        /// <summary>
        /// Search for the longest processing procedure
        /// </summary>
        /// <param name="longestProcedure"> Current longest procedure </param>
        /// <param name="action"> Action </param>
        /// <returns> The longest processing procedure </returns>
        public static ProcessingProcedure FindLongestProcessingProcedure(ProcessingProcedure longestProcedure, Recipe.KitchenActions action)
        {
            if (longestProcedure.Minutes < action.Minutes)
            {
                longestProcedure.Operation = action.CookOperation;
                longestProcedure.Device = action.Device;
                longestProcedure.Minutes = action.Minutes;
            }
            return longestProcedure;
        }

        /// <summary>
        /// Setting the number of client. If entered string cannot be converted to a number, throws exeption
        /// </summary>
        /// <param name="number"> Number of client </param>
        /// <returns> Number of client </returns>
        public static string SetClientNumber(string number)
        {
            Exception ex = new Exception("Invalid client number");
            if (number.Length != 3)
            {
                throw ex;
            }
            else if (!Int32.TryParse(number, out int _))
            {
                throw ex;
            }
            return number;
        }
    }
}
