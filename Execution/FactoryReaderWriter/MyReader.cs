using ClassLibraryCarPark;
using System;
using System.Collections.Generic;
using System.Text;

namespace Execution.FactoryReaderWriter
{
    abstract class MyReader
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Reading information from xml file
        /// </summary>
        /// <param name="listOfTrailers"> All trailers </param>
        /// <param name="listOfTractors"> All tractors </param>
        /// <param name="listOfCargo"> All cargo </param>
        public abstract void Read(List<Semitrailer> listOfTrailers, List<TruckTractor> listOfTractors, List<Cargo> listOfCargo);

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
