using ClassLibraryCarPark;
using ClassLibraryCarPark.Types_of_trailers;
using Execution.FactoryReaderWriter;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Execution
{
    class Program
    {
        enum Indices
        {
            Refrigerator = 0,
            TankTruck = 1,
            TiltSemitrailer = 2,

            Milk = 0,
            Fish = 1,
            DT = 2,
            Porridge = 3,
            WashingPowder = 4,
            Fairy = 5
        }
        static void Main(string[] args)
        {
            // if milk or something like that is in boxes or bags, then it doesn't rush with liquid
            // p = m*V
            
            try
            {
                List<Semitrailer> trailers = new List<Semitrailer>();
                List<TruckTractor> tractors = new List<TruckTractor>();
                List<Cargo> cargo = new List<Cargo>();

                //MyReader reader = new MyXmlReader();
                MyReader reader = new MyStreamReader();
                reader.Read(trailers, tractors, cargo);

                //MyWriter writer = new MyXmlWriter();
                MyWriter writer = new MyStreamWriter();
                writer.Write(trailers, tractors, cargo);

                Console.WriteLine(CarParkService.ViewCarPark(trailers, tractors));

                trailers[(int)Indices.Refrigerator].LoadTrailer(cargo[(int)Indices.Milk]);
                trailers[(int)Indices.Refrigerator].LoadTrailer(cargo[(int)Indices.Fish]);
                trailers[(int)Indices.TankTruck].LoadTrailer(cargo[(int)Indices.DT]);
                trailers[(int)Indices.TiltSemitrailer].LoadTrailer(cargo[(int)Indices.Fairy]);
                trailers[(int)Indices.TiltSemitrailer].LoadTrailer(cargo[(int)Indices.WashingPowder]);

                Console.WriteLine(trailers[(int)Indices.Refrigerator].ToString());
                Console.WriteLine(trailers[(int)Indices.TankTruck].ToString());
                Console.WriteLine(trailers[(int)Indices.TiltSemitrailer].ToString());

                trailers[(int)Indices.Refrigerator].UnloadAll();
                trailers[(int)Indices.TankTruck].UnloadTrailer(cargo[(int)Indices.DT], 50);
                trailers[(int)Indices.TiltSemitrailer].UnloadTrailer(cargo[(int)Indices.WashingPowder]);

                Console.WriteLine("\n\nAFTER UPLOADING:");
                Console.WriteLine(trailers[(int)Indices.Refrigerator].ToString());
                Console.WriteLine(trailers[(int)Indices.TankTruck].ToString());
                Console.WriteLine(trailers[(int)Indices.TiltSemitrailer].ToString());

                Console.WriteLine("\n\nAFTER JOING:\n");
                trailers[(int)Indices.Refrigerator].JoingWithTractor(tractors[0]);

                Console.WriteLine(tractors[0].ToString());
                Console.WriteLine(trailers[(int)Indices.Refrigerator].ToString());

                trailers[(int)Indices.Refrigerator].UnhookFromTractor();
                trailers[(int)Indices.TankTruck].JoingWithTractor(tractors[0]);
                trailers[(int)Indices.TiltSemitrailer].JoingWithTractor(tractors[1]);

                Console.WriteLine(trailers[(int)Indices.Refrigerator].ToString());
                Console.WriteLine(trailers[(int)Indices.TankTruck].ToString());

                Console.WriteLine(CarParkService.ViewCarPark(trailers, tractors));
                Console.WriteLine(CarParkService.FindSemitrailer(trailers, Semitrailer.TypesOfTrailers.Refrigerator));
                Console.WriteLine(CarParkService.FindSemitrailer(trailers, 30, 150, CarParkService.WeightOrVolume.Weight));
                Console.WriteLine(CarParkService.FindCouplingsByCargo(trailers, Cargo.CargoTypes.Fuel));
                Console.WriteLine(CarParkService.FindCouplingWithAnyFreeSpace(trailers));

                Console.WriteLine($"\nFuel consumption of {tractors[1].Model} #{tractors[1].Number} = {tractors[1].CountFuelConsumption()}");
            }
            catch(Exception error)
            {
                Console.WriteLine("Error: " + error.Message);
            }
        }
    }
}
