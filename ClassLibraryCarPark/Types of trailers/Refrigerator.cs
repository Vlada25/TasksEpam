using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark.Types_of_trailers
{
    public class Refrigerator : Semitrailer, ITrailer
    {
        public Refrigerator(int number, double maxWeight, double maxVolume)
            : base(number, maxWeight, maxVolume)
        {
            typeOfTrailer = "Refrigerator";
        }
        public void LoadTrailer(Cargo cargo)
        {
            if (cargo.isLiquid)
            {
                throw new Exception("Refrigerators can't carry liquid");
            }
            if (!cargo.wasTemperatureSet)
            {
                throw new Exception("Temperature wasn't set");
            }
            if (listOfCargo.Count > 0)
            {
                for (int i = 0; i < listOfCargo.Count; i++)
                {
                    if (listOfCargo[i].StartTemperature > cargo.EndTemperature
                        || listOfCargo[i].EndTemperature < cargo.StartTemperature)
                    {
                        throw new Exception("Mismatch of temperature transportation");
                    }
                }
            }
            ChangeWeightAndVolume(cargo.weight, cargo.volume);
            listOfCargo.Add(cargo);
        }
    }
}
