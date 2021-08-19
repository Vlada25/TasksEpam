using ClassLibraryGauss;
using ExecutionTask4.ReaderWriter;
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

        delegate void FileReader(string filename, StreamReader streamReader, List<double> fileData);
        

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
                    FileReader readSystemMatrix = MyReader.ReadData;
                    MyReader.OnFileReader += FileError.SetMessage;
                    readSystemMatrix(InA_filepath, streamReader, fileDataA);

                    matrixLen = (int)Math.Sqrt(fileDataA.Count);
                    matrix = new double[matrixLen, matrixLen + 1];
                }

                using (StreamReader streamReader = new StreamReader(InB_filepath))
                {
                    FileReader readFreeTerms = MyReader.ReadData;
                    readFreeTerms(InB_filepath, streamReader, fileDataB);
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

                if (FileError.Message != null)
                {
                    res += FileError.Message;
                }
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
    }
}
