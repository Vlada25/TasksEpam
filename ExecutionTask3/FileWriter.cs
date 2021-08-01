using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExecutionTask3
{
    class FileWriter
    {
        const string FILENAME = @"E:\Epam May 2021\TasksEpam\OutputFile.txt";
        static FileStream loggingFile = null;
        static StreamWriter streamWriter;

        public static void Write(string info)
        {
            try
            {
                loggingFile = new FileStream(FILENAME, FileMode.Append);
                streamWriter = new StreamWriter(loggingFile);
                streamWriter.WriteLine(info);
                streamWriter.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (loggingFile != null)
                {
                    loggingFile.Close();
                }
            }
        }
        public static void CleanFile()
        {
            try
            {
                loggingFile = new FileStream(FILENAME, FileMode.Create);
                streamWriter = new StreamWriter(loggingFile);
                streamWriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (loggingFile != null)
                {
                    loggingFile.Close();
                }
            }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
