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
            ConstructionMaterials
        }
        public double weight;
        public double volume;
        public string name;
        public bool isLiquid;
        public CargoTypes TypeOfCargo { get; set; }
        public int StartTemperature { get; }
        public int EndTemperature { get; }
        public bool wasTemperatureSet = false;
        public Cargo(string name, double weight, double volume, bool isLiquid)
        {
            this.name = name;
            this.weight = weight;
            this.volume = volume;
            this.isLiquid = isLiquid;
        }
        public Cargo(string name, double weight, double volume, bool isLiquid, int startTemperature, int endTemperature)
        {
            this.name = name;
            this.weight = weight;
            this.volume = volume;
            this.isLiquid = isLiquid;
            StartTemperature = startTemperature;
            EndTemperature = endTemperature;
            wasTemperatureSet = true;
        }
        public override string ToString()
        {
            return name + " ";
        }
    }
}
