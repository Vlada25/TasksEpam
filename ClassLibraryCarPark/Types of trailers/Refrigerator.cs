using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark.Types_of_trailers
{
    public class Refrigerator : Semitrailer, ITrailer
    {
        public Refrigerator(double maxWeight, double maxVolume)
            : base(maxWeight, maxVolume)
        {
            typeOfTrailer = TypesOfTrailers.Refrigerator;
        }

        public override bool Equals(object obj)
        {
            return obj is Refrigerator refrigerator &&
                   base.Equals(obj) &&
                   number == refrigerator.number &&
                   EqualityComparer<TruckTractor>.Default.Equals(joinedTractor, refrigerator.joinedTractor) &&
                   typeOfTrailer == refrigerator.typeOfTrailer &&
                   EqualityComparer<List<Cargo>>.Default.Equals(listOfCargo, refrigerator.listOfCargo) &&
                   MaxWeight == refrigerator.MaxWeight &&
                   MaxVolume == refrigerator.MaxVolume;
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
