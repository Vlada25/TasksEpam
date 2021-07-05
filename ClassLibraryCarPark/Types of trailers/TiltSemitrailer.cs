using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark.Types_of_trailers
{
    public class TiltSemitrailer : Semitrailer, ITrailer
    {
        public TiltSemitrailer(double maxWeight, double maxVolume)
            : base(maxWeight, maxVolume)
        {
            typeOfTrailer = "TiltSemitrailer";
        }

        public void LoadTrailer(Cargo cargo)
        {
            if (cargo.isLiquid)
            {
                throw new Exception("Tilt semitrailers can't carry liquid");
            }
            if (cargo.wasTemperatureSet)
            {
                throw new Exception("Special temperature conditions are required");
            }
            if (listOfCargo.Count > 0)
            {
                for (int i = 0; i < listOfCargo.Count; i++)
                {
                    if (listOfCargo[i].TypeOfCargo != cargo.TypeOfCargo)
                    {
                        throw new Exception("Different types of cargo");
                    }
                }
            }
            ChangeWeightAndVolume(cargo.weight, cargo.volume);
            listOfCargo.Add(cargo);
        }
    }
}
