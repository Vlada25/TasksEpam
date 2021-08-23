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
        [DataRow("ВЛАДА", "СОНЯ", "ВОТ ПОМИДОР", "Я НЕ ХОЧУ")]
        public void ExchangeMessage_RussianMessage_ReturnEnglishMessage(string name1, string name2, string message1, string message2)
        {
            MyTcpClient client1 = new MyTcpClient(8888, "127.0.0.1", name1);
            MyTcpClient client2 = new MyTcpClient(8888, "127.0.0.1", name2);
            MyTcpListener listener = new MyTcpListener();

            Task.Run(() => { listener.DataExchange(); });

            string result1 = client1.ExchangeMessage(message1);
            string result2 = client2.ExchangeMessage(message2);

            string expected1 = "VOT POMIDOR";
            string expected2 = "YA NE KHOCHU";

            Assert.AreEqual(expected1, result1);
            Assert.AreEqual(expected2, result2);
        }

        [TestMethod]
        [DataRow("ВЛАДА", "СОНЯ", "VOT KOLPAK", "KLASS")]
        public void ExchangeMessage_EnglishMessage_ReturnRussianMessage(string name1, string name2, string message1, string message2)
        {
            MyTcpClient client1 = new MyTcpClient(8888, "127.0.0.1", name1);
            MyTcpClient client2 = new MyTcpClient(8888, "127.0.0.1", name2);
            MyTcpListener listener = new MyTcpListener();

            Task.Run(() => { listener.DataExchange(); });

            string result1 = client1.ExchangeMessage(message1);
            string result2 = client2.ExchangeMessage(message2);

            string expected1 = "ВОТ КОЛПАК";
            string expected2 = "КЛАСС";

            Assert.AreEqual(expected1, result1);
            Assert.AreEqual(expected2, result2);
        }

        [TestMethod]
        [DataRow("ВЛАДА", "СОНЯ", "ВОТ ПОМИДОР", "Я НЕ ХОЧУ", "ПРИВЕТ")]
        public void GetAllMessagesByClientName_ClientName_ReturnCountOfMessages(string name1, string name2, string message1, string message2, string message3)
        {
            MyTcpClient client1 = new MyTcpClient(8888, "127.0.0.1", name1);
            MyTcpClient client2 = new MyTcpClient(8888, "127.0.0.1", name2);
            MyTcpListener listener = new MyTcpListener();

            Task.Run(() => { listener.DataExchange(); });

            client1.ExchangeMessage(message1);
            client2.ExchangeMessage(message2);
            client2.ExchangeMessage(message3);

            int result1 = MessageBase.GetAllMessagesByClientName("ВЛАДА").Count;
            int result2 = MessageBase.GetAllMessagesByClientName("СОНЯ").Count;

            int expected1 = 1;
            int expected2 = 2;

            Assert.AreEqual(expected1, result1);
            Assert.AreEqual(expected2, result2);
        }

        [TestMethod]
        [DataRow("ВЛАДА", "СОНЯ", "ВОТ ПОМИДОР", "Я НЕ ХОЧУ", "ПРИВЕТ")]
        public void GetAllMessages_ReturnCountOfMessages(string name1, string name2, string message1, string message2, string message3)
        {
            MyTcpClient client1 = new MyTcpClient(8888, "127.0.0.1", name1);
            MyTcpClient client2 = new MyTcpClient(8888, "127.0.0.1", name2);
            MyTcpListener listener = new MyTcpListener();

            Task.Run(() => { listener.DataExchange(); });

            client1.ExchangeMessage(message1);
            client2.ExchangeMessage(message2);
            client2.ExchangeMessage(message3);

            int result = MessageBase.GetAllMessages().Count;

            int expected = 2;

            Assert.AreEqual(expected, result);
        }
    }
}
