using ClassLibraryGauss;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace UnitTestTask4
{
    [TestClass]
    public class UnitTestGaussMethod
    {
        [TestMethod]
        public void MatrixKind1Solve_ReturnMultipliedSolutions()
        {
            int len = 2;
            double[,] matrix = { { 1, -1, -5 },
                                 { 2, 1, -7} };
            MatrixKind1 matrixKind1 = new MatrixKind1(len, matrix);

            Task.Run(() => { matrixKind1.Solve(matrix); });

            double result = matrixKind1.Solutions[0] * matrixKind1.Solutions[1];

            double expected = -4;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MatrixKind2Solve_ReturnMultipliedSolutions()
        {
            int len = 2;
            double[,] matrix = { { 1, -1, -5 },
                                 { 2, 1, -7} };
            MatrixKind1 matrixKind2 = new MatrixKind1(len, matrix);

            Task.Run(() => { matrixKind2.Solve(matrix); });

            double result = matrixKind2.Solutions[0] * matrixKind2.Solutions[1];

            double expected = -4;

            Assert.AreEqual(expected, result);
        }
    }
}
