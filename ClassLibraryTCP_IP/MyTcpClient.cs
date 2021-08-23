﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryTCP_IP
{
    public class MyTcpClient
    {
        public int Port { get; }
        public string Ip { get; }
        public string SenderName { get; }

        public delegate string MethodContainer(string message);
        public event MethodContainer OnTranscodingMessage;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="port"> Given port </param>
        /// <param name="ip"> Given IP </param>
        /// <param name="name"> Name of sender </param>
        public MyTcpClient(int port, string ip, string name)
        {
            Port = port;
            Ip = ip;
            SenderName = name;
            OnTranscodingMessage += message => ClientMessage.TranscodingMessage(message);
        }

        /// <summary>
        /// Sending a message to the server and back
        /// </summary>
        /// <param name="outMessage"> Given message </param>
        /// <returns> Message </returns>
        public string ExchangeMessage(string outMessage)
        {
            TcpClient tcpClient = new TcpClient(Ip, Port);
            Byte[] data = null;

            if (OnTranscodingMessage != null)
            {
                data = Encoding.UTF8.GetBytes(SenderName + ":" + OnTranscodingMessage(outMessage));
            }
            else
            {
                data = Encoding.UTF8.GetBytes(outMessage);
            }

            NetworkStream networkStream = tcpClient.GetStream();

            try
            {
                networkStream.Write(data, 0, data.Length);

                Byte[] readingData = new Byte[256];
                String responseData = String.Empty;

                StringBuilder completeMessage = new StringBuilder();
                int countOfBytesRead = 0;

                do
                {
                    countOfBytesRead = networkStream.Read(readingData, 0, readingData.Length);
                    completeMessage.AppendFormat("{0}", Encoding.UTF8.GetString(readingData, 0, countOfBytesRead));
                }
                while (networkStream.DataAvailable);

                responseData = completeMessage.ToString();
                return responseData;
            }
            finally
            {
                networkStream.Close();
                tcpClient.Close();
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
