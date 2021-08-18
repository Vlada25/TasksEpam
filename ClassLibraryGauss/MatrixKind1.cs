using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryGauss
{
    public class MatrixKind1 : ExtendedMatrix
    {
        public override int TypeOfSolution { get; }
        public MatrixKind1(int lenght, double[,] matrix) : base(lenght, matrix)
        {
            TypeOfSolution = 1;
        }

        public override double[] Solve(double[,] matrix)
        {
            double[] solution = new double[size];
            double[,] copyMatrix = matrix;

            // зануление левого нижнего угла
            for (int i = 1; i < size; i++)
            {
                for (int j = i; j < size; j++)
                {
                    double k = copyMatrix[j, i - 1] / copyMatrix[i - 1, i - 1];
                    for (int p = 0; p <= size; p++)
                    {
                        copyMatrix[j, p] = copyMatrix[j, p] - copyMatrix[i - 1, p] * k;
                    }
                }
            }

            // решения получившейся системы
            for (int i = size - 1; i >= 0; i--)
            {
                solution[i] = copyMatrix[i, size] / copyMatrix[i, i];
                for (int j = size - 1; j > i; j--)
                {
                    solution[i] = solution[i] - copyMatrix[i, j] * solution[j] / copyMatrix[i, i];
                }
            }

            return solution;
        }
    }
}
