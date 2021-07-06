using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace Spec_Laba_4
{
    class Matrix
    {
        public int[,] matrix;
        public int size;
        private int N;
        private int M;
        private int T;
        public Matrix(ClassicTask task)
        {
            this.N = task.N;
            this.M = task.M;
            this.T = task.T;
            size = N + N * T + M * T + 2;
            matrix = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    matrix[i, j] = 0;
            }

            for (int i = 0; i < N; i++)
                matrix[0, i + 1] = task.A[i];

            for (int i = 0; i < N; i++)
                for (int t = 0; t < T; t++)
                    matrix[i + 1, 1 + N + T * i + t] = task.B[i][t];

            for (int i = 0; i < N; i++)
                for (int j = 0; j < M; j++)
                    if (task.D[j].Contains(i + 1))
                        for (int k = 0; k < T; k++)
                            matrix[1 + N + T * i + k, 1 + N + N * T + j * T + k] = task.B[i][k];

            for (int i = 0; i < M; i++)
                for (int t = 0; t < T; t++)
                    matrix[1 + N + N * T + i * T + t, 1 + N + N * T + M * T] = task.C[i][t];
        }

        public void AddStores(int capacity)
        {
            for (int i = 0; i < M; i++)
                for (int t = 0; t < T - 1; t++)
                    matrix[1 + N + N * T + i * T + t, 1 + N + N * T + i * T + t + 1] = capacity;
        }

        public void DeleteStore(int store)
        {
            for (int t = 0; t < T - 1; t++)
                matrix[1 + N + N * T + store * T + t, 1 + N + N * T + store * T + t + 1] = 0;
        }

        public void PrintMatrix()
        {
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    Console.Write("{0,3} ", matrix[i, j]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
