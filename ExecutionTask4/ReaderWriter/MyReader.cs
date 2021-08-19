﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecutionTask4.ReaderWriter
{
    class MyReader
    {
        public delegate void MethodContainer(string filename, int num);
        public static event MethodContainer OnReadData;
        public static void ReadData(string filename, StreamReader streamReader, List<double> fileData)
        {
            int digitNumber = 0;
            string line = streamReader.ReadToEnd();
            string s = "";
            
            foreach (char c in line)
            {
                if (c == 45 || c == 44 || (c > 47 && c < 58))
                {
                    s += c;
                }
                else
                {
                    if (s != "")
                    {
                        digitNumber++;
                        bool isNum = double.TryParse(s, out double number);
                        if (!isNum)
                        {
                            Console.WriteLine();
                            OnReadData(filename, digitNumber);
                        }
                        else
                        {
                            fileData.Add(number);
                        }
                    }
                    s = "";
                }
            }
        }
    }
}
