using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecutionTask4
{
    static class FileError
    {
        public static string Message;

        /// <summary>
        /// Setting message if there was an error
        /// </summary>
        /// <param name="filename"> Name of file </param>
        /// <param name="num"> Number of obgect where was an error </param>
        public static void SetMessage(string filename, int num)
        {
            Message += $"Invalid double value in digit #{num} in {filename}\n";
            throw new Exception(Message);
        }
    }
}
