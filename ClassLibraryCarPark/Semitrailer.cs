using System;
using System.Collections.Generic;

namespace ClassLibraryCarPark
{
    public abstract class Semitrailer
    {
        public int trailerNumber;
        public double maxWeight;
        public double maxVolume;
        public double FreeMass { get; set; }
        public double FreeVolume { get; set; }
        protected string typeOfTrailer;
        protected static List<int> existNumbers = new List<int>();
        protected List<Cargo> listOfCargo = new List<Cargo>();
        public Semitrailer(int number, double maxWeight, double maxVolume)
        {
            trailerNumber = SetNumber(number);
            this.maxVolume = maxVolume;
            this.maxWeight = maxWeight;
            FreeMass = maxWeight;
            FreeVolume = maxVolume;
        }
        public abstract void LoadTrailer(Cargo cargo);
        public void ChangeWeightAndVolume(double weight, double volume)
        {
            if (FreeMass < weight || FreeVolume < volume)
            {
                throw new Exception("Not enough space");
            }
            FreeMass -= weight;
            FreeVolume -= volume;
        }
        public int SetNumber(int number)
        {
            if (number < 1000 || number > 9999)
            {
                throw new Exception("Invalid number of trailer");
            }
            if (existNumbers.Contains(number))
            {
                throw new Exception("Trailer with the same number already exists");
            }
            else
            {
                existNumbers.Add(number);
            }
            return number;
        }
        public override string ToString()
        {
            string result = "";
            result += $"Trailer #{trailerNumber}\nType of trailer: {typeOfTrailer}\n" +
                $"Carrying capacity: {maxWeight}\nMaximum volume: {maxVolume}\nCargo: ";
            if (listOfCargo.Count == 0)
            {
                result += "trailer is empty";
            }
            else
            {
                for (int i = 0; i < listOfCargo.Count; i++)
                {
                    result += listOfCargo[i].ToString();
                }
            }
            result += $"\nFree space:\n\tweight: {Math.Round(FreeMass / maxWeight * 100, 2)}%" 
                + $"\n\tvolume: {Math.Round(FreeVolume / maxVolume * 100, 2)}%";
            result += "\n";
            return result;
        }
    }
}
