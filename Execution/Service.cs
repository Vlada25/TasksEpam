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

        /// <summary>
        /// Allows to view all trailers and tractors
        /// </summary>
        /// <param name="listOfTrailers"> All trailers </param>
        /// <param name="listOfTractors"> All tractors </param>
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
                    Console.WriteLine(tractor.Model + "\t#" + tractor.Number);
                }
            }
        }

        /// <summary>
        /// Looking for a trailer by type
        /// </summary>
        /// <param name="listOfTrailers"> All trailers </param>
        /// <param name="typeOfTrailer"> Selected type of trailer </param>
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

        /// <summary>
        /// Looking for trailer by mass or volume (in range)
        /// </summary>
        /// <param name="listOfTrailers"> All trailers </param>
        /// <param name="minValue"> Start value </param>
        /// <param name="maxValue"> End value </param>
        /// <param name="value"> Mass or volume </param>
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

        /// <summary>
        /// Search for hitch depending on the transported cargo
        /// </summary>
        /// <param name="listOfTrailers"> All trailers </param>
        /// <param name="cargoType"> Type of cargo </param>
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
                        " joined with tractor " + trailer.JoinedTractor.Model + " #" + trailer.JoinedTractor.Number);
                    noInfo = false;
                }
            }
            if (noInfo)
            {
                Console.WriteLine("no info");
            }
        }

        /// <summary>
        /// Searching for hitch where there is any free space in semitrailer
        /// </summary>
        /// <param name="listOfTrailers"> All trailers </param>
        public static void FindCouplingWithAnyFreeSpace(List<Semitrailer> listOfTrailers)
        {
            bool noInfo = true;
            foreach (Semitrailer trailer in listOfTrailers)
            {
                if (trailer.JoinedTractor != null && trailer.JoinedTractor.CarryingCapacity > trailer.GetWeihgtOfAllCargo())
                {
                    Console.WriteLine("\n" + trailer.GetTypeOfTrailer() + " #" + trailer.Number +
                        " joined with tractor " + trailer.JoinedTractor.Model + " #" + trailer.JoinedTractor.Number);
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
