using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryGauss
{
    public abstract class ExtendedMatrix
    {
        protected int size;
        private static int _countOfMatrices;
        public double[,] Matrix { get; }
        public double[] Solutions { get; }
        public int CountOfLines { get; }
        public int CountOfColumns { get; }
        public int Number { get; }
        public abstract int TypeOfSolution { get; }

        public ExtendedMatrix(int size, double[,] matrix)
        {
            this.size = size;
            Matrix = matrix;
            Solutions = Solve(matrix);
            CountOfLines = size;
            CountOfColumns = size + 1;
            _countOfMatrices++;
            Number = _countOfMatrices;
        }

        public abstract double[] Solve(double[,] matrix);

        public override string ToString()
        {
            string result = "";

            result += $"\nMatrix #{Number}\n";
            result += $"Type of solution: {TypeOfSolution}\n";

            for (int i = 0; i < CountOfLines; i++)
            {
                for (int j = 0; j < CountOfColumns; j++)
                {
                    result += Matrix[i, j] + " ";
                    if (j == CountOfLines - 1)
                    {
                        result += "| ";
                    }
                }
                result += "\n";
            }

            result += "Solutions: ";

            foreach (double num in Solutions)
            {
                result += num + " ";
            }

            result += "\n";

            return result;
        }
    }
}
