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

                manager.TakeOrder("023", Manager.Menu.Salad, "Caesar", 5, "10:23");
                manager.AddDishToOrder("023", Manager.Menu.Meat, "Barbeque", 8);
                manager.AddDishToOrder("023", Manager.Menu.Coctail, "Mojito", 5);

                chiefCooker.CompleteTheOrder("023");

                chiefCooker.AddProduct("Green salad", 17.63);
                chiefCooker.AddProduct("Tomato", 4.9);
                chiefCooker.AddProduct("Chicken fillet", 7.5);
                chiefCooker.AddProduct("White bread", 2.2);
                chiefCooker.AddProduct("Mayonnaise", 6.42);
                chiefCooker.AddProduct("Butter", 20.28);
                chiefCooker.AddProduct("Garlic", 1.2);
                chiefCooker.AddProduct("Cheese", 16.7);

                chiefCooker.AddProduct("Pork", 10.2);

                chiefCooker.AddProduct("Tonic", 1.5);
                chiefCooker.AddProduct("White rum", 67);
                chiefCooker.AddProduct("Lime", 7.5);
                chiefCooker.AddProduct("Mint", 39.8);
                chiefCooker.AddProduct("Ice", 2);

                Console.WriteLine(manager.ViewAllOrders());
                Console.WriteLine(chiefCooker.ViewAllIngredients());
                Console.WriteLine(chiefCooker.ViewCurrentOrder());
            }
            catch(Exception error)
            {
                Console.WriteLine("Error" + error.Message);
            }
        }
    }
}
