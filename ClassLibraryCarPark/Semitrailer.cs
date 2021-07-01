using System;
using System.Collections.Generic;

namespace ClassLibraryCarPark
{
    public abstract class Semitrailer
    {
        public int trailerNumber;
        public double maxWeight;
        public double maxVolume;
        protected string typeOfTrailer;
        protected static List<int> existNumbers = new List<int>();
        protected List<Cargo> listOfCargo = new List<Cargo>();
        public Semitrailer(int number, double maxWeight, double maxVolume)
        {
            trailerNumber = SetNumber(number);
            this.maxVolume = maxVolume;
            this.maxWeight = maxWeight;
        }
        public void LoadTrailer(Cargo cargo)
        {
            listOfCargo.Add(cargo);
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
            result += "\n";
            return result;
        }
    }
}
