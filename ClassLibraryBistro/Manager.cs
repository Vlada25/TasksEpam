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

        /// <summary>
        /// Manager accepts the customer's order 
        /// </summary>
        /// <param name="clientNumber"> Number of client </param>
        /// <param name="Type"> Type of dish </param>
        /// <param name="dishName"> Name of dish </param>
        /// <param name="countOfPortions"> Count of portions </param>
        /// <param name="orderTime"> What time was the order </param>
        public void TakeOrder(string clientNumber, Menu Type, string dishName, int countOfPortions, string orderTime)
        {
            ClientOrder clientOrder = new ClientOrder(clientNumber, Type, dishName, countOfPortions, orderTime);
            ClientOrdersList.Add(clientOrder);
        }

        /// <summary>
        /// Adding one more dish to the order
        /// </summary>
        /// <param name="clientNumber"> Number of client </param>
        /// <param name="Type"> Type of dish </param>
        /// <param name="dishName"> Name of dish </param>
        /// <param name="countOfPortions"> Count of portions </param>
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

        /// <summary>
        /// View of orders made in a selected period of time
        /// </summary>
        /// <param name="startTime"> Start of time span </param>
        /// <param name="endTime"> End of time span </param>
        /// <returns> Matching orders </returns>
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
