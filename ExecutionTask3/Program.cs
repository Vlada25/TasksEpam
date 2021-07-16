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
                manager.TakeOrder("023", Manager.Menu.Salad, "Caesar", 2, "10:23");
                manager.AddDishToOrder("023", Manager.Menu.Coctail, "Mojito", 2);
                Console.WriteLine(manager.ViewAllOrders());
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
    }
}
