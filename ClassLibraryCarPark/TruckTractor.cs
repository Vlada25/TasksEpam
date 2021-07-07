using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark
{
    public class TruckTractor
    {
        public string Number;
        private readonly List<string> existNumbers = new List<string>();
        public string model;
        public double fuelConsumption;
        public double carryingCapacity;
        public bool isFree;
        public TruckTractor(string model, double fuelConsumption, double carryingCapacity)
        {
            Number = GenerateNumber();
            existNumbers.Add(Number);
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
                throw new Exception("This Number is already exist");
            }
            return res;
        }
        public override string ToString()
        {
            return $"Tractor #{Number}\nModel: {model}\nCarrying capacity: {carryingCapacity}\n";
        }

        public override bool Equals(object obj)
        {
            return obj is TruckTractor tractor &&
                   Number == tractor.Number &&
                   EqualityComparer<List<string>>.Default.Equals(existNumbers, tractor.existNumbers) &&
                   model == tractor.model &&
                   fuelConsumption == tractor.fuelConsumption &&
                   carryingCapacity == tractor.carryingCapacity &&
                   isFree == tractor.isFree;
        }

        public override int GetHashCode()
        {
            int hashCode = 1098702807;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Number);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(existNumbers);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(model);
            hashCode = hashCode * -1521134295 + fuelConsumption.GetHashCode();
            hashCode = hashCode * -1521134295 + carryingCapacity.GetHashCode();
            hashCode = hashCode * -1521134295 + isFree.GetHashCode();
            return hashCode;
        }
    }
}
