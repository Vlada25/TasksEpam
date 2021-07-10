using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark
{
    public class TruckTractor
    {
        public string Number;
        private readonly List<string> existNumbers = new List<string>();
        public string Model;
        private readonly double _fuelConsumption;
        public double ExtraWeight;
        public double CarryingCapacity;
        public bool IsFree;
        public TruckTractor(string model, double fuelConsumption, double carryingCapacity)
        {
            Number = GenerateNumber();
            existNumbers.Add(Number);
            Model = model;
            _fuelConsumption = fuelConsumption;
            CarryingCapacity = carryingCapacity;
            IsFree = true;
            ExtraWeight = 0;
        }

        /// <summary>
        /// Counting of the fuel consumption (given the weight of the trailer, if any)
        /// </summary>
        /// <returns> Fuel consumption </returns>
        public double CountFuelConsumption()
        {
            return _fuelConsumption + ExtraWeight * 1.3;
        }

        /// <summary>
        /// Generation of tractor's number
        /// </summary>
        /// <returns> Number of tractor </returns>
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
            return $"Tractor #{Number}\nModel: {Model}\nCarrying capacity: {CarryingCapacity}\n";
        }

        public override bool Equals(object obj)
        {
            return obj is TruckTractor tractor &&
                   Number == tractor.Number &&
                   EqualityComparer<List<string>>.Default.Equals(existNumbers, tractor.existNumbers) &&
                   Model == tractor.Model &&
                   _fuelConsumption == tractor._fuelConsumption &&
                   CarryingCapacity == tractor.CarryingCapacity &&
                   IsFree == tractor.IsFree;
        }

        public override int GetHashCode()
        {
            int hashCode = 1098702807;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Number);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(existNumbers);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Model);
            hashCode = hashCode * -1521134295 + _fuelConsumption.GetHashCode();
            hashCode = hashCode * -1521134295 + CarryingCapacity.GetHashCode();
            hashCode = hashCode * -1521134295 + IsFree.GetHashCode();
            return hashCode;
        }
    }
}
