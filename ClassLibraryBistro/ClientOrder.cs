using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryBistro
{
    public class ClientOrder
    {
        public string ClientNumber { get; }
        double finalBill = 0;
        string orderTime;
        public bool IsDone = false;
        public List<Dish> Dishes = new List<Dish>();
        public List<int> ContOfPortionsList = new List<int>();
        public ClientOrder(string clientNumber, Manager.Menu dishType, string dishName, int countOfPortions, string orderTime)
        {
            ClientNumber = SetClientNumber(clientNumber);
            this.orderTime = SetTime(orderTime);
            Dishes.Add(new Dish(dishName, dishType));
            ContOfPortionsList.Add(countOfPortions);
        }
        public void AddDish(Manager.Menu dishType, string dishName, int countOfPortions)
        {
            Dishes.Add(new Dish(dishName, dishType));
            ContOfPortionsList.Add(countOfPortions);
        }
        string SetClientNumber(string number)
        {
            Exception ex = new Exception("Invalid client number");
            if (number.Length != 3)
            {
                throw ex;
            }
            else if (!Int32.TryParse(number, out int _))
            {
                throw ex;
            }
            return number;
        }
        private string SetTime(string time)
        {
            Exception ex = new Exception("Invalid value of time");
            if (time.Length != 5)
            {
                throw ex;
            }
            else if (!Int32.TryParse(Convert.ToString(time[0]) + Convert.ToString(time[1]), out int hours)
                    || !Int32.TryParse(Convert.ToString(time[3]) + Convert.ToString(time[4]), out int minutes))
            {
                throw ex;
            }
            else if (hours < 9 || hours > 22 || minutes < 0 || minutes > 59)
            {
                throw ex;
            }
            return time;
        }
        public override string ToString()
        {
            string result = "";
            result += $"\nClient #{ClientNumber} made an order at {orderTime}";
            for(int i = 0; i < ContOfPortionsList.Count; i++)
            {
                result += $"\n{Dishes[i]} - {ContOfPortionsList[i]} - ";
                result += Dishes[i].IsDishDone ? "done" : "not done";
            }
            result += $"\nFinal bill: {finalBill}$";
            return result;
        }
    }
}
