using ClassLibraryCarPark;
using System;
using System.Collections.Generic;
using System.Text;

namespace Execution.FactoryReaderWriter
{
    class MyStreamReader : MyReader
    {
        public override void ReadInfo(List<Semitrailer> listOfTrailers, List<TruckTractor> listOfTractors, List<Cargo> listOfCargo)
        {
            throw new NotImplementedException();
        }
    }
}
