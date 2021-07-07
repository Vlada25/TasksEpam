using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark.Types_of_trailers
{
    public class TiltSemitrailer : Semitrailer
    {
        public TiltSemitrailer(double maxWeight, double maxVolume)
            : base(maxWeight, maxVolume)
        {
            typeOfTrailer = TypesOfTrailers.TiltSemitrailer;
        }
        public override void LoadTrailer(Cargo cargo)
        {
            if (cargo.IsLiquid)
            {
                throw new Exception("Tilt semitrailers can't carry liquid");
            }
            if (cargo.WasTemperatureSet)
            {
                throw new Exception("Special temperature conditions are required");
            }
            if (ListOfCargo.Count > 0)
            {
                for (int i = 0; i < ListOfCargo.Count; i++)
                {
                    if (ListOfCargo[i].TypeOfCargo != cargo.TypeOfCargo)
                    {
                        throw new Exception("Different types of cargo");
                    }
                }
            }
            ChangeWeightAndVolume(cargo.Weight, cargo.Volume);
            ListOfCargo.Add(cargo);
        }
        public override bool Equals(object obj)
        {
            return obj is TiltSemitrailer semitrailer &&
                   base.Equals(obj) &&
                   Number == semitrailer.Number &&
                   EqualityComparer<TruckTractor>.Default.Equals(JoinedTractor, semitrailer.JoinedTractor) &&
                   typeOfTrailer == semitrailer.typeOfTrailer &&
                   EqualityComparer<List<Cargo>>.Default.Equals(ListOfCargo, semitrailer.ListOfCargo) &&
                   MaxWeight == semitrailer.MaxWeight &&
                   MaxVolume == semitrailer.MaxVolume;
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
