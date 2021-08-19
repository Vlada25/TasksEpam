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
        public static void SetMessage(string filename, int num)
        {
            Message += $"Invalid double value in digit #{num} in {filename}\n";
            throw new Exception(Message);
        }
    }
}
