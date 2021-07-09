using ClassLibraryCarPark;
using System;
using System.Collections.Generic;
using System.Text;

namespace Execution.FactoryReaderWriter
{
    abstract class MyReader
    {
        public abstract void Read(List<Semitrailer> listOfTrailers, List<TruckTractor> listOfTractors, List<Cargo> listOfCargo);
    }
}
