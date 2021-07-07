using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark
{
    public class Cargo
    {
        public enum CargoTypes
        {
            Product,
            Chemicals,
            ConstructionMaterials,
            Fuel
        }
        public double Weight;
        public double Volume;
        public string Name;
        public bool IsLiquid;
        public CargoTypes TypeOfCargo { get;}
        public int StartTemperature { get; }
        public int EndTemperature { get; }
        public bool WasTemperatureSet = false;
        public Cargo(string name, double weight, double volume, bool isLiquid, CargoTypes type)
        {
            Name = name;
            Weight = weight;
            Volume = volume;
            IsLiquid = isLiquid;
            TypeOfCargo = type;
        }
        public Cargo(string name, double weight, double volume, bool isLiquid, int startTemperature, int endTemperature, CargoTypes type)
        {
            Name = name;
            Weight = weight;
            Volume = volume;
            IsLiquid = isLiquid;
            StartTemperature = startTemperature;
            EndTemperature = endTemperature;
            WasTemperatureSet = true;
            TypeOfCargo = type;
        }
        public override string ToString()
        {
            return Name + " ";
        }

        public override bool Equals(object obj)
        {
            return obj is Cargo cargo &&
                   Weight == cargo.Weight &&
                   Volume == cargo.Volume &&
                   Name == cargo.Name &&
                   IsLiquid == cargo.IsLiquid &&
                   TypeOfCargo == cargo.TypeOfCargo &&
                   StartTemperature == cargo.StartTemperature &&
                   EndTemperature == cargo.EndTemperature &&
                   WasTemperatureSet == cargo.WasTemperatureSet;
        }

        public override int GetHashCode()
        {
            int hashCode = -851064942;
            hashCode = hashCode * -1521134295 + Weight.GetHashCode();
            hashCode = hashCode * -1521134295 + Volume.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + IsLiquid.GetHashCode();
            hashCode = hashCode * -1521134295 + TypeOfCargo.GetHashCode();
            hashCode = hashCode * -1521134295 + StartTemperature.GetHashCode();
            hashCode = hashCode * -1521134295 + EndTemperature.GetHashCode();
            hashCode = hashCode * -1521134295 + WasTemperatureSet.GetHashCode();
            return hashCode;
        }
    }
}
