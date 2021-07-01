using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark.Types_of_trailers
{
    class TiltSemitrailer : Semitrailer
    {
        public TiltSemitrailer(int number, double maxWeight, double maxVolume)
            : base(number, maxWeight, maxVolume)
        {
            typeOfTrailer = "TiltSemitrailer";
        }
    }
}
