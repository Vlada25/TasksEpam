using ClassLibraryCarPark;
using System;
using System.Collections.Generic;
using System.Text;

namespace Execution.FactoryReaderWriter
{
    abstract class MyWriter
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        /// <summary>
        /// Writing information to xml file
        /// </summary>
        /// <param name="listOfTrailers"> All trailers </param>
        /// <param name="listOfTractors"> All tractors </param>
        /// <param name="listOfCargo"> All cargo </param>
        public abstract void Write(List<Semitrailer> listOfTrailers, List<TruckTractor> listOfTractors, List<Cargo> listOfCargo);
    }
}
