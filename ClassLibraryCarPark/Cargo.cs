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
        public double weight;
        public double volume;
        public string name;
        public bool isLiquid;
        public CargoTypes TypeOfCargo { get;}
        public int StartTemperature { get; }
        public int EndTemperature { get; }
        public bool wasTemperatureSet = false;
        public Cargo(string name, double weight, double volume, bool isLiquid, CargoTypes type)
        {
            this.name = name;
            this.weight = weight;
            this.volume = volume;
            this.isLiquid = isLiquid;
            TypeOfCargo = type;
        }
        public Cargo(string name, double weight, double volume, bool isLiquid, int startTemperature, int endTemperature, CargoTypes type)
        {
            this.name = name;
            this.weight = weight;
            this.volume = volume;
            this.isLiquid = isLiquid;
            StartTemperature = startTemperature;
            EndTemperature = endTemperature;
            wasTemperatureSet = true;
            TypeOfCargo = type;
        }
        public override string ToString()
        {
            return name + " ";
        }

        public override bool Equals(object obj)
        {
            return obj is Cargo cargo &&
                   weight == cargo.weight &&
                   volume == cargo.volume &&
                   name == cargo.name &&
                   isLiquid == cargo.isLiquid &&
                   TypeOfCargo == cargo.TypeOfCargo &&
                   StartTemperature == cargo.StartTemperature &&
                   EndTemperature == cargo.EndTemperature &&
                   wasTemperatureSet == cargo.wasTemperatureSet;
        }

        public override int GetHashCode()
        {
            int hashCode = -851064942;
            hashCode = hashCode * -1521134295 + weight.GetHashCode();
            hashCode = hashCode * -1521134295 + volume.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + isLiquid.GetHashCode();
            hashCode = hashCode * -1521134295 + TypeOfCargo.GetHashCode();
            hashCode = hashCode * -1521134295 + StartTemperature.GetHashCode();
            hashCode = hashCode * -1521134295 + EndTemperature.GetHashCode();
            hashCode = hashCode * -1521134295 + wasTemperatureSet.GetHashCode();
            return hashCode;
        }
    }
}
