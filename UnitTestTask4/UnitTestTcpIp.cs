using ClassLibraryGauss;
using ClassLibraryTCP_IP;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace UnitTestTask4
{
    [TestClass]
    public class UnitTestTcpIp
    {
        [TestMethod]
        [DataRow("ВЛАДА", "СОНЯ")]
        public void ExchangeMessage_Matrix_ReturnSolution(string name1, string name2)
        {
            MyTcpClient client1 = new MyTcpClient(8888, "127.0.0.1", name1);
            MyTcpClient client2 = new MyTcpClient(8888, "127.0.0.1", name2);
            MyTcpListener listener = new MyTcpListener();

            Task.Run(() => { listener.DataExchange(); });

            int len = 3;
            double[,] matrix = { { 2, 1, -1, 8}, { -3, -1, 2, -11}, { -2, 1, 2, -3} };

            MatrixKind1 matrixKind1 = new MatrixKind1(len, matrix);
            MatrixKind2 matrixKind2 = new MatrixKind2(len, matrix);

            string result1 = client1.ExchangeMessage(matrixKind1);
            string result2 = client2.ExchangeMessage(matrixKind2);

            Assert.AreEqual(result1, result2);
        }
    }
}
