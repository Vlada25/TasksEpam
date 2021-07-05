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
    }
}
