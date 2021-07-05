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
            typeOfTrailer = TypesOfTrailers.TiltSemitrailer;
        }

        public override bool Equals(object obj)
        {
            return obj is TiltSemitrailer semitrailer &&
                   base.Equals(obj) &&
                   number == semitrailer.number &&
                   EqualityComparer<TruckTractor>.Default.Equals(joinedTractor, semitrailer.joinedTractor) &&
                   typeOfTrailer == semitrailer.typeOfTrailer &&
                   EqualityComparer<List<Cargo>>.Default.Equals(listOfCargo, semitrailer.listOfCargo) &&
                   MaxWeight == semitrailer.MaxWeight &&
                   MaxVolume == semitrailer.MaxVolume;
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
