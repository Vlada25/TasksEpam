using ClassLibraryCarPark;
using ClassLibraryCarPark.Types_of_trailers;
using System;

namespace Execution
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Refrigerator r = new Refrigerator(1223, 32, 12);
                Console.WriteLine(r.ToString());
                Cargo cargo = new Cargo("milk", 0.515, 0.5, true, 10);
                r.LoadTrailer(cargo);
                Console.WriteLine(r.ToString());
            }
            catch(Exception error)
            {
                Console.WriteLine(error.Message);
            }
        }
    }
}
