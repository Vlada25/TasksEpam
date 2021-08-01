using ClassLibraryBistro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestTask3
{
    [TestClass]
    public class UnitTestManager
    {
        
        [TestMethod]
        public void TakeOrder_ClientNumber_ReturnSameNumber()
        {
            Manager manager = new Manager();
            manager.TakeOrder("023", Manager.Menu.Salad, "Caesar", 8, "10:23");

            string result = Manager.ClientOrdersList[0].ClientNumber;
            string expected = "023";

            Assert.AreEqual(result, expected);
        }
        [TestMethod]
        public void AddDishToOrder_NumberOfDishes_Return2()
        {
            Manager manager = new Manager();
            manager.TakeOrder("023", Manager.Menu.Salad, "Caesar", 8, "10:23");
            manager.AddDishToOrder("023", Manager.Menu.Meat, "Steak", 16);

            int result = Manager.ClientOrdersList[0].Dishes.Count;
            int expected = 2;

            Assert.AreEqual(result, expected);
        }
    }
}
