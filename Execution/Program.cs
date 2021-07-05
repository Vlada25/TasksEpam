using ClassLibraryCarPark;
using ClassLibraryCarPark.Types_of_trailers;
using System;
using System.Collections.Generic;

namespace Execution
{
    class Program
    {
        static void Main(string[] args)
        {
            // if milk or something like that is in boxes or bags, then it doesn't rush with liquid
            // p = m*V
            try
            {
                List<Semitrailer> listOfTrailers = new List<Semitrailer>();
                List<TruckTractor> listOfTractors = new List<TruckTractor>();

                Refrigerator trailer1 = new Refrigerator(40, 50);
                TankTruck trailer2 = new TankTruck(1000, 1200);
                TiltSemitrailer trailer3 = new TiltSemitrailer(100, 200);
                listOfTrailers.Add(trailer1);
                listOfTrailers.Add(trailer2);
                listOfTrailers.Add(trailer3);

                //Console.WriteLine(trailer1.ToString());

                Cargo c1 = new Cargo("milk", 12, 12.93, false, 0, 5, Cargo.CargoTypes.Product);
                Cargo c2 = new Cargo("fish", 10, 9.3, false, -12, 0, Cargo.CargoTypes.Product);
                Cargo c3 = new Cargo("DT", 420, 500, true, Cargo.CargoTypes.Fuel);
                //Cargo c4 = new Cargo("porridge", 15, 44.46, false, Cargo.CargoTypes.Product);
                Cargo c5 = new Cargo("washing-powder", 30, 40.6, false, Cargo.CargoTypes.Chemicals);
                Cargo c6 = new Cargo("fairy", 18, 18.36, false, Cargo.CargoTypes.Chemicals);

                //c4.TypeOfCargo = Cargo.CargoTypes.Product;

                trailer1.LoadTrailer(c1);
                trailer1.LoadTrailer(c2);
                trailer2.LoadTrailer(c3);
                trailer3.LoadTrailer(c6);
                trailer3.LoadTrailer(c5);

                Console.WriteLine(trailer1.ToString());
                Console.WriteLine(trailer2.ToString());
                Console.WriteLine(trailer3.ToString());

                trailer1.UnloadAll();
                trailer2.UnloadTrailer(c3, 50);
                trailer3.UnloadTrailer(c5);

                Console.WriteLine("\nAFTER UPLOADING:\n");
                Console.WriteLine(trailer1.ToString());
                Console.WriteLine(trailer2.ToString());
                Console.WriteLine(trailer3.ToString());

                Console.WriteLine("\nAFTER JOING:\n");
                TruckTractor tractor1 = new TruckTractor("MAN 40.604 DFAT", 41.3, 50000);
                listOfTractors.Add(tractor1);
                trailer1.JoingWithTractor(tractor1);

                Console.WriteLine(tractor1.ToString());
                //Console.WriteLine(trailer1.ToString());

                trailer1.UnhookFromTractor();
                trailer2.JoingWithTractor(tractor1);

                //Console.WriteLine(trailer1.ToString());
                //Console.WriteLine(trailer2.ToString());

                Service.ViewCarPark(listOfTrailers, listOfTractors);
                Service.FindSemitrailer(listOfTrailers, Semitrailer.TypesOfTrailers.Refrigerator);
                Service.FindSemitrailer(listOfTrailers, 30, 150, Service.WeightOrVolume.Weight);
                Service.FindCouplingsByCargo(listOfTrailers, Cargo.CargoTypes.Fuel);
                Service.FindCouplingWithAnyFreeSpace(listOfTrailers);
            }
            catch(Exception error)
            {
                Console.WriteLine("Error: " + error.Message);
            }
        }
    }
}
