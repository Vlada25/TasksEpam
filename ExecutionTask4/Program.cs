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
            OutFileName = @"..\outputFile.txt",
            InFileName = @"..\inputFile.txt";
        private static IPHostEntry host = Dns.GetHostEntry("google.com");

        static void Main()
        {
            List<ExtendedMatrix> matricesList = new List<ExtendedMatrix>();
            string res = "";

            try
            {
                using (StreamReader streamReader = new StreamReader(InA_filepath))
                {
                    List<double> fileDataA = new List<double>();
                    string line;

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string s = "";
                        for (int i = 0; i < line.Length; i++)
                        {
                            char c = line[i];
                            if (c != ' ' && i != line.Length - 1)
                            {
                                s += c;
                            }
                            else if (i == line.Length - 1)
                            {
                                fileDataA.Add(Convert.ToDouble(s));
                            }
                            else
                            {
                                if (s != "")
                                {
                                    fileDataA.Add(Convert.ToDouble(s));
                                }
                                s = "";
                            }
                        }
                    }

                    int matrixLen = (int)Math.Sqrt(fileDataA.Count);

                    double[,] matrix = new double[matrixLen, matrixLen + 1];

                    int currentIndex = 0;
                    for (int i = 0; i < matrixLen; i++)
                    {
                        for (int j = 0; j < matrixLen; j++)
                        {
                            matrix[i, j] = fileDataA[currentIndex];
                            currentIndex++;
                        }
                    }

                    for (int i = 0; i < matrixLen; i++)
                    {
                        for (int j = 0; j < matrixLen + 1; j++)
                        {
                            res += matrix[i, j] + " ";
                        }
                        res += "\n";
                    }
                    /*
                    string line;
                    int lineIndex = 0;
                    double[,] matrix = new double[0, 0];
                    int matrixLen = 0;

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (int.TryParse(line, out int len))
                        {
                            matrixLen = len;
                            matrix = new double[matrixLen, matrixLen + 1];
                            lineIndex = 0;
                            continue;
                        }
                        else
                        {
                            string[] equationElements = line.Split(' ');
                            int lastElementIndex = equationElements.Length - 1;
                            int j = 0;

                            foreach (string str in equationElements)
                            {
                                if (str.Equals("="))
                                {
                                    break;
                                }
                                if (str.Equals("+"))
                                {
                                    continue;
                                }

                                int.TryParse(string.Join("", str.Where(c => char.IsDigit(c))), out int num);

                                if (num == 0)
                                {
                                    num = 1;
                                }
                                if (str[0] == '-')
                                {
                                    num *= -1;
                                }

                                matrix[lineIndex, j] = num;
                                j++;
                            }

                            int freeTerm = Convert.ToInt32(equationElements[lastElementIndex]);
                            matrix[lineIndex, j] = freeTerm;
                            lineIndex++;

                            if (lineIndex == matrixLen)
                            {
                                matricesList.Add(new MatrixKind2(matrixLen, matrix));
                            }
                        }
                    }*/
                }

                /*
                foreach (ExtendedMatrix matrix in matricesList)
                {
                    res += matrix.ToString();
                }*/

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
