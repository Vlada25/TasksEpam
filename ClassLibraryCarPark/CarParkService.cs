using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark
{
    public class CarParkService
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
        /// <returns> Some information about all trailers and tractors </returns>
        public static string ViewCarPark(List<Semitrailer> listOfTrailers, List<TruckTractor> listOfTractors)
        {
            string result = "";
            result += "\nTrailers:\n";
            if (listOfTrailers.Count == 0)
            {
                result += "no info";
            }
            else
            {
                foreach (Semitrailer trailer in listOfTrailers)
                {
                    result += $"{trailer.GetTypeOfTrailer()} \t# {trailer.Number}\n";
                }
            }

            result += "\nTractors:\n";
            if (listOfTractors.Count == 0)
            {
                result += "no info";
            }
            else
            {
                foreach (TruckTractor tractor in listOfTractors)
                {
                    result += $"{tractor.Model} \t# {tractor.Number}\n";
                }
            }
            return result;
        }

        /// <summary>
        /// Looking for a trailer by type
        /// </summary>
        /// <param name="listOfTrailers"> All trailers </param>
        /// <param name="typeOfTrailer"> Selected type of trailer </param>
        /// <returns> Some information about appropriate trailers </returns>
        public static string FindSemitrailer(List<Semitrailer> listOfTrailers, Semitrailer.TypesOfTrailers typeOfTrailer)
        {
            string result = "";
            bool noInfo = true;
            result += $"\nType: {typeOfTrailer}\n";
            foreach (Semitrailer trailer in listOfTrailers)
            {
                if (typeOfTrailer == trailer.GetTypeOfTrailer())
                {
                    result += $"{trailer.GetTypeOfTrailer()} \t# {trailer.Number}\n";
                    noInfo = false;
                }
            }
            if (noInfo)
            {
                result += "no info";
            }
            return result;
        }

        /// <summary>
        /// Looking for trailer by mass or volume (in range)
        /// </summary>
        /// <param name="listOfTrailers"> All trailers </param>
        /// <param name="minValue"> Start value </param>
        /// <param name="maxValue"> End value </param>
        /// <param name="value"> Mass or volume </param>
        /// <returns> Some information about appropriate trailers </returns>
        public static string FindSemitrailer(List<Semitrailer> listOfTrailers, double minValue, double maxValue, WeightOrVolume value)
        {
            string result = "";
            bool noInfo = true;
            result += $"\n{value} : {minValue} - {maxValue}\n";
            foreach (Semitrailer trailer in listOfTrailers)
            {
                if (value == WeightOrVolume.Weight)
                {
                    if (trailer.MaxWeight >= minValue && trailer.MaxWeight <= maxValue)
                    {
                        result += $"{trailer.GetTypeOfTrailer()} \t# {trailer.Number}\n";
                        noInfo = false;
                    }
                }
                else
                {
                    if (trailer.MaxVolume >= minValue && trailer.MaxVolume <= maxValue)
                    {
                        result += $"{trailer.GetTypeOfTrailer()} \t# {trailer.Number}\n";
                        noInfo = false;
                    }
                }
            }
            if (noInfo)
            {
                result += "no info";
            }
            return result;
        }

        /// <summary>
        /// Search for hitch depending on the transported cargo
        /// </summary>
        /// <param name="listOfTrailers"> All trailers </param>
        /// <param name="cargoType"> Type of cargo </param>
        /// <returns> Some information about appropriate couplings </returns>
        public static string FindCouplingsByCargo(List<Semitrailer> listOfTrailers, Cargo.CargoTypes cargoType)
        {
            string result = "";
            bool noInfo = true;
            result += $"\nType of cargo: {cargoType}\n";
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
                    result += $"{trailer.GetTypeOfTrailer()} #{trailer.Number}" +
                        $" joined with tractor {trailer.JoinedTractor.Model} #{trailer.JoinedTractor.Number}";
                    noInfo = false;
                }
            }
            if (noInfo)
            {
                result += "no info";
            }
            return result;
        }

        /// <summary>
        /// Searching for hitch where there is any free space in semitrailer
        /// </summary>
        /// <param name="listOfTrailers"> All trailers </param>
        /// <returns> Some information about appropriate couplings </returns>
        public static string FindCouplingWithAnyFreeSpace(List<Semitrailer> listOfTrailers)
        {
            string result = "";
            bool noInfo = true;
            foreach (Semitrailer trailer in listOfTrailers)
            {
                if (trailer.JoinedTractor != null && trailer.JoinedTractor.CarryingCapacity > trailer.GetWeihgtOfAllCargo())
                {
                    result += $"\n{trailer.GetTypeOfTrailer()} #{trailer.Number}" +
                        $" joined with tractor {trailer.JoinedTractor.Model} #{trailer.JoinedTractor.Number}";
                    noInfo = false;
                }
            }
            if (noInfo)
            {
                result += "no info";
            }
            return result;
        }
    }
}
