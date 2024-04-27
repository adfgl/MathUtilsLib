using BenchmarkDotNet.Attributes;
using LinearAlgebraLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkConsole
{
    public class MatrixMultiplicationBenchmark
    {
        static int m_steps = 10000;

        [Benchmark]
        public Mat4 StructApproach()
        {
            Mat4 mat = new Mat4(1, 3, -44, 5, 5, 55, -23, 2, 2, 0, 2, 2, 2, 0, 2, 2);
            for (int i = 0; i < m_steps; i++)
            {
                mat = Mat4.Multiply(mat, mat);
            }
            return mat;
        }

        [Benchmark]
        public double[,] ArrayApproach()
        {
            double[,] m = new double[4, 4];
            m[0, 0] = 1;
            m[0, 1] = 3;
            m[0, 2] = -44;
            m[0, 3] = 5;

            m[1, 0] = 5;
            m[1, 1] = 55;
            m[1, 2] = -23;
            m[1, 3] = 2;

            m[2, 0] = 2;
            m[2, 1] = 0;
            m[2, 2] = 2;
            m[2, 3] = 2;

            m[3, 0] = 2;
            m[3, 1] = 0;
            m[3, 2] = 2;
            m[3, 3] = 2;

            for (int i = 0; i < m_steps; i++)
            {
                m = MultiplyMatrices(m, m);
            }
            return m;
        }

        static double[,] MultiplyMatrices(double[,] matrix1, double[,] matrix2)
        {
            double[,] result = new double[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result[i, j] = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        result[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }
            return result;
        }
    }
}
