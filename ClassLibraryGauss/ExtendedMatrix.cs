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

        public override bool Equals(object obj)
        {
            return obj is ExtendedMatrix matrix &&
                   size == matrix.size &&
                   _countOfLines == matrix._countOfLines &&
                   _countOfColumns == matrix._countOfColumns &&
                   EqualityComparer<double[,]>.Default.Equals(Matrix, matrix.Matrix) &&
                   EqualityComparer<double[]>.Default.Equals(Solutions, matrix.Solutions) &&
                   Number == matrix.Number &&
                   typeOfSolution == matrix.typeOfSolution;
        }

        public override int GetHashCode()
        {
            int hashCode = 634073516;
            hashCode = hashCode * -1521134295 + size.GetHashCode();
            hashCode = hashCode * -1521134295 + _countOfLines.GetHashCode();
            hashCode = hashCode * -1521134295 + _countOfColumns.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<double[,]>.Default.GetHashCode(Matrix);
            hashCode = hashCode * -1521134295 + EqualityComparer<double[]>.Default.GetHashCode(Solutions);
            hashCode = hashCode * -1521134295 + Number.GetHashCode();
            hashCode = hashCode * -1521134295 + typeOfSolution.GetHashCode();
            return hashCode;
        }
    }
}
