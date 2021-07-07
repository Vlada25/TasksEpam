using ClassLibraryCarPark;
using System;
using System.Collections.Generic;
using System.Text;

namespace Execution
{
    class Service
    {
        public enum WeightOrVolume
        {
            Weight,
            Volume
        }
        public static void ViewCarPark(List<Semitrailer> listOfTrailers, List<TruckTractor> listOfTractors)
        {
            Console.WriteLine("\nTrailers:");
            if (listOfTrailers.Count == 0)
            {
                Console.WriteLine("no info");
            }
            else
            {
                foreach (Semitrailer trailer in listOfTrailers)
                {
                    Console.WriteLine(trailer.GetTypeOfTrailer() + "\t#" + trailer.Number);
                }
            }
            
            Console.WriteLine("\nTractors:");
            if (listOfTractors.Count == 0)
            {
                Console.WriteLine("no info");
            }
            else
            {
                foreach (TruckTractor tractor in listOfTractors)
                {
                    Console.WriteLine(tractor.model + "\t#" + tractor.Number);
                }
            }
        }
        public static void FindSemitrailer(List<Semitrailer> listOfTrailers, Semitrailer.TypesOfTrailers typeOfTrailer)
        {
            bool noInfo = true;
            Console.WriteLine("\nType: " + typeOfTrailer);
            foreach (Semitrailer trailer in listOfTrailers)
            {
                if (typeOfTrailer == trailer.GetTypeOfTrailer())
                {
                    Console.WriteLine(trailer.GetTypeOfTrailer() + "\t#" + trailer.Number);
                    noInfo = false;
                }
            }
            if (noInfo)
            {
                Console.WriteLine("no info");
            }
        }
        public static void FindSemitrailer(List<Semitrailer> listOfTrailers, double minValue, double maxValue, WeightOrVolume value)
        {
            bool noInfo = true;
            Console.WriteLine("\n" + value + " : " + minValue + " - " + maxValue);
            foreach (Semitrailer trailer in listOfTrailers)
            {
                if (value == WeightOrVolume.Weight)
                {
                    if (trailer.MaxWeight >= minValue && trailer.MaxWeight <= maxValue)
                    {
                        Console.WriteLine(trailer.GetTypeOfTrailer() + "\t#" + trailer.Number);
                        noInfo = false;
                    }
                }
                else
                {
                    if (trailer.MaxVolume >= minValue && trailer.MaxVolume <= maxValue)
                    {
                        Console.WriteLine(trailer.GetTypeOfTrailer() + "\t#" + trailer.Number);
                        noInfo = false;
                    }
                }
            }
            if (noInfo)
            {
                Console.WriteLine("no info");
            }
        }
        public static void FindCouplingsByCargo(List<Semitrailer> listOfTrailers, Cargo.CargoTypes cargoType)
        {
            bool noInfo = true;
            Console.WriteLine("\nType of cargo: " + cargoType);
            foreach (Semitrailer trailer in listOfTrailers)
            {
                bool isThereDesiredType = false;
                foreach (Cargo cargo in trailer.ListOfCargo)
                {
                    if (cargo.TypeOfCargo == cargoType)
                    {
                        isThereDesiredType = true;
                        break;
                    }
                }
                if (isThereDesiredType && trailer.JoinedTractor != null)
                {
                    Console.WriteLine(trailer.GetTypeOfTrailer() + " #" + trailer.Number + 
                        " joined with tractor " + trailer.JoinedTractor.model + " #" + trailer.JoinedTractor.Number);
                    noInfo = false;
                }
            }
            if (noInfo)
            {
                Console.WriteLine("no info");
            }
        }
        public static void FindCouplingWithAnyFreeSpace(List<Semitrailer> listOfTrailers)
        {
            bool noInfo = true;
            foreach (Semitrailer trailer in listOfTrailers)
            {
                if (trailer.JoinedTractor != null && trailer.JoinedTractor.carryingCapacity > trailer.GetWeihgtOfAllCargo())
                {
                    Console.WriteLine("\n" + trailer.GetTypeOfTrailer() + " #" + trailer.Number +
                        " joined with tractor " + trailer.JoinedTractor.model + " #" + trailer.JoinedTractor.Number);
                    noInfo = false;
                }
            }
            if (noInfo)
            {
                Console.WriteLine("no info");
            }
        }
    }
}
