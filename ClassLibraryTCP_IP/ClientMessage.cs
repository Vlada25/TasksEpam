using ClassLibraryGauss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibraryTCP_IP
{
    class ClientMessage
    {
        public static string FormSolutionMessage(ExtendedMatrix matrix)
        {
            string result = "";
            for (int i = 0; i < matrix.Solutions.Length; i++)
            {
                result += matrix.Solutions[i];
                if (i != matrix.Solutions.Length - 1)
                {
                    result += ", ";
                }
            }
            return result;
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
