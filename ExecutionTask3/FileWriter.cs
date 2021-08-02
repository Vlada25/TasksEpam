using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExecutionTask3
{
    internal class FileWriter
    {
        private const string FileName = @"E:\Epam May 2021\TasksEpam\OutputFile.txt";
        private static FileStream _loggingFile = null;
        private static StreamWriter _streamWriter;

        public static void Write(string info)
        {
            try
            {
                _loggingFile = new FileStream(FileName, FileMode.Append);
                _streamWriter = new StreamWriter(_loggingFile);
                _streamWriter.WriteLine(info);
                _streamWriter.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (_loggingFile != null)
                {
                    _loggingFile.Close();
                }
            }
        }
        public static void CleanFile()
        {
            try
            {
                _loggingFile = new FileStream(FileName, FileMode.Create);
                _streamWriter = new StreamWriter(_loggingFile);
                _streamWriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (_loggingFile != null)
                {
                    _loggingFile.Close();
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
