using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibraryCarPark
{
    interface ITrailer
    {
        double MaxWeight { get; }
        double MaxVolume { get; }
        void UnloadAll();
        void UnloadTrailer(Cargo cargo);
        void UnloadTrailer(Cargo cargo, int percentOfCargo);
        void JoingWithTractor(TruckTractor tractor);
        void UnhookFromTractor();
    }
}
