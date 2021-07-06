using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark.Types_of_trailers
{
    public class TankTruck : Semitrailer
    {
        public TankTruck(double maxWeight, double maxVolume)
            : base(maxWeight, maxVolume)
        {
            typeOfTrailer = TypesOfTrailers.TankTruck;
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
        public override bool Equals(object obj)
        {
            return obj is TankTruck truck &&
                   base.Equals(obj) &&
                   number == truck.number &&
                   EqualityComparer<TruckTractor>.Default.Equals(joinedTractor, truck.joinedTractor) &&
                   typeOfTrailer == truck.typeOfTrailer &&
                   EqualityComparer<List<Cargo>>.Default.Equals(listOfCargo, truck.listOfCargo) &&
                   MaxWeight == truck.MaxWeight &&
                   MaxVolume == truck.MaxVolume;
        }

        public override int GetHashCode()
        {
            int hashCode = -779872140;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(number);
            hashCode = hashCode * -1521134295 + EqualityComparer<TruckTractor>.Default.GetHashCode(joinedTractor);
            hashCode = hashCode * -1521134295 + typeOfTrailer.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Cargo>>.Default.GetHashCode(listOfCargo);
            hashCode = hashCode * -1521134295 + MaxWeight.GetHashCode();
            hashCode = hashCode * -1521134295 + MaxVolume.GetHashCode();
            return hashCode;
        }  
    }
}
