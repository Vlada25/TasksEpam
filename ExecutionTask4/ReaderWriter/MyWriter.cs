using ClassLibraryGauss;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExecutionTask4.ReaderWriter
{
    class MyWriter
    {
        static string res;

        /// <summary>
        /// Writing data to file
        /// </summary>
        /// <param name="filepath"> Filepath </param>
        /// <param name="matrices"> List of matrices </param>
        public static void WriteData(string filepath, params ExtendedMatrix[] matrices)
        {
            res = "";
            if (FileError.Message != null)
            {
                res += FileError.Message;
            }

            foreach (ExtendedMatrix matrix in matrices)
            {
                res += matrix.ToString();
            }

            using (StreamWriter streamWriter = new StreamWriter(filepath, false, Encoding.Default))
            {
                streamWriter.WriteLine(res);
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
