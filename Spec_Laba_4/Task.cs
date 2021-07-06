using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace Spec_Laba_4
{
    class ClassicTask 
    {
        public int N { get; set; }   
        public int M { get; set; }
        public int T { get; set; }         
        public List<int> A { get; set; }
        public List<List<int>> B { get; set; }
        public List<List<int>> C { get; set; }
        public List<List<int>> D { get; set; }
        public int C_sum { get; set; }
        public int A_sum { get; set; }
        public List<List<int>> X { get; set; }
        public Matrix matrix;

        public ClassicTask(string file_path)
        {
            this.C_sum = 0;
            this.A_sum = 0;
            this.A = new List<int>();
            this.B = new List<List<int>>();
            this.C = new List<List<int>>();
            this.D = new List<List<int>>();
            this.X = new List<List<int>>();
            ReadData(file_path);
            this.matrix = new Matrix(this);
        }

        private void ReadData(string file_path)
        {
            using StreamReader stream_reader = new StreamReader(file_path);
            string[] tmp = null;
            this.N = Convert.ToInt32(stream_reader.ReadLine());
            this.M = Convert.ToInt32(stream_reader.ReadLine());
            this.T = Convert.ToInt32(stream_reader.ReadLine());
            stream_reader.ReadLine();
            string line = stream_reader.ReadLine();
            tmp = line.Split(' ');
            for (int i = 0; i < N; i++)
            {
                A.Add(Convert.ToInt32(tmp[i]));
                A_sum += A[i];
            }

            stream_reader.ReadLine();
            for (int i = 0; i < N; i++)
            {
                B.Add(new List<int>());
                line = stream_reader.ReadLine();
                tmp = line.Split(' ');
                for (int t = 0; t < T; t++)
                    B[i].Add(Convert.ToInt32(tmp[t]));
            }

            stream_reader.ReadLine();
            for (int j = 0; j < M; j++)
            {
                C.Add(new List<int>());
                line = stream_reader.ReadLine();
                tmp = line.Split(' ');
                for (int t = 0; t < T; t++)
                {
                    C[j].Add(Convert.ToInt32(tmp[t]));
                    C_sum += C[j][t];
                }
            }

            stream_reader.ReadLine();
            for (int i = 0; i < M; i++)
            {
                D.Add(new List<int>());
                line = stream_reader.ReadLine();
                tmp = line.Split(' ');
                for (int j = 0; j < tmp.Length - 1; j++)
                    D[i].Add(Convert.ToInt32(tmp[j]));
            }

            for (int i = 0; i < N * T; i++)
            {
                X.Add(new List<int>());
                for (int j = 0; j < M * T; j++)
                    X[i].Add(0);
            }
        }
    }
}
