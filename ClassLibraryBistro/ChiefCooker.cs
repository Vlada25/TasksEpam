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
            public int NumberOfUses;
            public Product(string name, double price, StorageConditions conditions)
            {
                Name = name;
                TotalPrice = price;
                Conditions = conditions;
                NumberOfUses = 0;
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
        public struct ProcessingProcedure
        {
            public CookOperations Operation;
            public KitchenDevices Device;
            public int Minutes;
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
            Grill,
            Oven,
            Knife,
            Spoon,
            Grater,
            Juicer
        }
        public enum NumOfUses
        {
            Max,
            Min
        }

        const int PAN_CAPACITY = 2,
            SAUCEPAN_CAPACITY = 5,
            GRILL_CAPACITY = 3,
            COUNT_OF_DISHES_IN_OVEN = 2,
            MAX_CUT_INGREDIENTS = 5;

        double _freeSpaceInPan,
            _freeSpaceInSaucepan,
            _freeSpaceInGrill;

        static bool _alreadyExist = false;
        public Recipe CurrentRecipe;
        ClientOrder _clientOrder = null;
        int _counterOfDishes;
        ProcessingProcedure _longestProcedure;

        readonly List<Product> _products = new List<Product>();
        readonly List<Recipe> _recipes = new List<Recipe>();
        readonly Dictionary<string, double> pricesForProcessingProcedures = new Dictionary<string, double> 
        {
            { "Pan", 0.1 },
            { "Saucepan", 0.1 },
            { "Grill", 0.2 },
            { "Oven", 0.1 },
            { "Kettle", 0.05 },
            { "CoffeeMachine", 0.3 }
        };

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
                    _clientOrder = order;
                    _counterOfDishes = 0;
                    break;
                }
            }
            if (!isNumberExist)
            {
                throw new Exception("This order is not exist");
            }
        }
        public void AddProducts(params Product[] _products)
        {
            this._products.AddRange(_products);
        }

        public void CookTheDish(string name, Manager.Menu type, int countOfPortions)
        {
            if (_clientOrder == null)
            {
                throw new Exception("No orders");
            }

            Recipe currentDish = Helper.DefineCurrentDish(name, type, _recipes);

            Helper.CheckDishInOrder(name, countOfPortions, _clientOrder);
            Helper.CheckExistenceOfAllProducts(currentDish, _products);

            _freeSpaceInPan = PAN_CAPACITY;
            _freeSpaceInSaucepan = SAUCEPAN_CAPACITY;
            _freeSpaceInGrill = GRILL_CAPACITY;

            foreach (Recipe.KitchenActions action in currentDish.Actions)
            {
                switch (action.CookOperation)
                {
                    case CookOperations.Fry:
                        double commonWeight = Helper.SetCommonWeight(action, currentDish);
                        switch (action.Device)
                        {
                            case KitchenDevices.Pan:
                                CurrentRecipe.PriceOfDish += pricesForProcessingProcedures["Pan"] * action.Minutes;
                                _freeSpaceInPan = Helper.TrySetFreeSpaceInDevice(_freeSpaceInPan, commonWeight, countOfPortions, currentDish.Name);
                                break;
                            case KitchenDevices.Grill:
                                CurrentRecipe.PriceOfDish += pricesForProcessingProcedures["Grill"] * action.Minutes;
                                _freeSpaceInGrill = Helper.TrySetFreeSpaceInDevice(_freeSpaceInGrill, commonWeight, countOfPortions, currentDish.Name);
                                break;
                        }
                        break;
                    case CookOperations.Cut:
                        if (action.NamesOfIngredients.Count > MAX_CUT_INGREDIENTS)
                        {
                            throw new Exception("You can't cut more than 5 ingredients at the same time");
                        }
                        break;
                    case CookOperations.Bake:
                        CurrentRecipe.PriceOfDish += pricesForProcessingProcedures["Oven"] * action.Minutes;
                        if (countOfPortions > COUNT_OF_DISHES_IN_OVEN)
                        {
                            throw new Exception("You can't bake more than 2 dishes at the same time");
                        }
                        break;
                    case CookOperations.Boil:
                        commonWeight = Helper.SetCommonWeight(action, currentDish);
                        _freeSpaceInSaucepan = Helper.TrySetFreeSpaceInDevice(_freeSpaceInSaucepan, commonWeight, countOfPortions, currentDish.Name);

                        switch (action.Device)
                        {
                            case KitchenDevices.Saucepan:
                                CurrentRecipe.PriceOfDish += pricesForProcessingProcedures["Saucepan"] * action.Minutes;
                                break;
                            case KitchenDevices.Kettle:
                                CurrentRecipe.PriceOfDish += pricesForProcessingProcedures["Kettle"] * action.Minutes;
                                break;
                            case KitchenDevices.CoffeeMachine:
                                CurrentRecipe.PriceOfDish += pricesForProcessingProcedures["CoffeeMachine"] * action.Minutes;
                                break;
                        }
                        break;
                }
                _longestProcedure = Helper.FindLongestProcessingProcedure(_longestProcedure, action);
            }

            foreach (Dish dish in _clientOrder.Dishes)
            {
                if (name.Equals(dish.Name))
                {
                    dish.NeedPortions -= countOfPortions;
                    if (dish.NeedPortions == 0)
                    {
                        dish.IsDishDone = true;
                        _counterOfDishes++;
                    }
                    break;
                }
            }

            _clientOrder.FinalBill += currentDish.PriceOfDish * countOfPortions;
            _clientOrder.SpentMinutes += currentDish.SpentMinutes;

            if (_counterOfDishes == _clientOrder.Dishes.Count)
            {
                _clientOrder.IsDone = true;
            }

            Helper.CountNumOfUsesForProducts(currentDish, countOfPortions, _products);
        }

        public void CreateRecipe(Recipe recipe)
        {
            if (_recipes.Count != 0)
            {
                if (!CurrentRecipe.IsCompleted)
                {
                    throw new Exception("The previous recipe is incomplete");
                }
            }
            _recipes.Add(recipe);
            CurrentRecipe = recipe;
        }
        public void IdentifyIngredients(params Recipe.Ingredient[] ingredients)
        {
            CurrentRecipe.Ingredients.AddRange(ingredients);
            CurrentRecipe.PriceOfDish = Helper.CountPriceOfIngredients(CurrentRecipe.Ingredients, _products);

            CurrentRecipe.WrittenRecipe += "\nNecessary ingredients: ";
            foreach (Recipe.Ingredient product in ingredients)
            {
                CurrentRecipe.WrittenRecipe += $"\n\t{product.Name} - {product.Weight} kg; ";
            }
        }
        private void TakeAnAction(Recipe.KitchenActions action, CookOperations operation, int spentMinutes, string[] ingredientNames, KitchenDevices device)
        {
            CurrentRecipe.CountOfOperations++;
            CurrentRecipe.SpentMinutes += spentMinutes;

            action.NamesOfIngredients = new List<string>();
            action.CookOperation = operation;
            action.Device = device;
            action.Minutes = spentMinutes;
            action.NamesOfIngredients.AddRange(ingredientNames);
            CurrentRecipe.Actions.Add(action);
        }
        public void Add(params string[] ingredientNames)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            TakeAnAction(action, CookOperations.Add, 1, ingredientNames, KitchenDevices.Spoon);

            CurrentRecipe.WrittenRecipe += $"\n{CurrentRecipe.CountOfOperations}) " +
                $"Add";
            foreach (string ingredientName in ingredientNames)
            {
                CurrentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void Mix(params string[] ingredientNames)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            TakeAnAction(action, CookOperations.Mix, 2, ingredientNames, KitchenDevices.Spoon);

            CurrentRecipe.WrittenRecipe += $"\n{CurrentRecipe.CountOfOperations}) " +
                $"Mix";
            foreach (string ingredientName in ingredientNames)
            {
                CurrentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void Cut(int startSize, int endSize, params string[] ingredientNames)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            TakeAnAction(action, CookOperations.Cut, 2, ingredientNames, KitchenDevices.Knife);

            CurrentRecipe.WrittenRecipe += $"\n{CurrentRecipe.CountOfOperations}) " +
                $"Cut into {startSize}-{endSize} mm pieces:";
            foreach (string ingredientName in ingredientNames)
            {
                CurrentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void Grate(params string[] ingredientNames)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            TakeAnAction(action, CookOperations.Grate, 2, ingredientNames, KitchenDevices.Grater);

            CurrentRecipe.WrittenRecipe += $"\n{CurrentRecipe.CountOfOperations}) " +
                $"Grate";
            foreach (string ingredientName in ingredientNames)
            {
                CurrentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void Squeze(params string[] ingredientNames)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            TakeAnAction(action, CookOperations.Squeeze, 2, ingredientNames, KitchenDevices.Juicer);

            CurrentRecipe.WrittenRecipe += $"\n{CurrentRecipe.CountOfOperations}) " +
                $"Squeeze juice from";
            foreach (string ingredientName in ingredientNames)
            {
                CurrentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void MixAll()
        {
            CurrentRecipe.CountOfOperations++;
            CurrentRecipe.SpentMinutes += 1;

            CurrentRecipe.WrittenRecipe += $"\n{CurrentRecipe.CountOfOperations}) Mix all ingredients";
        }
        public void Fry(int minutes, KitchenDevices device, params string[] ingredientNames)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            TakeAnAction(action, CookOperations.Fry, minutes, ingredientNames, device);

            CurrentRecipe.WrittenRecipe += $"\n{CurrentRecipe.CountOfOperations}) " +
                $"Fry {minutes} min";
            foreach (string ingredientName in ingredientNames)
            {
                CurrentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void Boil(int minutes, KitchenDevices device, params string[] ingredientNames)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            TakeAnAction(action, CookOperations.Boil, minutes, ingredientNames, device);

            CurrentRecipe.WrittenRecipe += $"\n{CurrentRecipe.CountOfOperations}) " +
                $"Boil {minutes} min";
            foreach (string ingredientName in ingredientNames)
            {
                CurrentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void Bake(int minutes, params string[] ingredientNames)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            TakeAnAction(action, CookOperations.Bake, minutes, ingredientNames, KitchenDevices.Oven);

            CurrentRecipe.WrittenRecipe += $"\n{CurrentRecipe.CountOfOperations}) " +
                $"Bake {minutes} min";
            foreach (string ingredientName in ingredientNames)
            {
                CurrentRecipe.WrittenRecipe += $" {ingredientName},";
            }
        }
        public void Bake(int minutes)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            TakeAnAction(action, CookOperations.Bake, minutes, new string[] { "All"}, KitchenDevices.Oven);

            CurrentRecipe.WrittenRecipe += $"\n{CurrentRecipe.CountOfOperations}) " +
                $"Bake all {minutes} min";
        }
        public void CompleteRecipeCreation()
        {
            CurrentRecipe.WrittenRecipe += $"\nTime: {CurrentRecipe.SpentMinutes} minutes";
            CurrentRecipe.WrittenRecipe += $"\nPrice of ingredients: {Math.Round(CurrentRecipe.PriceOfDish, 2)}$";

            CurrentRecipe.IsCompleted = true;
        }


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
            string result = "\nAll _products:";
            foreach (Product product in _products)
            {
                result += $"\n{product.Name} - {product.TotalPrice}$";
            }
            return result;
        }
        public string FindIngredientsByStorageConditions(StorageConditions storageConditions)
        {
            string result = $"\nStorage conditions {storageConditions.StartTemperature}-{storageConditions.EndTemperature} deg:";
            foreach (Product product in _products)
            {
                if (product.Conditions.StartTemperature >= storageConditions.StartTemperature &&
                    product.Conditions.EndTemperature <= storageConditions.EndTemperature)
                {
                    result += $"\n{product.Name} - {product.TotalPrice}$";
                    result += $" ({product.Conditions.StartTemperature}-{product.Conditions.EndTemperature} deg)";
                }
            }
            return result;
        }
        public string FindIngredientsByNumberOfUses(NumOfUses value)
        {
            string result = $"\n{value} number of uses:";
            int minOrMax = _products[0].NumberOfUses;
            foreach (Product product in _products)
            {
                if (value == NumOfUses.Max)
                {
                    if (minOrMax < product.NumberOfUses)
                    {
                        minOrMax = product.NumberOfUses;
                    }
                }
                else
                {
                    if (minOrMax > product.NumberOfUses)
                    {
                        minOrMax = product.NumberOfUses;
                    }
                }
            }
            foreach (Product product in _products)
            {
                if (product.NumberOfUses == minOrMax)
                {
                    result += $"\n{product.Name} - {product.TotalPrice}$ (used {product.NumberOfUses} times)";
                }
            }
            return result;
        }
        public string ViewLongestProcessingProcedure()
        {
            string result = "\nLongest processing procedure:";
            result += $"\n{_longestProcedure.Operation} ({_longestProcedure.Device}) - {_longestProcedure.Minutes} min";
            return result;
        }
        public string ViewTheMostExpensiveProcessingProcedure()
        {
            string result = "\nThe most expensive processing procedure:";
            result += $"\nUsing coffee machine - {pricesForProcessingProcedures["CoffeeMachine"]}$/min";
            return result;
        }
    }
}
