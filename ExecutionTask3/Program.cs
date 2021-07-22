using ClassLibraryBistro;
using System;

namespace ExecutionTask3
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Manager manager = new Manager();
                ChiefCooker chiefCooker = new ChiefCooker();
                
                manager.TakeOrder("023", Manager.Menu.Salad, "Caesar", 8, "10:23");
                manager.AddDishToOrder("023", Manager.Menu.Meat, "Steak", 16);
                manager.AddDishToOrder("023", Manager.Menu.Coctail, "Mojito", 8);

                chiefCooker.CreateRecipe(new Recipe("Caesar", Manager.Menu.Salad));
                chiefCooker.IdentifyIngredients(
                    new Recipe.Ingredient("Green salad", 0.03),
                    new Recipe.Ingredient("Garlic", 0.002),
                    new Recipe.Ingredient("Chicken fillet", 0.1),
                    new Recipe.Ingredient("Butter", 0.005),
                    new Recipe.Ingredient("White bread", 0.05),
                    new Recipe.Ingredient("Tomato", 0.05),
                    new Recipe.Ingredient("Cheese", 0.05),
                    new Recipe.Ingredient("Mayonnaise", 0.02)
                    );
                chiefCooker.CutDirection(1, 2, "Garlic");
                chiefCooker.CutDirection(10, 30, "Chicken fillet");
                chiefCooker.FryDirection(10, ChiefCooker.KitchenDevices.Pan, "Butter", "Garlic", "Chicken fillet");
                chiefCooker.CutDirection(5, 15, "White bread");
                chiefCooker.FryDirection(10, ChiefCooker.KitchenDevices.Pan, "White bread");
                chiefCooker.CutDirection(3, 5, "Tomato");
                chiefCooker.GrateDirection("Cheese");
                chiefCooker.MixAllDirection();
                chiefCooker.CompleteRecipeCreation();

                chiefCooker.AddProducts(
                    new ChiefCooker.Product("Green salad", 17.63), 
                    new ChiefCooker.Product("Tomato", 4.9),
                    new ChiefCooker.Product("Chicken fillet", 7.5),
                    new ChiefCooker.Product("White bread", 2.2),
                    new ChiefCooker.Product("Butter", 20.28),
                    new ChiefCooker.Product("Garlic", 6.6),
                    new ChiefCooker.Product("Cheese", 16.7),
                    new ChiefCooker.Product("Mayonnaise", 6.42)
                    );

                chiefCooker.CompleteTheOrder("023");
                chiefCooker.CookTheDish("Caesar", Manager.Menu.Salad, 8);

                /*
                chiefCooker.AddProduct("Beef", 15.2);

                chiefCooker.AddProduct("Tonic", 1.5);
                chiefCooker.AddProduct("White rum", 67);
                chiefCooker.AddProduct("Lime", 7.5);
                chiefCooker.AddProduct("Mint", 39.8);
                chiefCooker.AddProduct("Ice", 2);
                */

                Console.WriteLine(manager.ViewAllOrders());
                //Console.WriteLine(chiefCooker.ViewAllIngredients());
                //Console.WriteLine(chiefCooker.ViewCurrentOrder());
                //Console.WriteLine(chiefCooker.currentRecipe.ToString());
            }
            catch(Exception error)
            {
                Console.WriteLine("Error: " + error.Message);
            }
        }
    }
}
