using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark.Types_of_trailers
{
    public class TankTruck : Semitrailer, ITrailer
    {
        public TankTruck(double maxWeight, double maxVolume)
            : base(maxWeight, maxVolume)
        {
            typeOfTrailer = TypesOfTrailers.TankTruck;
        }
        public void LoadTrailer(Cargo cargo)
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
