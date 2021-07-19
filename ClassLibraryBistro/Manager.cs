using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryBistro
{
    public class Manager
    {
        public enum Menu
        {
            Salad,
            Coctail
        }
        private static bool _alreadyExist = false;
        public static List<ClientOrder> ClientOrdersList = new List<ClientOrder>();
        public Manager()
        {
            if (_alreadyExist)
            {
                throw new Exception("There can only be one manager");
            }
            _alreadyExist = true;
        }
        public void TakeOrder(string clientNumber, Menu dishType, string dishName, int countOfPortions, string orderTime)
        {
            ClientOrder clientOrder = new ClientOrder(clientNumber, dishType, dishName, countOfPortions, orderTime);
            ClientOrdersList.Add(clientOrder);
        }
        public void AddDishToOrder(string clientNumber, Menu dishType, string dishName, int countOfPortions)
        {
            bool isOrderExist = false;
            foreach (ClientOrder order in ClientOrdersList)
            {
                if (order.ClientNumber.Equals(clientNumber))
                {
                    isOrderExist = true;
                    order.AddDish(dishType, dishName, countOfPortions);
                    break;
                }
            }
            if (!isOrderExist)
            {
                throw new Exception("This number is not in the order list");
            }
        }
        public string ViewAllOrders()
        {
            string result = "";
            foreach (ClientOrder order in ClientOrdersList)
            {
                result += order.ToString();
                result += $"\nIs order ready: ";
                result += order.IsDone ? "Yes" : "No";
            }
            return result;
        }
    }
}
