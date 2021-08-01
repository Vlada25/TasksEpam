using ClassLibraryBistro;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestTask3
{
    [TestClass]
    public class UnitTestClientOrder
    {
        [TestMethod]
        public void AddDish_NumberOfDishes_Return2()
        {
            ClientOrder clientOrder = new ClientOrder("023", Manager.Menu.Salad, "Caesar", 8, "10:23");
            clientOrder.AddDish(Manager.Menu.Meat, "Steak", 16);

            int result = clientOrder.Dishes.Count;
            int expected = 2;

            Assert.AreEqual(result, expected);
        }
    }
}
