using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryBistro
{
    public class ClientOrder
    {
        public string ClientNumber { get; }
        public double FinalBill = 0;
        public string OrderTime;
        public int SpentMinutes = 0;
        public bool IsDone = false;
        public bool OrderInProgress = false;
        public List<Dish> Dishes = new List<Dish>();
        public ClientOrder(string clientNumber, Manager.Menu dishType, string dishName, int countOfPortions, string orderTime)
        {
            ClientNumber = SetClientNumber(clientNumber);
            OrderTime = Helper.SetTime(orderTime);
            Dishes.Add(new Dish(dishName, dishType, countOfPortions));
        }
        public void AddDish(Manager.Menu dishType, string dishName, int countOfPortions)
        {
            Dishes.Add(new Dish(dishName, dishType, countOfPortions));
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
        public override string ToString()
        {
            string result = "";
            result += $"\nClient #{ClientNumber} made an order at {OrderTime}";
            for(int i = 0; i < Dishes.Count; i++)
            {
                result += $"\n{Dishes[i]} - ";
                result += Dishes[i].IsDishDone ? "done" : "not done";
            }
            if (IsDone)
            {
                result += $"\nFinal bill: {Math.Round(FinalBill, 2)}$";
                result += $"\nSpent minutes: {SpentMinutes}"; 
            }
            return result;
        }
    }
}
