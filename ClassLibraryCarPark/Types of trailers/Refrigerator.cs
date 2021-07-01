using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark.Types_of_trailers
{
    public class Refrigerator : Semitrailer
    {
        public Refrigerator(int number, double maxWeight, double maxVolume)
            : base(number, maxWeight, maxVolume)
        {
            typeOfTrailer = "Refrigerator";
        }
    }
}
