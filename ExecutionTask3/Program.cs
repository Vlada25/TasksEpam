using ClassLibraryBistro;
using static ClassLibraryBistro.ChiefCooker;
using static ClassLibraryBistro.Manager;
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
                
                manager.TakeOrder("023", Menu.Salad, "Caesar", 8, "10:23");
                manager.AddDishToOrder("023", Menu.Meat, "Steak", 16);
                manager.AddDishToOrder("023", Menu.Coctail, "Mojito", 8);

                manager.TakeOrder("105", Menu.Soup, "Kharcho", 3, "11:00");
                manager.AddDishToOrder("105", Menu.Dessert, "Cherry pie", 1);
                manager.AddDishToOrder("105", Menu.HotDrink, "Tea", 2);
                manager.AddDishToOrder("105", Menu.HotDrink, "Cappuccino", 1);

                manager.TakeOrder("040", Menu.HotDrink, "Cappuccino", 2, "16:40");

                chiefCooker.AddProducts(FileReader.ReadProducts());

                // Salad Caesar
                chiefCooker.CreateRecipe(new Recipe("Caesar", Menu.Salad));
                chiefCooker.IdentifyIngredients(FileReader.ReadRecipeIngredients("Caesar"));
                chiefCooker.Cut(1, 2, "Garlic");
                chiefCooker.Cut(10, 30, "Chicken");
                chiefCooker.Fry(10, KitchenDevices.Pan, "Butter", "Garlic", "Chicken");
                chiefCooker.Cut(5, 15, "White bread");
                chiefCooker.Fry(10, KitchenDevices.Pan, "White bread");
                chiefCooker.Cut(3, 5, "Tomato");
                chiefCooker.Grate("Cheese");
                chiefCooker.MixAll();
                chiefCooker.CompleteRecipeCreation();

                // Steak
                chiefCooker.CreateRecipe(new Recipe("Steak", Menu.Meat));
                chiefCooker.IdentifyIngredients(FileReader.ReadRecipeIngredients("Steak"));
                chiefCooker.Cut(20, 30, "Beef");
                chiefCooker.MixAll();
                chiefCooker.Fry(7, KitchenDevices.Grill, "Butter", "Beef");
                chiefCooker.CompleteRecipeCreation();

                // Mojito
                chiefCooker.CreateRecipe(new Recipe("Mojito", Menu.Coctail));
                chiefCooker.IdentifyIngredients(FileReader.ReadRecipeIngredients("Mojito"));
                chiefCooker.Squeze("Lime");
                chiefCooker.MixAll();
                chiefCooker.CompleteRecipeCreation();

                // Soup Kharcho
                chiefCooker.CreateRecipe(new Recipe("Kharcho", Menu.Soup));
                chiefCooker.IdentifyIngredients(FileReader.ReadRecipeIngredients("Kharcho"));
                chiefCooker.Cut(50, 70, "Chicken");
                chiefCooker.Boil(35, KitchenDevices.Saucepan, "Water", "Chicken");
                chiefCooker.Add("Rice", "Salt");
                chiefCooker.Boil(10, KitchenDevices.Saucepan, "Rice", "Salt");
                chiefCooker.Cut(3, 5, "Onion", "Carrot", "Garlic", "Greens");
                chiefCooker.Fry(5, KitchenDevices.Pan, "Butter", "Onion", "Carrot");
                chiefCooker.Squeze("Tomato");
                chiefCooker.Add("Garlic");
                chiefCooker.MixAll();
                chiefCooker.CompleteRecipeCreation();

                // Cherry pie
                chiefCooker.CreateRecipe(new Recipe("Cherry pie", Menu.Dessert));
                chiefCooker.IdentifyIngredients(FileReader.ReadRecipeIngredients("Cherry pie"));
                chiefCooker.Mix("Egg", "Sugar");
                chiefCooker.Add("Sour cream", "Butter", "Baking soda", "Flour");
                chiefCooker.Add("Cherry");
                chiefCooker.Bake(30);
                chiefCooker.CompleteRecipeCreation();

                // Tea
                chiefCooker.CreateRecipe(new Recipe("Tea", Menu.HotDrink));
                chiefCooker.IdentifyIngredients(FileReader.ReadRecipeIngredients("Tea"));
                chiefCooker.Boil(5, KitchenDevices.Kettle, "Water");
                chiefCooker.Add("Tea");
                chiefCooker.CompleteRecipeCreation();

                // Cappuccino
                chiefCooker.CreateRecipe(new Recipe("Cappuccino", Menu.HotDrink));
                chiefCooker.IdentifyIngredients(FileReader.ReadRecipeIngredients("Cappuccino"));
                chiefCooker.Boil(5, KitchenDevices.CoffeeMachine, "Water", "Coffee");
                chiefCooker.Mix("Milk");
                chiefCooker.Add("Cinnamon");
                chiefCooker.CompleteRecipeCreation();

                chiefCooker.CompleteTheOrder("023");
                chiefCooker.CookTheDish("Caesar", Menu.Salad, 8);
                chiefCooker.CookTheDish("Steak", Menu.Meat, 14);
                chiefCooker.CookTheDish("Steak", Menu.Meat, 2);
                chiefCooker.CookTheDish("Mojito", Menu.Coctail, 8);

                chiefCooker.CompleteTheOrder("105");
                chiefCooker.CookTheDish("Kharcho", Menu.Soup, 3);
                chiefCooker.CookTheDish("Cherry pie", Menu.Dessert, 1);
                chiefCooker.CookTheDish("Tea", Menu.HotDrink, 2);
                chiefCooker.CookTheDish("Cappuccino", Menu.HotDrink, 1);

                chiefCooker.CompleteTheOrder("040");
                chiefCooker.CookTheDish("Cappuccino", Menu.HotDrink, 1);

                FileWriter.CleanFile();
                FileWriter.Write(chiefCooker.ViewAllProductionCapacity());
                FileWriter.Write(chiefCooker.ViewAllIngredients());
                FileWriter.Write(manager.ViewOrdersInTime("10:00", "17:30"));
                FileWriter.Write(chiefCooker.FindIngredientsByNumberOfUses(NumOfUses.Max));
                FileWriter.Write(chiefCooker.FindIngredientsByStorageConditions(new StorageConditions(-10, 10)));
                FileWriter.Write(chiefCooker.ViewLongestProcessingProcedure());
                FileWriter.Write(chiefCooker.ViewTheMostExpensiveProcessingProcedure());
            }
            catch(Exception error)
            {
                Console.WriteLine("Error: " + error.Message);
            }
        }
    }
}
