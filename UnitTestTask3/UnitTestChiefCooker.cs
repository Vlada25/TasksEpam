using ClassLibraryBistro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using static ClassLibraryBistro.ChiefCooker;

namespace UnitTestTask3
{
    [TestClass]
    public class UnitTestChiefCooker
    {
        [TestMethod]
        public void CreateRecipe_CurrentRecipeName_ReturnTea()
        {
            Manager manager = new Manager();
            ChiefCooker chiefCooker = new ChiefCooker();

            manager.TakeOrder("100", Manager.Menu.HotDrink, "Tea", 2, "12:00");
            chiefCooker.CreateRecipe(new Recipe("Tea", Manager.Menu.HotDrink));

            string result = chiefCooker.CurrentRecipe.Name;
            string expected = "Tea";

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void IdentifyIngredients_CountOfIngredients_Return2()
        {
            Manager manager = new Manager();
            ChiefCooker chiefCooker = new ChiefCooker();

            manager.TakeOrder("100", Manager.Menu.HotDrink, "Tea", 2, "12:00");
            chiefCooker.CreateRecipe(new Recipe("Tea", Manager.Menu.HotDrink));

            List<Recipe.Ingredient> ingredients = new List<Recipe.Ingredient>
            {
                new Recipe.Ingredient("Tea", 0.002),
                new Recipe.Ingredient("Water", 0.2)
            };

            chiefCooker.IdentifyIngredients(ingredients);

            int result = chiefCooker.CurrentRecipe.Ingredients.Count;
            int expected = 2;

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Add_CookOperationName_ReturnAdd()
        {
            Manager manager = new Manager();
            ChiefCooker chiefCooker = new ChiefCooker();

            manager.TakeOrder("100", Manager.Menu.HotDrink, "Recipe", 1, "12:00");

            chiefCooker.CreateRecipe(new Recipe("Recipe", Manager.Menu.HotDrink));

            List<Recipe.Ingredient> ingredients = new List<Recipe.Ingredient>
            {
                new Recipe.Ingredient("Water", 0.2)
            };

            chiefCooker.IdentifyIngredients(ingredients);
            chiefCooker.Add("Water");

            string result = Convert.ToString(chiefCooker.CurrentRecipe.Actions[0].CookOperation);
            string expected = "Add";

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Cut_CookOperationName_ReturnCut()
        {
            Manager manager = new Manager();
            ChiefCooker chiefCooker = new ChiefCooker();

            manager.TakeOrder("100", Manager.Menu.Soup, "Recipe", 1, "12:00");

            chiefCooker.CreateRecipe(new Recipe("Recipe", Manager.Menu.Soup));

            List<Recipe.Ingredient> ingredients = new List<Recipe.Ingredient>
            {
                new Recipe.Ingredient("Carrot", 0.2)
            };

            chiefCooker.IdentifyIngredients(ingredients);
            chiefCooker.Cut(5, 10, "Carrot");

            string result = Convert.ToString(chiefCooker.CurrentRecipe.Actions[0].CookOperation);
            string expected = "Cut";

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Grate_CookOperationName_ReturnGrate()
        {
            Manager manager = new Manager();
            ChiefCooker chiefCooker = new ChiefCooker();

            manager.TakeOrder("100", Manager.Menu.Salad, "Recipe", 1, "12:00");

            chiefCooker.CreateRecipe(new Recipe("Recipe", Manager.Menu.Salad));

            List<Recipe.Ingredient> ingredients = new List<Recipe.Ingredient>
            {
                new Recipe.Ingredient("Cheese", 0.2)
            };

            chiefCooker.IdentifyIngredients(ingredients);
            chiefCooker.Grate("Cheese");

            string result = Convert.ToString(chiefCooker.CurrentRecipe.Actions[0].CookOperation);
            string expected = "Grate";

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Mix_CookOperationName_ReturnMix()
        {
            Manager manager = new Manager();
            ChiefCooker chiefCooker = new ChiefCooker();

            manager.TakeOrder("100", Manager.Menu.HotDrink, "Recipe", 1, "12:00");

            chiefCooker.CreateRecipe(new Recipe("Recipe", Manager.Menu.HotDrink));

            List<Recipe.Ingredient> ingredients = new List<Recipe.Ingredient>
            {
                new Recipe.Ingredient("Water", 0.2),
                new Recipe.Ingredient("Mint", 0.002)
            };

            chiefCooker.IdentifyIngredients(ingredients);
            chiefCooker.Mix("Water", "Mint");

            string result = Convert.ToString(chiefCooker.CurrentRecipe.Actions[0].CookOperation);
            string expected = "Mix";

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Fry_CookOperationName_ReturnFry()
        {
            Manager manager = new Manager();
            ChiefCooker chiefCooker = new ChiefCooker();

            manager.TakeOrder("100", Manager.Menu.Soup, "Recipe", 1, "12:00");

            chiefCooker.CreateRecipe(new Recipe("Recipe", Manager.Menu.Soup));

            List<Recipe.Ingredient> ingredients = new List<Recipe.Ingredient>
            {
                new Recipe.Ingredient("Carrot", 0.2)
            };

            chiefCooker.IdentifyIngredients(ingredients);
            chiefCooker.Fry(5, KitchenDevices.Pan, "Carrot");

            string result = Convert.ToString(chiefCooker.CurrentRecipe.Actions[0].CookOperation);
            string expected = "Fry";

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Boil_CookOperationName_ReturnBoil()
        {
            Manager manager = new Manager();
            ChiefCooker chiefCooker = new ChiefCooker();

            manager.TakeOrder("100", Manager.Menu.Soup, "Recipe", 1, "12:00");

            chiefCooker.CreateRecipe(new Recipe("Recipe", Manager.Menu.Soup));

            List<Recipe.Ingredient> ingredients = new List<Recipe.Ingredient>
            {
                new Recipe.Ingredient("Water", 0.2)
            };

            chiefCooker.IdentifyIngredients(ingredients);
            chiefCooker.Boil(5, KitchenDevices.Kettle, "Water");

            string result = Convert.ToString(chiefCooker.CurrentRecipe.Actions[0].CookOperation);
            string expected = "Boil";

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Bake_CookOperationName_ReturnBake()
        {
            Manager manager = new Manager();
            ChiefCooker chiefCooker = new ChiefCooker();

            manager.TakeOrder("100", Manager.Menu.Soup, "Recipe", 1, "12:00");

            chiefCooker.CreateRecipe(new Recipe("Recipe", Manager.Menu.Soup));

            List<Recipe.Ingredient> ingredients = new List<Recipe.Ingredient>
            {
                new Recipe.Ingredient("Carrot", 0.2)
            };

            chiefCooker.IdentifyIngredients(ingredients);
            chiefCooker.Bake(10, "Carrot");

            string result = Convert.ToString(chiefCooker.CurrentRecipe.Actions[0].CookOperation);
            string expected = "Bake";

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void Squeeze_CookOperationName_ReturnSqueeze()
        {
            Manager manager = new Manager();
            ChiefCooker chiefCooker = new ChiefCooker();

            manager.TakeOrder("100", Manager.Menu.Soup, "Recipe", 1, "12:00");

            chiefCooker.CreateRecipe(new Recipe("Recipe", Manager.Menu.Soup));

            List<Recipe.Ingredient> ingredients = new List<Recipe.Ingredient>
            {
                new Recipe.Ingredient("Lime", 0.02)
            };

            chiefCooker.IdentifyIngredients(ingredients);
            chiefCooker.Squeze("Lime");

            string result = Convert.ToString(chiefCooker.CurrentRecipe.Actions[0].CookOperation);
            string expected = "Squeeze";

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void MixAll_CountOfOperations_Return1()
        {
            Manager manager = new Manager();
            ChiefCooker chiefCooker = new ChiefCooker();

            manager.TakeOrder("100", Manager.Menu.Soup, "Recipe", 1, "12:00");

            chiefCooker.CreateRecipe(new Recipe("Recipe", Manager.Menu.Soup));

            List<Recipe.Ingredient> ingredients = new List<Recipe.Ingredient>
            {
                new Recipe.Ingredient("Water", 0.2),
                new Recipe.Ingredient("Lime", 0.02)
            };

            chiefCooker.IdentifyIngredients(ingredients);
            chiefCooker.MixAll();

            int result = chiefCooker.CurrentRecipe.CountOfOperations;
            int expected = 1;

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void CompleteRecipeCreation_IsCompleted_ReturnTrue()
        {
            Manager manager = new Manager();
            ChiefCooker chiefCooker = new ChiefCooker();

            List<Recipe.Ingredient> ingredients = new List<Recipe.Ingredient>
            {
                new Recipe.Ingredient("Tea", 0.002),
                new Recipe.Ingredient("Water", 0.2)
            };

            manager.TakeOrder("040", Manager.Menu.HotDrink, "Tea", 2, "16:40");

            chiefCooker.CreateRecipe(new Recipe("Tea", Manager.Menu.HotDrink));
            chiefCooker.IdentifyIngredients(ingredients);
            chiefCooker.Boil(5, KitchenDevices.Kettle, "Water");
            chiefCooker.Add("Tea");
            chiefCooker.CompleteRecipeCreation();

            Assert.IsTrue(chiefCooker.CurrentRecipe.IsCompleted);
        }

        [TestMethod]
        public void CompleteTheOrder_OrderInProgress_ReturnTrue()
        {
            Manager manager = new Manager();
            ChiefCooker chiefCooker = new ChiefCooker();

            List<Recipe.Ingredient> ingredients = new List<Recipe.Ingredient>
            {
                new Recipe.Ingredient("Tea", 0.002),
                new Recipe.Ingredient("Water", 0.2)
            };

            manager.TakeOrder("040", Manager.Menu.HotDrink, "Tea", 2, "16:40");

            chiefCooker.CreateRecipe(new Recipe("Tea", Manager.Menu.HotDrink));
            chiefCooker.IdentifyIngredients(ingredients);
            chiefCooker.Boil(5, KitchenDevices.Kettle, "Water");
            chiefCooker.Add("Tea");
            chiefCooker.CompleteRecipeCreation();

            chiefCooker.CompleteTheOrder("040");

            bool result = Manager.ClientOrdersList[0].OrderInProgress;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CookTheDish_IsDone_ReturnTrue()
        {
            Manager manager = new Manager();
            ChiefCooker chiefCooker = new ChiefCooker();

            List<Recipe.Ingredient> ingredients = new List<Recipe.Ingredient>
            {
                new Recipe.Ingredient("Tea", 0.002),
                new Recipe.Ingredient("Water", 0.2)
            };

            manager.TakeOrder("040", Manager.Menu.HotDrink, "Tea", 2, "16:40");

            chiefCooker.CreateRecipe(new Recipe("Tea", Manager.Menu.HotDrink));
            chiefCooker.IdentifyIngredients(ingredients);
            chiefCooker.Boil(5, KitchenDevices.Kettle, "Water");
            chiefCooker.Add("Tea");
            chiefCooker.CompleteRecipeCreation();

            List<Product> products = new List<Product> {
                new Product("Tea", 800, new StorageConditions(10, 25)),
                new Product("Water", 0.5, new StorageConditions(0, 10))
            };

            chiefCooker.AddProducts(products);

            chiefCooker.CompleteTheOrder("040");
            chiefCooker.CookTheDish("Tea", Manager.Menu.HotDrink, 2);

            bool result = Manager.ClientOrdersList[0].IsDone;

            Assert.IsTrue(result);
        }
    }
}
