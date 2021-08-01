using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryBistro;
using System;
using System.Collections.Generic;
using static ClassLibraryBistro.ChiefCooker;

namespace UnitTestTask3
{
    [TestClass]
    public class UnitTestHelper
    {
        [TestMethod]
        public void SetTime_StringValueOfTime_ReturnTheSameTime()
        {
            string result = Helper.SetTime("12:43");
            string expected = "12:43";

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void IsTimeInRange_Time_ReturnTrue()
        {
            bool result = Helper.IsTimeInRange("12:43", "10:00", "12:48");

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TrySetFreeSpaceInDevice_FreeSpace_ReturnNewFreeSpace()
        {
            double freeSpaceInDevice = 2,
                weightOfIngredients = 0.5;
            int countOfPortions = 3;
            string nameOfDevice = "Pan";

            double result = Helper.TrySetFreeSpaceInDevice(freeSpaceInDevice, weightOfIngredients, countOfPortions, nameOfDevice);
            double expectd = 0.5;

            Assert.AreEqual(result, expectd);
        }

        [TestMethod]
        public void SetClientNumber_Number_ReturnSameNumber()
        {
            string result = Helper.SetClientNumber("010");
            string expected = "010";

            Assert.AreEqual(result, expected);
        }
    }
}
