using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark
{
    class TruckTractor
    {
        public int tractorNumber;
        public double carryingCapacity;
        public TruckTractor(int number, double carryingCapacity)
        {
            tractorNumber = number;
            this.carryingCapacity = carryingCapacity;
        }
        public double CountFuelConsumption()
        {
            return 0;
        }
    }
}
