using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryGauss
{
    public abstract class ExtendedMatrix : IExtendedMatrix
    {
        protected int size;
        private static int _countOfMatrices;
        private readonly int _countOfLines;
        private readonly int _countOfColumns;
        public double[,] Matrix { get; }
        public double[] Solutions { get; }
        public int Number { get; }
        protected int typeOfSolution;

        public ExtendedMatrix(int size, double[,] matrix)
        {
            this.size = size;
            Matrix = matrix;
            Solutions = Solve((double[,])matrix.Clone());
            _countOfLines = size;
            _countOfColumns = size + 1;
            _countOfMatrices++;
            Number = _countOfMatrices;
        }

        public abstract double[] Solve(double[,] matrix);

        public override string ToString()
        {
            string result = "";

            result += $"\nMatrix #{Number}\n";
            result += $"Type of solution: {typeOfSolution}\n";

            for (int i = 0; i < _countOfLines; i++)
            {
                for (int j = 0; j < _countOfColumns; j++)
                {
                    result += Matrix[i, j] + " ";
                    if (j == _countOfLines - 1)
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
