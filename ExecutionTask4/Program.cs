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

        delegate void FileReader(string filename, StreamReader streamReader, List<double> fileData);
        
        static void Main()
        {
            try
            {
                int matrixLen;
                double[,] matrix;
                List<double> fileDataA = new List<double>();
                List<double> fileDataB = new List<double>();

                Read(InA_filepath, fileDataA);

                matrixLen = (int)Math.Sqrt(fileDataA.Count);
                matrix = new double[matrixLen, matrixLen + 1];

                Read(InB_filepath, fileDataB);

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

                MyWriter.WriteData(OutFileName, matrixKind1, matrixKind2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Read(string filepath, List<double> fileData)
        {
            using (StreamReader streamReader = new StreamReader(filepath))
            {
                FileReader readSystemMatrix = MyReader.ReadData;
                MyReader.OnReadData += FileError.SetMessage;
                readSystemMatrix(InA_filepath, streamReader, fileData);
            }
        }
    }
}
