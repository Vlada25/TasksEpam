﻿using System;
using System.Collections.Generic;

namespace ClassLibraryCarPark
{
    public abstract class Semitrailer 
    {
        public string number;
        public TruckTractor joinedTractor;
        private readonly double maxWeight;
        private readonly double maxVolume;
        private double freeMass;
        private double freeVolume;
        protected string typeOfTrailer;
        protected static List<string> existNumbers = new List<string>();
        protected List<Cargo> listOfCargo = new List<Cargo>();
        public Semitrailer(double maxWeight, double maxVolume)
        {
            number = GenerateNumber();
            existNumbers.Add(number);
            this.maxVolume = maxVolume;
            this.maxWeight = maxWeight;
            freeMass = maxWeight;
            freeVolume = maxVolume;
        }
        public void UnloadAll()
        {
            freeMass = maxWeight;
            freeVolume = maxVolume;
            listOfCargo.Clear();
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
        public void JoingWithTractor(TruckTractor tractor)
        {
            if (!tractor.isFree)
            {
                throw new Exception("This tractor is already taken");
            }
            if (tractor.carryingCapacity < maxWeight)
            {
                throw new Exception("Carrying capacity of the tractor must be no less than that of the trailer");
            }
            joinedTractor = tractor;
            tractor.isFree = false;
        }
        public void UnhookFromTractor()
        {
            if (joinedTractor == null)
            {
                throw new Exception("The trailer is already free");
            }
            joinedTractor.isFree = true;
            joinedTractor = null;
        }
        protected void ChangeWeightAndVolume(double weight, double volume)
        {
            if (freeMass < weight || freeVolume < volume)
            {
                throw new Exception("Not enough space");
            }
            freeMass -= weight;
            freeVolume -= volume;
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
            string result = "";
            result += $"Trailer #{number}\nType of trailer: {typeOfTrailer}\n" +
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
            if (joinedTractor != null)
            {
                result += $"\nJoined with tractor #{joinedTractor.number}";
            }
            result += "\n";
            return result;
        }
    }
}
