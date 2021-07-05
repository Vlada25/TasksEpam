using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark
{
    public class TruckTractor
    {
        public string number;
        private readonly List<string> existNumbers = new List<string>();
        public string model;
        public double fuelConsumption;
        public double carryingCapacity;
        public bool isFree;
        public TruckTractor(string model, double fuelConsumption, double carryingCapacity)
        {
            number = GenerateNumber();
            existNumbers.Add(number);
            this.model = model;
            this.fuelConsumption = fuelConsumption;
            this.carryingCapacity = carryingCapacity;
            isFree = true;
        }
        public string GenerateNumber()
        {
            string res;
            Random rand = new Random();
            int x = rand.Next(0, 10000);
            if (x < 10) 
            {
                res = "000" + x;
            }
            else if (x < 100)
            {
                res = "00" + x;
            }
            else if (x < 1000)
            {
                res = "0" + x;
            }
            else
            {
                res = x + "";
            }
            if (existNumbers.Contains(res))
            {
                throw new Exception("This number is already exist");
            }
            return res;
        }
        public override string ToString()
        {
            return $"Tractor #{number}\nModel: {model}\nCarrying capacity: {carryingCapacity}\n";
        }
    }
}
