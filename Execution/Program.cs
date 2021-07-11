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

            Random random = new Random();
            int x = random.Next(0, 2);
            
            try
            {
                List<Semitrailer> trailers = new List<Semitrailer>();
                List<TruckTractor> tractors = new List<TruckTractor>();
                List<Cargo> cargo = new List<Cargo>();

                if (x == 0)
                {
                    MyReader reader = new MyStreamReader();
                    reader.Read(trailers, tractors, cargo);
                }
                else
                {
                    MyReader reader = new MyXmlReader();
                    reader.Read(trailers, tractors, cargo);
                }

                trailers[(int)Indices.Refrigerator].LoadTrailer(cargo[(int)Indices.Milk]);
                trailers[(int)Indices.Refrigerator].LoadTrailer(cargo[(int)Indices.Fish]);
                trailers[(int)Indices.TankTruck].LoadTrailer(cargo[(int)Indices.DT]);
                trailers[(int)Indices.TiltSemitrailer].LoadTrailer(cargo[(int)Indices.Fairy]);
                trailers[(int)Indices.TiltSemitrailer].LoadTrailer(cargo[(int)Indices.WashingPowder]);

                trailers[(int)Indices.Refrigerator].UnloadAll();
                trailers[(int)Indices.TankTruck].UnloadTrailer(cargo[(int)Indices.DT], 50);
                trailers[(int)Indices.TiltSemitrailer].UnloadTrailer(cargo[(int)Indices.WashingPowder]);

                trailers[(int)Indices.Refrigerator].JoingWithTractor(tractors[0]);
                trailers[(int)Indices.Refrigerator].UnhookFromTractor();
                trailers[(int)Indices.TankTruck].JoingWithTractor(tractors[0]);
                trailers[(int)Indices.TiltSemitrailer].JoingWithTractor(tractors[1]);

                string carParkViewer = CarParkService.ViewCarPark(trailers, tractors);
                string foundedTrailers1 = CarParkService.FindSemitrailer(trailers, Semitrailer.TypesOfTrailers.Refrigerator);
                string foundedTrailers2 = CarParkService.FindSemitrailer(trailers, 30, 150, CarParkService.WeightOrVolume.Weight);
                string foundedCouplings1 = CarParkService.FindCouplingsByCargo(trailers, Cargo.CargoTypes.Fuel);
                string foundedCouplings2 = CarParkService.FindCouplingWithAnyFreeSpace(trailers);

                string fuelConsumption = $"\nFuel consumption of {tractors[1].Model} #{tractors[1].Number} = {tractors[1].CountFuelConsumption()}";

                if (x == 0)
                {
                    MyWriter writer = new MyStreamWriter();
                    writer.Write(trailers, tractors, cargo);
                }
                else
                {
                    MyWriter writer = new MyXmlWriter();
                    writer.Write(trailers, tractors, cargo);
                }
            }
            catch(Exception error)
            {
                string errorMessage = "Error: " + error.Message;
            }
        }
    }
}
