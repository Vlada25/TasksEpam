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
                    Console.WriteLine(trailer.GetTypeOfTrailer() + "\t#" + trailer.number);
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
                    Console.WriteLine(tractor.model + "\t#" + tractor.number);
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
                    Console.WriteLine(trailer.GetTypeOfTrailer() + "\t#" + trailer.number);
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
                        Console.WriteLine(trailer.GetTypeOfTrailer() + "\t#" + trailer.number);
                        noInfo = false;
                    }
                }
                else
                {
                    if (trailer.MaxVolume >= minValue && trailer.MaxVolume <= maxValue)
                    {
                        Console.WriteLine(trailer.GetTypeOfTrailer() + "\t#" + trailer.number);
                        noInfo = false;
                    }
                }
            }
            if (noInfo)
            {
                Console.WriteLine("no info");
            }
        }
    }
}
