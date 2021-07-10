using ClassLibraryCarPark;
using ClassLibraryCarPark.Types_of_trailers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestTask2
{
    [TestClass]
    public class UnitTestSemitrailer
    {
        [TestMethod]
        public void LoadTrailer_Test()
        {
            Semitrailer trailer = new Refrigerator(40, 50);
            Cargo cargo = new Cargo("milk", 12, 12.93, false, 3, 5, Cargo.CargoTypes.Product);
            trailer.LoadTrailer(cargo);

            int result = trailer.ListOfCargo.Count;
            int expected = 1;

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void GetTypeOfTrailer_Test()
        {
            Semitrailer trailer = new Refrigerator(40, 50);

            Semitrailer.TypesOfTrailers result = trailer.GetTypeOfTrailer();
            Semitrailer.TypesOfTrailers expected = Semitrailer.TypesOfTrailers.Refrigerator;

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void GetWeihgtOfAllCargo_Test()
        {
            Semitrailer trailer = new Refrigerator(40, 50);
            Cargo cargo1 = new Cargo("milk", 12, 12.93, false, 3, 5, Cargo.CargoTypes.Product);
            Cargo cargo2 = new Cargo("cream", 15, 16.3, false, 0, 10, Cargo.CargoTypes.Product);

            trailer.LoadTrailer(cargo1);
            trailer.LoadTrailer(cargo2);

            string result = Convert.ToString(trailer.GetWeihgtOfAllCargo());
            string expected = "27";

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void UnloadAll_Test()
        {
            Semitrailer trailer = new Refrigerator(40, 50);
            Cargo cargo = new Cargo("milk", 12, 12.93, false, 3, 5, Cargo.CargoTypes.Product);
            trailer.LoadTrailer(cargo);
            trailer.UnloadAll();

            int result = trailer.ListOfCargo.Count;
            int expected = 0;

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void UnloadTrailer_Test_1()
        {
            Semitrailer trailer = new Refrigerator(40, 50);
            Cargo cargo = new Cargo("milk", 12, 12.93, false, 3, 5, Cargo.CargoTypes.Product);
            trailer.LoadTrailer(cargo);
            trailer.UnloadTrailer(cargo);

            int result = trailer.ListOfCargo.Count;
            int expected = 0;

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void UnloadTrailer_Test_2()
        {
            Semitrailer trailer = new Refrigerator(40, 50);
            Cargo cargo = new Cargo("milk", 12, 12.93, false, 3, 5, Cargo.CargoTypes.Product);
            trailer.LoadTrailer(cargo);
            trailer.UnloadTrailer(cargo, 50);

            string result = Convert.ToString(trailer.GetWeihgtOfAllCargo());
            string expected = "6";

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void JoingWithTractor_Test()
        {
            Semitrailer trailer = new Refrigerator(40, 50);
            TruckTractor tractor = new TruckTractor("MAN_40.604_DFAT", 41.3, 10000);

            trailer.JoingWithTractor(tractor);

            string result = trailer.JoinedTractor.Model;
            string expected = "MAN_40.604_DFAT";

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void UnhookFromTractor_Test()
        {
            Semitrailer trailer = new Refrigerator(40, 50);
            TruckTractor tractor = new TruckTractor("MAN_40.604_DFAT", 41.3, 10000);

            trailer.JoingWithTractor(tractor);
            trailer.UnhookFromTractor();

            TruckTractor result = trailer.JoinedTractor;
            TruckTractor expected = null;

            Assert.AreEqual(result, expected);
        }
    }
}
