using ClassLibraryCarPark;
using System;
using System.Collections.Generic;
using System.Text;

namespace Execution.FactoryReaderWriter
{
    public abstract class MyWriter
    {
        public abstract void Write(List<Semitrailer> listOfTrailers, List<TruckTractor> listOfTractors, List<Cargo> listOfCargo);
    }
}
