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
            if (!cargo.IsLiquid)
            {
                throw new Exception("Tank trucks can only carry liquid");
            }
            if (ListOfCargo.Count > 1)
            {
                throw new Exception("Tank trucks can carry only one kind of liquid");
            }
            ChangeWeightAndVolume(cargo.Weight, cargo.Volume);
            ListOfCargo.Add(cargo);
        }
        public override bool Equals(object obj)
        {
            return obj is TankTruck truck &&
                   base.Equals(obj) &&
                   Number == truck.Number &&
                   EqualityComparer<TruckTractor>.Default.Equals(JoinedTractor, truck.JoinedTractor) &&
                   typeOfTrailer == truck.typeOfTrailer &&
                   EqualityComparer<List<Cargo>>.Default.Equals(ListOfCargo, truck.ListOfCargo) &&
                   MaxWeight == truck.MaxWeight &&
                   MaxVolume == truck.MaxVolume;
        }

        public override int GetHashCode()
        {
            int hashCode = -779872140;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Number);
            hashCode = hashCode * -1521134295 + EqualityComparer<TruckTractor>.Default.GetHashCode(JoinedTractor);
            hashCode = hashCode * -1521134295 + typeOfTrailer.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Cargo>>.Default.GetHashCode(ListOfCargo);
            hashCode = hashCode * -1521134295 + MaxWeight.GetHashCode();
            hashCode = hashCode * -1521134295 + MaxVolume.GetHashCode();
            return hashCode;
        }  
    }
}
