using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark.Types_of_trailers
{
    public class Refrigerator : Semitrailer
    {
        public Refrigerator(double maxWeight, double maxVolume)
            : base(maxWeight, maxVolume)
        {
            typeOfTrailer = TypesOfTrailers.Refrigerator;
        }
        public override void LoadTrailer(Cargo cargo)
        {
            if (cargo.IsLiquid)
            {
                throw new Exception("Refrigerators can't carry liquid");
            }
            if (!cargo.WasTemperatureSet)
            {
                throw new Exception("Temperature wasn't set");
            }
            if (ListOfCargo.Count > 0)
            {
                for (int i = 0; i < ListOfCargo.Count; i++)
                {
                    if (ListOfCargo[i].StartTemperature > cargo.EndTemperature
                        || ListOfCargo[i].EndTemperature < cargo.StartTemperature)
                    {
                        throw new Exception("Mismatch of temperature transportation");
                    }
                }
            }
            ChangeWeightAndVolume(cargo.Weight, cargo.Volume);
            ListOfCargo.Add(cargo);
        }
        public override bool Equals(object obj)
        {
            return obj is Refrigerator refrigerator &&
                   base.Equals(obj) &&
                   Number == refrigerator.Number &&
                   EqualityComparer<TruckTractor>.Default.Equals(JoinedTractor, refrigerator.JoinedTractor) &&
                   typeOfTrailer == refrigerator.typeOfTrailer &&
                   EqualityComparer<List<Cargo>>.Default.Equals(ListOfCargo, refrigerator.ListOfCargo) &&
                   MaxWeight == refrigerator.MaxWeight &&
                   MaxVolume == refrigerator.MaxVolume;
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
