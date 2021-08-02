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

        private const int PanCapacity = 2,
            SausepanCapacity = 5,
            GrillCapacity = 3,
            CountOfDishesInOven = 2,
            MaxCutIngredients = 5;

        private double _freeSpaceInPan,
            _freeSpaceInSaucepan,
            _freeSpaceInGrill;

        private static bool _alreadyExist = false;
        public Recipe CurrentRecipe;
        private ClientOrder _clientOrder = null;
        private int _counterOfDishes;
        ProcessingProcedure _longestProcedure;

        private readonly List<Product> _products = new List<Product>();
        private readonly List<Recipe> _recipes = new List<Recipe>();
        private readonly Dictionary<string, double> pricesForProcessingProcedures = new Dictionary<string, double> 
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

        /// <summary>
        /// Assigns an executable recipe
        /// </summary>
        /// <param name="clientNumber"> Number of client </param>
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

        /// <summary>
        /// Add products to list
        /// </summary>
        /// <param name="products"> List of products </param>
        public void AddProducts(List<Product> products)
        {
            _products.AddRange(products);
        }

        /// <summary>
        /// Cooking the dish
        /// </summary>
        /// <param name="name"> Name of the dish </param>
        /// <param name="type"> Type of dish </param>
        /// <param name="countOfPortions"> Count of portions </param>
        public void CookTheDish(string name, Manager.Menu type, int countOfPortions)
        {
            if (_clientOrder == null)
            {
                throw new Exception("No orders");
            }

            Recipe currentDish = Helper.DefineCurrentDish(name, type, _recipes);

            Helper.CheckDishInOrder(name, countOfPortions, _clientOrder);
            Helper.CheckExistenceOfAllProducts(currentDish, _products);

            _freeSpaceInPan = PanCapacity;
            _freeSpaceInSaucepan = SausepanCapacity;
            _freeSpaceInGrill = GrillCapacity;

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
                        if (action.NamesOfIngredients.Count > MaxCutIngredients)
                        {
                            throw new Exception("You can't cut more than 5 ingredients at the same time");
                        }
                        break;
                    case CookOperations.Bake:
                        CurrentRecipe.PriceOfDish += pricesForProcessingProcedures["Oven"] * action.Minutes;
                        if (countOfPortions > CountOfDishesInOven)
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

        /// <summary>
        /// Recipe creating
        /// </summary>
        /// <param name="recipe"> Selected recipe </param>
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

        /// <summary>
        /// Identification of ingredients for selected recipe
        /// </summary>
        /// <param name="ingredients"> List of ingredients </param>
        public void IdentifyIngredients(List<Recipe.Ingredient> ingredients)
        {
            CurrentRecipe.Ingredients.AddRange(ingredients);
            CurrentRecipe.PriceOfDish = Helper.CountPriceOfIngredients(CurrentRecipe.Ingredients, _products);

            CurrentRecipe.WrittenRecipe += "\nNecessary ingredients: ";
            foreach (Recipe.Ingredient product in ingredients)
            {
                CurrentRecipe.WrittenRecipe += $"\n\t{product.Name} - {product.Weight} kg; ";
            }
        }

        /// <summary>
        /// Taking selected cooking action
        /// </summary>
        /// <param name="action"> Action </param>
        /// <param name="operation"> Operation </param>
        /// <param name="spentMinutes"> Minutes spent on the operation </param>
        /// <param name="ingredientNames"> List of names of necessary ingredients </param>
        /// <param name="device"> Kitchen device </param>
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

        /// <summary>
        /// Adding some ingredients
        /// </summary>
        /// <param name="ingredientNames"> List of names of ingredients </param>
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

        /// <summary>
        /// Mixing some ingredients
        /// </summary>
        /// <param name="ingredientNames"> List of names of ingredients </param>
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

        /// <summary>
        /// Cutting ingredients into pieces of chosen size
        /// </summary>
        /// <param name="startSize"> Minimum size of pieces </param>
        /// <param name="endSize"> Maximum size of pieces </param>
        /// <param name="ingredientNames"> List of names of ingredients </param>
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

        /// <summary>
        /// Grating some ingredients
        /// </summary>
        /// <param name="ingredientNames"> List of names of ingredients </param>
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

        /// <summary>
        /// Squezing some ingredients
        /// </summary>
        /// <param name="ingredientNames"> List of names of ingredients </param>
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

        /// <summary>
        /// Mixing all ingredients
        /// </summary>
        public void MixAll()
        {
            CurrentRecipe.CountOfOperations++;
            CurrentRecipe.SpentMinutes += 1;

            CurrentRecipe.WrittenRecipe += $"\n{CurrentRecipe.CountOfOperations}) Mix all ingredients";
        }

        /// <summary>
        /// Frying some ingredients using selected device
        /// </summary>
        /// <param name="minutes"> Minutes spent on frying </param>
        /// <param name="device"> Selected device </param>
        /// <param name="ingredientNames"> List of names of ingredients </param>
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

        /// <summary>
        /// Boiling some ingredients using selected device
        /// </summary>
        /// <param name="minutes"> Minutes spent on boiling </param>
        /// <param name="device"> Selected device </param>
        /// <param name="ingredientNames"> List of names of ingredients </param>
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

        /// <summary>
        /// Baking some ingredients
        /// </summary>
        /// <param name="minutes"> Minutes spent on baking </param>
        /// <param name="ingredientNames"> List of names of ingredients </param>
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

        /// <summary>
        /// Baking all ingredients
        /// </summary>
        /// <param name="minutes"> Minutes spent on baking </param>
        public void Bake(int minutes)
        {
            Recipe.KitchenActions action = new Recipe.KitchenActions();

            TakeAnAction(action, CookOperations.Bake, minutes, new string[] { "All"}, KitchenDevices.Oven);

            CurrentRecipe.WrittenRecipe += $"\n{CurrentRecipe.CountOfOperations}) " +
                $"Bake all {minutes} min";
        }

        /// <summary>
        /// Completing creation of the recipe
        /// </summary>
        public void CompleteRecipeCreation()
        {
            CurrentRecipe.WrittenRecipe += $"\nTime: {CurrentRecipe.SpentMinutes} minutes";
            CurrentRecipe.WrittenRecipe += $"\nPrice of ingredients: {Math.Round(CurrentRecipe.PriceOfDish, 2)}$";

            CurrentRecipe.IsCompleted = true;
        }

        /// <summary>
        /// View all production capacity
        /// </summary>
        /// <returns> All production capacity </returns>
        public string ViewAllProductionCapacity()
        {
            string result = $"\nAll production capacity:";
            result += $"\nPan - {PanCapacity} kg";
            result += $"\nSaucepan - {SausepanCapacity} kg";
            result += $"\nGrill - {GrillCapacity} kg";
            result += $"\nCount of dishes in oven - {CountOfDishesInOven}";
            return result;
        }

        /// <summary>
        /// View all added ingredients
        /// </summary>
        /// <returns> All ingredients </returns>
        public string ViewAllIngredients()
        {
            string result = "\nAll products:";
            foreach (Product product in _products)
            {
                result += $"\n{product.Name} - {product.TotalPrice}$";
            }
            return result;
        }

        /// <summary>
        /// Searching for ingredients according to their storage condition
        /// </summary>
        /// <param name="storageConditions"> Storage conditions (start temperature - end temperature) </param>
        /// <returns> List of products that meet storage conditions </returns>
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

        /// <summary>
        /// Searching for ingredient which was used minimum or maximum times
        /// </summary>
        /// <param name="value"> Minimum or maximum </param>
        /// <returns> Ingredient - how many times was used </returns>
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

        /// <summary>
        /// View the longest processing procedure
        /// </summary>
        /// <returns> The longest processing procedure </returns>
        public string ViewLongestProcessingProcedure()
        {
            string result = "\nLongest processing procedure:";
            result += $"\n{_longestProcedure.Operation} ({_longestProcedure.Device}) - {_longestProcedure.Minutes} min";
            return result;
        }

        /// <summary>
        /// View the most expensive processing procedure
        /// </summary>
        /// <returns> The most expensive processing procedure </returns>
        public string ViewTheMostExpensiveProcessingProcedure()
        {
            string result = "\nThe most expensive processing procedure:";
            result += $"\nUsing coffee machine - {pricesForProcessingProcedures["CoffeeMachine"]}$/min";
            return result;
        }

        public override bool Equals(object obj)
        {
            return obj is ChiefCooker cooker &&
                   _freeSpaceInPan == cooker._freeSpaceInPan &&
                   _freeSpaceInSaucepan == cooker._freeSpaceInSaucepan &&
                   _freeSpaceInGrill == cooker._freeSpaceInGrill &&
                   EqualityComparer<Recipe>.Default.Equals(CurrentRecipe, cooker.CurrentRecipe) &&
                   EqualityComparer<ClientOrder>.Default.Equals(_clientOrder, cooker._clientOrder) &&
                   _counterOfDishes == cooker._counterOfDishes &&
                   EqualityComparer<ProcessingProcedure>.Default.Equals(_longestProcedure, cooker._longestProcedure) &&
                   EqualityComparer<List<Product>>.Default.Equals(_products, cooker._products) &&
                   EqualityComparer<List<Recipe>>.Default.Equals(_recipes, cooker._recipes) &&
                   EqualityComparer<Dictionary<string, double>>.Default.Equals(pricesForProcessingProcedures, cooker.pricesForProcessingProcedures);
        }

        public override int GetHashCode()
        {
            int hashCode = 1304779551;
            hashCode = hashCode * -1521134295 + _freeSpaceInPan.GetHashCode();
            hashCode = hashCode * -1521134295 + _freeSpaceInSaucepan.GetHashCode();
            hashCode = hashCode * -1521134295 + _freeSpaceInGrill.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Recipe>.Default.GetHashCode(CurrentRecipe);
            hashCode = hashCode * -1521134295 + EqualityComparer<ClientOrder>.Default.GetHashCode(_clientOrder);
            hashCode = hashCode * -1521134295 + _counterOfDishes.GetHashCode();
            hashCode = hashCode * -1521134295 + _longestProcedure.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Product>>.Default.GetHashCode(_products);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Recipe>>.Default.GetHashCode(_recipes);
            hashCode = hashCode * -1521134295 + EqualityComparer<Dictionary<string, double>>.Default.GetHashCode(pricesForProcessingProcedures);
            return hashCode;
        }
    }
}
