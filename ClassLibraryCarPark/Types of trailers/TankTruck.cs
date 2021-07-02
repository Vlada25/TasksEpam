using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark.Types_of_trailers
{
    public class TankTruck : Semitrailer
    {
        public TankTruck(int number, double maxWeight, double maxVolume)
            : base(number, maxWeight, maxVolume)
        {
            typeOfTrailer = "TankTruck";
        }
        public override void LoadTrailer(Cargo cargo)
        {
            if (!cargo.isLiquid)
            {
                throw new Exception("Tank trucks can only carry liquid");
            }
            if (listOfCargo.Count > 1)
            {
                throw new Exception("Tank trucks can carry only one kind of liquid");
            }
            ChangeWeightAndVolume(cargo.weight, cargo.volume);
            listOfCargo.Add(cargo);
        }
    }
}
