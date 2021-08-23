using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryTCP_IP
{
    public class MyTcpListener
    {
        public int Port { get; }
        public string Ip { get; }

        public delegate void MethodContainer(string ip, string message);
        public event MethodContainer OnMessageList;

        /// <summary>
        /// Constructor
        /// </summary>
        public MyTcpListener()
        {
            Port = 8888;
            Ip = "127.0.0.1";
            OnMessageList += (ip, message) => MessageBase.AddMessage(ip, message);
        }

        /// <summary>
        /// Getting a message to the server and saving it to the client message list and sending it back
        /// </summary>
        public void DataExchange()
        {
            IPAddress localAddr = IPAddress.Parse(Ip);
            TcpListener server = new TcpListener(localAddr, Port);

            server.Start();

            while (true)
            {
                try
                {
                    TcpClient tcpClient = server.AcceptTcpClient();
                    NetworkStream networkStream = tcpClient.GetStream();

                    try
                    {
                        if (networkStream.CanRead)
                        {
                            byte[] myReadBuffer = new byte[1024];
                            StringBuilder myCompleteMessage = new StringBuilder();
                            int numberOfBytesRead = 0;
                            do
                            {
                                numberOfBytesRead = networkStream.Read(myReadBuffer, 0, myReadBuffer.Length);
                                myCompleteMessage.AppendFormat("{0}", Encoding.UTF8.GetString(myReadBuffer, 0, numberOfBytesRead));
                            }
                            while (networkStream.DataAvailable);

                            string[] message = myCompleteMessage.ToString().Split(':');
                            OnMessageList?.Invoke(message[0], myCompleteMessage.ToString());

                            Byte[] responseData = Encoding.UTF8.GetBytes(message[1]);
                            networkStream.Write(responseData, 0, responseData.Length);
                        }
                    }
                    finally
                    {
                        networkStream.Close();
                        tcpClient.Close();
                    }
                }
                catch
                {
                    server.Stop();
                    break;
                }
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
