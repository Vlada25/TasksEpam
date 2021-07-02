using System;
using System.Collections.Generic;

namespace ClassLibraryCarPark
{
    public abstract class Semitrailer 
    {
        public int trailerNumber;
        public double maxWeight;
        public double maxVolume;
        private double freeMass;
        private double freeVolume;
        protected string typeOfTrailer;
        protected static List<int> existNumbers = new List<int>();
        protected List<Cargo> listOfCargo = new List<Cargo>();
        public Semitrailer(int number, double maxWeight, double maxVolume)
        {
            trailerNumber = SetNumber(number);
            this.maxVolume = maxVolume;
            this.maxWeight = maxWeight;
            freeMass = maxWeight;
            freeVolume = maxVolume;
        }
        public void UnloadAll()
        {
            freeMass = maxWeight;
            freeVolume = maxVolume;
            listOfCargo = new List<Cargo>();
        }
        public void UnloadTrailer(Cargo cargo)
        {
            if (!listOfCargo.Contains(cargo))
            {
                throw new Exception("There is no such load in this trailer");
            }
            listOfCargo.Remove(cargo);
            freeMass += cargo.weight;
            freeVolume += cargo.volume;
        }
        public void UnloadTrailer(Cargo cargo, int percentOfCargo)
        {
            if (!listOfCargo.Contains(cargo))
            {
                throw new Exception("There is no such load in this trailer");
            }
            if (percentOfCargo > 100)
            {
                throw new Exception("Percentage can't be > 100");
            }
            else if (percentOfCargo == 100)
            {
                listOfCargo.Remove(cargo);
            }
            freeMass += cargo.weight * percentOfCargo / 100;
            freeVolume += cargo.volume * percentOfCargo / 100;
        }
        public void ChangeWeightAndVolume(double weight, double volume)
        {
            if (freeMass < weight || freeVolume < volume)
            {
                throw new Exception("Not enough space");
            }
            freeMass -= weight;
            freeVolume -= volume;
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
            result += $"\nFree space:\n\tweight: {Math.Round(freeMass / maxWeight * 100, 2)}%" 
                + $"\n\tvolume: {Math.Round(freeVolume / maxVolume * 100, 2)}%";
            result += "\n";
            return result;
        }
    }
}
