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
            Meat,
            Coctail,
            Soup,
            Dessert,
            HotDrink
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
        public void TakeOrder(string clientNumber, Menu Type, string dishName, int countOfPortions, string orderTime)
        {
            ClientOrder clientOrder = new ClientOrder(clientNumber, Type, dishName, countOfPortions, orderTime);
            ClientOrdersList.Add(clientOrder);
        }
        public void AddDishToOrder(string clientNumber, Menu Type, string dishName, int countOfPortions)
        {
            bool isOrderExist = false;
            foreach (ClientOrder order in ClientOrdersList)
            {
                if (order.ClientNumber.Equals(clientNumber))
                {
                    if (order.OrderInProgress)
                    {
                        throw new Exception("This order is in progress, you can't add new dishes");
                    }
                    isOrderExist = true;
                    order.AddDish(Type, dishName, countOfPortions);
                    break;
                }
            }
            if (!isOrderExist)
            {
                throw new Exception("This number is not in the order list");
            }
        }
        public string ViewOrdersInTime(string startTime, string endTime)
        {
            string result = "";
            foreach (ClientOrder order in ClientOrdersList)
            {
                if (Helper.IsTimeInRange(order.OrderTime, startTime, endTime))
                {
                    result += order.ToString();
                    result += $"\nIs order ready: ";
                    result += order.IsDone ? "Yes" : "No";
                    result += "\n";
                }
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
