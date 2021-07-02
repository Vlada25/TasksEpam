using ClassLibraryCarPark;
using ClassLibraryCarPark.Types_of_trailers;
using System;

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
                Refrigerator tr1 = new Refrigerator(1223, 40, 50);
                TankTruck tr2 = new TankTruck(4285, 1000, 1200);
                TiltSemitrailer tr3 = new TiltSemitrailer(8123, 100, 200);
                //Console.WriteLine(tr1.ToString());
                Cargo c1 = new Cargo("milk", 12, 12.93, false, 0, 5);
                Cargo c2 = new Cargo("fish", 10, 9.3, false, -12, 0);
                Cargo c3 = new Cargo("DT", 420, 500, true);
                //Cargo c4 = new Cargo("porridge", 15, 44.46, false);
                Cargo c5 = new Cargo("washing-powder", 30, 40.6, false);
                Cargo c6 = new Cargo("fairy", 18, 18.36, false);
                //c4.TypeOfCargo = Cargo.CargoTypes.Product;
                c5.TypeOfCargo = Cargo.CargoTypes.Chemicals;
                c6.TypeOfCargo = Cargo.CargoTypes.Chemicals;
                tr1.LoadTrailer(c1);
                tr1.LoadTrailer(c2);
                tr2.LoadTrailer(c3);
                tr3.LoadTrailer(c6);
                tr3.LoadTrailer(c5);
                Console.WriteLine(tr1.ToString());
                Console.WriteLine(tr2.ToString());
                Console.WriteLine(tr3.ToString());
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
    }
}
