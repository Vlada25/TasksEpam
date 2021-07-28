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

                manager.TakeOrder("105", Manager.Menu.Soup, "Kharcho", 3, "11:00");
                manager.AddDishToOrder("105", Manager.Menu.Dessert, "Cherry pie", 1);
                manager.AddDishToOrder("105", Manager.Menu.HotDrink, "Tea", 2);
                manager.AddDishToOrder("105", Manager.Menu.HotDrink, "Cappuccino", 1);

                chiefCooker.AddProducts(
                    new ChiefCooker.Product("Green salad", 17.63, new ChiefCooker.StorageConditions(0, 20)),
                    new ChiefCooker.Product("Tomato", 4.9, new ChiefCooker.StorageConditions(0, 20)),
                    new ChiefCooker.Product("Chicken", 7.5, new ChiefCooker.StorageConditions(-25, 10)),
                    new ChiefCooker.Product("White bread", 2.2, new ChiefCooker.StorageConditions(5, 25)),
                    new ChiefCooker.Product("Butter", 20.28, new ChiefCooker.StorageConditions(-25, 10)),
                    new ChiefCooker.Product("Garlic", 6.6, new ChiefCooker.StorageConditions(0, 20)),
                    new ChiefCooker.Product("Cheese", 16.7, new ChiefCooker.StorageConditions(0, 10)),
                    new ChiefCooker.Product("Mayonnaise", 6.42, new ChiefCooker.StorageConditions(0, 10)),
                    new ChiefCooker.Product("Beef", 33, new ChiefCooker.StorageConditions(-25, 10)),
                    new ChiefCooker.Product("Flavoring", 27.4, new ChiefCooker.StorageConditions(5, 25)),
                    new ChiefCooker.Product("Sugar", 1.54, new ChiefCooker.StorageConditions(10, 25)),
                    new ChiefCooker.Product("Soda", 2, new ChiefCooker.StorageConditions(0, 10)),
                    new ChiefCooker.Product("White rum", 67, new ChiefCooker.StorageConditions(10, 20)),
                    new ChiefCooker.Product("Lime", 7.5, new ChiefCooker.StorageConditions(0, 20)),
                    new ChiefCooker.Product("Mint", 39.8, new ChiefCooker.StorageConditions(0, 20)),
                    new ChiefCooker.Product("Ice", 2, new ChiefCooker.StorageConditions(-25, -15)),
                    new ChiefCooker.Product("Greens", 16.9, new ChiefCooker.StorageConditions(0, 20)),
                    new ChiefCooker.Product("Rice", 2.2, new ChiefCooker.StorageConditions(10, 25)),
                    new ChiefCooker.Product("Onion", 1.6, new ChiefCooker.StorageConditions(0, 20)),
                    new ChiefCooker.Product("Carrot", 1.55, new ChiefCooker.StorageConditions(0, 20)),
                    new ChiefCooker.Product("Salt", 0.64, new ChiefCooker.StorageConditions(10, 25)),
                    new ChiefCooker.Product("Water", 0.5, new ChiefCooker.StorageConditions(0, 10)),
                    new ChiefCooker.Product("Egg", 3, new ChiefCooker.StorageConditions(0, 10)),
                    new ChiefCooker.Product("Sour cream", 5.25, new ChiefCooker.StorageConditions(0, 10)),
                    new ChiefCooker.Product("Baking soda", 2.18, new ChiefCooker.StorageConditions(10, 25)),
                    new ChiefCooker.Product("Flour", 1.28, new ChiefCooker.StorageConditions(10, 25)),
                    new ChiefCooker.Product("Cherry", 9, new ChiefCooker.StorageConditions(0, 20)),
                    new ChiefCooker.Product("Tea", 800, new ChiefCooker.StorageConditions(10, 25)),
                    new ChiefCooker.Product("Coffee", 35, new ChiefCooker.StorageConditions(10, 25)),
                    new ChiefCooker.Product("Milk", 1.34, new ChiefCooker.StorageConditions(0, 10)),
                    new ChiefCooker.Product("Cinnamon", 18, new ChiefCooker.StorageConditions(10, 25))
                    );

                // Salad Caesar
                chiefCooker.CreateRecipe(new Recipe("Caesar", Manager.Menu.Salad));
                chiefCooker.IdentifyIngredients(
                    new Recipe.Ingredient("Green salad", 0.03),
                    new Recipe.Ingredient("Garlic", 0.002),
                    new Recipe.Ingredient("Chicken", 0.1),
                    new Recipe.Ingredient("Butter", 0.005),
                    new Recipe.Ingredient("White bread", 0.05),
                    new Recipe.Ingredient("Tomato", 0.05),
                    new Recipe.Ingredient("Cheese", 0.05),
                    new Recipe.Ingredient("Mayonnaise", 0.02),
                    new Recipe.Ingredient("Flavoring", 0.002)
                    );
                chiefCooker.Cut(1, 2, "Garlic");
                chiefCooker.Cut(10, 30, "Chicken");
                chiefCooker.Fry(10, ChiefCooker.KitchenDevices.Pan, "Butter", "Garlic", "Chicken");
                chiefCooker.Cut(5, 15, "White bread");
                chiefCooker.Fry(10, ChiefCooker.KitchenDevices.Pan, "White bread");
                chiefCooker.Cut(3, 5, "Tomato");
                chiefCooker.Grate("Cheese");
                chiefCooker.MixAll();
                chiefCooker.CompleteRecipeCreation();

                // Steak
                chiefCooker.CreateRecipe(new Recipe("Steak", Manager.Menu.Meat));
                chiefCooker.IdentifyIngredients(
                    new Recipe.Ingredient("Beef", 0.2),
                    new Recipe.Ingredient("Butter", 0.01),
                    new Recipe.Ingredient("Flavoring", 0.002)
                    );
                chiefCooker.Cut(20, 30, "Beef");
                chiefCooker.MixAll();
                chiefCooker.Fry(7, ChiefCooker.KitchenDevices.Grill, "Butter", "Beef");
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
                chiefCooker.Squeze("Lime");
                chiefCooker.MixAll();
                chiefCooker.CompleteRecipeCreation();

                // Soup Kharcho
                chiefCooker.CreateRecipe(new Recipe("Kharcho", Manager.Menu.Soup));
                chiefCooker.IdentifyIngredients(
                    new Recipe.Ingredient("Chicken", 0.02),
                    new Recipe.Ingredient("Rice", 0.03),
                    new Recipe.Ingredient("Garlic", 0.002),
                    new Recipe.Ingredient("Butter", 0.01),
                    new Recipe.Ingredient("Onion", 0.02),
                    new Recipe.Ingredient("Carrot", 0.025),
                    new Recipe.Ingredient("Tomato", 0.015),
                    new Recipe.Ingredient("Greens", 0.01),
                    new Recipe.Ingredient("Salt", 0.0015),
                    new Recipe.Ingredient("Water", 0.42)
                    );
                chiefCooker.Cut(50, 70, "Chicken");
                chiefCooker.Boil(35, ChiefCooker.KitchenDevices.Saucepan, "Water", "Chicken");
                chiefCooker.Add("Rice", "Salt");
                chiefCooker.Boil(10, ChiefCooker.KitchenDevices.Saucepan, "Rice", "Salt");
                chiefCooker.Cut(3, 5, "Onion", "Carrot", "Garlic", "Greens");
                chiefCooker.Fry(5, ChiefCooker.KitchenDevices.Pan, "Butter", "Onion", "Carrot");
                chiefCooker.Squeze("Tomato");
                chiefCooker.Add("Garlic");
                chiefCooker.MixAll();
                chiefCooker.CompleteRecipeCreation();

                // Cherry pie
                chiefCooker.CreateRecipe(new Recipe("Cherry pie", Manager.Menu.Dessert));
                chiefCooker.IdentifyIngredients(
                    new Recipe.Ingredient("Egg", 0.08),
                    new Recipe.Ingredient("Sugar", 0.17),
                    new Recipe.Ingredient("Sour cream", 0.23),
                    new Recipe.Ingredient("Butter", 0.1),
                    new Recipe.Ingredient("Baking soda", 0.001),
                    new Recipe.Ingredient("Flour", 0.3),
                    new Recipe.Ingredient("Cherry", 0.3)
                    );
                chiefCooker.Mix("Egg", "Sugar");
                chiefCooker.Add("Sour cream", "Butter", "Baking soda", "Flour");
                chiefCooker.Add("Cherry");
                chiefCooker.Bake(30);
                chiefCooker.CompleteRecipeCreation();

                // Tea
                chiefCooker.CreateRecipe(new Recipe("Tea", Manager.Menu.HotDrink));
                chiefCooker.IdentifyIngredients(
                    new Recipe.Ingredient("Tea", 0.002),
                    new Recipe.Ingredient("Water", 0.2)
                    );
                chiefCooker.Boil(5, ChiefCooker.KitchenDevices.Kettle, "Water");
                chiefCooker.Add("Tea");
                chiefCooker.CompleteRecipeCreation();

                // Cappuccino
                chiefCooker.CreateRecipe(new Recipe("Cappuccino", Manager.Menu.HotDrink));
                chiefCooker.IdentifyIngredients(
                    new Recipe.Ingredient("Water", 0.06),
                    new Recipe.Ingredient("Coffee", 0.007),
                    new Recipe.Ingredient("Milk", 0.07),
                    new Recipe.Ingredient("Cinnamon", 0.003)
                    );
                chiefCooker.Boil(5, ChiefCooker.KitchenDevices.CoffeeMachine, "Water", "Coffee");
                chiefCooker.Mix("Milk");
                chiefCooker.Add("Cinnamon");
                chiefCooker.CompleteRecipeCreation();


                chiefCooker.CompleteTheOrder("023");
                chiefCooker.CookTheDish("Caesar", Manager.Menu.Salad, 8);
                chiefCooker.CookTheDish("Steak", Manager.Menu.Meat, 14);
                chiefCooker.CookTheDish("Steak", Manager.Menu.Meat, 2);
                chiefCooker.CookTheDish("Mojito", Manager.Menu.Coctail, 8);

                chiefCooker.CompleteTheOrder("105");
                chiefCooker.CookTheDish("Kharcho", Manager.Menu.Soup, 3);
                chiefCooker.CookTheDish("Cherry pie", Manager.Menu.Dessert, 1);
                chiefCooker.CookTheDish("Tea", Manager.Menu.HotDrink, 2);

                Console.WriteLine(chiefCooker.ViewAllProductionCapacity());
                Console.WriteLine(chiefCooker.ViewAllIngredients());
                Console.WriteLine(manager.ViewOrdersInTime("10:00", "11:30"));
                Console.WriteLine(chiefCooker.FindIngredientsByNumberOfUses(ChiefCooker.NumOfUses.Max));
                Console.WriteLine(chiefCooker.FindIngredientsByStorageConditions(new ChiefCooker.StorageConditions(-10, 10)));
                Console.WriteLine(chiefCooker.ViewLongestProcessingProcedure());
                Console.WriteLine(chiefCooker.ViewTheMostExpensiveProcessingProcedure());
            }
            catch(Exception error)
            {
                Console.WriteLine("Error: " + error.Message);
                Console.WriteLine(error.StackTrace);
            }
        }
    }
}
