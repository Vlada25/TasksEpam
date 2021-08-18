using ClassLibraryGauss;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExecutionTask4
{
    class Program
    {
        private const string InA_filepath = @"..\inA.txt",
            InB_filepath = @"..\inB.txt",
            OutFileName = @"..\outputFile.txt";
        private static IPHostEntry host = Dns.GetHostEntry("google.com");

        static void Main()
        {
            string res = "";

            try
            {
                int matrixLen;
                double[,] matrix;
                List<double> fileDataA = new List<double>();
                List<double> fileDataB = new List<double>();

                using (StreamReader streamReader = new StreamReader(InA_filepath))
                {
                    ReadData(streamReader, fileDataA);

                    matrixLen = (int)Math.Sqrt(fileDataA.Count);
                    matrix = new double[matrixLen, matrixLen + 1];
                }

                using (StreamReader streamReader = new StreamReader(InB_filepath))
                {
                    ReadData(streamReader, fileDataB);
                }

                int currentIndex = 0;
                for (int i = 0; i < matrixLen; i++)
                {
                    for (int j = 0; j < matrixLen; j++)
                    {
                        matrix[i, j] = fileDataA[currentIndex];
                        currentIndex++;
                    }
                }

                currentIndex = 0;
                for (int i = 0; i < matrixLen; i++)
                {
                    matrix[i, matrixLen] = fileDataB[currentIndex];
                    currentIndex++;
                }

                MatrixKind1 matrixKind1 = new MatrixKind1(matrixLen, matrix);
                MatrixKind2 matrixKind2 = new MatrixKind2(matrixLen, matrix);

                res += matrixKind1.ToString();
                res += matrixKind2.ToString();

                using (StreamWriter streamWriter = new StreamWriter(OutFileName, false, Encoding.Default))
                {
                    streamWriter.WriteLine(res);
                    streamWriter.WriteLine(host.HostName);
                    foreach (IPAddress ip in host.AddressList)
                        streamWriter.WriteLine(ip.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void ReadData(StreamReader streamReader, List<double> fileData)
        {
            string line = streamReader.ReadToEnd();
            string s = "";

            foreach (char c in line)
            {
                if (c != ' ')
                {
                    s += c;
                }
                else
                {
                    if (s != "")
                    {
                        fileData.Add(Convert.ToDouble(s));
                    }
                    s = "";
                }
            }
        }
    }
}
