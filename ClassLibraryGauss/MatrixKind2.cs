using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryGauss
{
    public class MatrixKind2 : ExtendedMatrix
    {
        public MatrixKind2(int lenght, double[,] matrix) : base(lenght, matrix)
        {
            typeOfSolution = 2;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override double[] Solve(double[,] matrix)
        {
            double[] solution = new double[size];

            double[,] copyMatrix = new double[size, size + 1];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size + 1; j++)
                {
                    copyMatrix[i, j] = matrix[i, j];
                }
            }

            // зануление нижнего левого угла
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size + 1; j++)
                {
                    copyMatrix[i, j] = copyMatrix[i, j] / matrix[i, i];
                }
                for (int j = i + 1; j < size; j++)
                {
                    double k = copyMatrix[j, i] / copyMatrix[i, i];
                    for (int p = 0; p < size + 1; p++) //p-номер столбца следующей строки после i
                    {
                        copyMatrix[j, p] = copyMatrix[j, p] - copyMatrix[i, p] * k;
                    }
                }
                for (int j = 0; j < size; j++)
                {
                    for (int p = 0; p < size + 1; p++)
                    {
                        matrix[j, p] = copyMatrix[j, p];
                    }
                }
            }

            // зануление верхнего правого угла
            for (int i = size - 1; i > -1; i--)
            {
                for (int j = size; j > -1; j--)
                {
                    copyMatrix[i, j] = copyMatrix[i, j] / matrix[i, i];
                }

                for (int j = i - 1; j > -1; j--)
                {
                    double k = copyMatrix[j, i] / copyMatrix[i, i];
                    for (int p = size; p > -1; p--)
                    {
                        copyMatrix[j, p] = copyMatrix[j, p] - copyMatrix[i, p] * k;
                    }
                }
            }

            // решения получившейся системы
            for (int i = 0; i < size; i++)
            {
                solution[i] = copyMatrix[i, size];
            }

            return solution;
        }

    }
}
