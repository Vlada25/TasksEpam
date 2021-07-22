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
                manager.AddDishToOrder("023", Manager.Menu.Meat, "Steak", 8);
                manager.AddDishToOrder("023", Manager.Menu.Coctail, "Mojito", 8);

                // Salad Caesar
                chiefCooker.CreateRecipe(new Recipe("Caesar", Manager.Menu.Salad));
                chiefCooker.IdentifyIngredients(
                    new Recipe.Ingredient("Green salad", 0.03),
                    new Recipe.Ingredient("Garlic", 0.002),
                    new Recipe.Ingredient("Chicken fillet", 0.1),
                    new Recipe.Ingredient("Butter", 0.005),
                    new Recipe.Ingredient("White bread", 0.05),
                    new Recipe.Ingredient("Tomato", 0.05),
                    new Recipe.Ingredient("Cheese", 0.05),
                    new Recipe.Ingredient("Mayonnaise", 0.02),
                    new Recipe.Ingredient("Flavoring", 0.002)
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

                // Steak
                chiefCooker.CreateRecipe(new Recipe("Steak", Manager.Menu.Meat));
                chiefCooker.IdentifyIngredients(
                    new Recipe.Ingredient("Beef", 0.15),
                    new Recipe.Ingredient("Butter", 0.01),
                    new Recipe.Ingredient("Flavoring", 0.002)
                    );
                chiefCooker.CutDirection(20, 30, "Beef");
                chiefCooker.MixAllDirection();
                chiefCooker.FryDirection(7, ChiefCooker.KitchenDevices.Grill, "Butter", "Beef");
                chiefCooker.CompleteRecipeCreation();

                // Mojito
                chiefCooker.CreateRecipe(new Recipe("Mojito", Manager.Menu.Coctail));
                chiefCooker.IdentifyIngredients(
                    new Recipe.Ingredient("Sugar", 0.013),
                    new Recipe.Ingredient("Soda", 0.09),
                    new Recipe.Ingredient("White rum", 0.045),
                    new Recipe.Ingredient("Lime", 0.05),
                    new Recipe.Ingredient("Mint", 0.01),
                    new Recipe.Ingredient("Ice", 0.05)
                    );
                chiefCooker.SqueezeDirection("Lime");
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
                    new ChiefCooker.Product("Mayonnaise", 6.42),
                    new ChiefCooker.Product("Beef", 33),
                    new ChiefCooker.Product("Flavoring", 27.4),
                    new ChiefCooker.Product("Sugar", 1.54),
                    new ChiefCooker.Product("Soda", 2),
                    new ChiefCooker.Product("White rum", 67),
                    new ChiefCooker.Product("Lime", 7.5),
                    new ChiefCooker.Product("Mint", 39.8),
                    new ChiefCooker.Product("Ice", 2)
                    );

                chiefCooker.CompleteTheOrder("023");
                chiefCooker.CookTheDish("Caesar", Manager.Menu.Salad, 8);
                chiefCooker.CookTheDish("Steak", Manager.Menu.Meat, 8);
                chiefCooker.CookTheDish("Mojito", Manager.Menu.Coctail, 8);

                Console.WriteLine(manager.ViewAllOrders());
                Console.WriteLine(chiefCooker.ViewRecipe("Steak"));
                Console.WriteLine(chiefCooker.ViewRecipe("Mojito"));
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
