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
                ChiefCooker cook = new ChiefCooker();

                manager.TakeOrder("023", Manager.Menu.Salad, "Caesar", 2, "10:23");
                manager.AddDishToOrder("023", Manager.Menu.Coctail, "Mojito", 2);

                //Product p1 = new Product("Chicken fillet", 7.5);
                //Product p2 = new Product("White bread", 2.36);

                cook.CompleteTheOrder("023", "Caesar");
                cook.AddIngredient("Chicken fillet", 7.5, 100.0);

                Console.WriteLine(manager.ViewAllOrders());
                Console.WriteLine(cook.ViewAllIngredients());
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
    }
}
