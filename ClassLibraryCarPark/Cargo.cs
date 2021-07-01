using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark
{
    public class Cargo
    {
        public double weight;
        public double volume;
        public string name;
        public bool isLiquid;
        public int temperature;
        public string typeOfCargo;
        public Cargo(string name, double weight, double volume, bool isLiquid, int temperature)
        {
            this.name = name;
            this.weight = weight;
            this.volume = volume;
            this.isLiquid = isLiquid;
            this.temperature = temperature;
        }
        public override string ToString()
        {
            return name + " ";
        }
    }
}
