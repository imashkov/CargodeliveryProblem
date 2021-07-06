using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spec_Laba_4
{
    interface ISolver
    {
        int SolveByFordFulkerson(ClassicTask task);
        int MinimizeStorageCapacity(ClassicTask task);
        int MinimizeStorageCount(ClassicTask task, IStrategy strategy);
    }
    interface IFordFulkerson
    {
        int FindMaxFlow(ClassicTask task);
    }
    interface IStorageCapacity
    {
        int MinimizeStorageCapacity(ClassicTask task);
    }
    interface IStorageCount
    {
        int MinimizeStorageCount(ClassicTask task, IStrategy strategy);
    }
    interface IStrategy
    {
        List<Consumers> FillConsumers(List<Consumers> consumers);
    }
    class BaseStrategy : IStrategy
    {
        public List<Consumers> FillConsumers(List<Consumers> consumers)
        {
            consumers = consumers.OrderBy(p => p.c).ToList();
            return consumers;
        }
    }
    class MyStrategy : IStrategy
    {
        public List<Consumers> FillConsumers(List<Consumers> consumers)
        {
            consumers = consumers.OrderBy(p => p.c).ToList();
            Consumers temp;
            for(int i = 1; i < consumers.Count; i += 2)
            {
                if (consumers.Count % 2 == 0)
                    break;
                temp = consumers[i];
                consumers[i] = consumers[i + 1];
                consumers[i + 1] = temp;
            }
            return consumers;
        }
    }
    class Solver : ISolver
    {
        public int SolveByFordFulkerson(ClassicTask task)
        {
            IFordFulkerson solution_by_FF = new FordFulkerson();
            return solution_by_FF.FindMaxFlow(task);
        }
        public int MinimizeStorageCapacity(ClassicTask task)
        {
            IStorageCapacity min_capacity_solution = new StorageCapacity();
            return min_capacity_solution.MinimizeStorageCapacity(task);
        }
        public int MinimizeStorageCount(ClassicTask task, IStrategy strategy)
        {
            IStorageCount min_stors_base_solution = new StorageCount();
            return min_stors_base_solution.MinimizeStorageCount(task, strategy);
        }
    }
    class FordFulkerson : IFordFulkerson
    {
        public int FindMaxFlow(ClassicTask task)
        {
            int dimension = task.matrix.size;
            int start_ver = 0;
            int flow = 0;
            int[,] matrix = new int[dimension, dimension];
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    matrix[i, j] = task.matrix.matrix[i, j];
                }
            }
            for (;;)
            {
                int current_flow = FindPath(matrix, new bool[dimension], start_ver, int.MaxValue);
                if (current_flow == 0)
                    return flow;
                flow += current_flow;
            }
        }

        int FindPath(int[,] matrix, bool[] visited_vertices, int current_ver, int flow)
        {
            if (current_ver == matrix.GetLength(0) - 1)
                return flow;
            visited_vertices[current_ver] = true;
            for (int next_ver = 0; next_ver < matrix.GetLength(0); next_ver++)
            {
                if (!visited_vertices[next_ver] && matrix[current_ver, next_ver] > 0)
                {
                    int next_flow = FindPath(matrix, visited_vertices, next_ver, Math.Min(flow, matrix[current_ver, next_ver]));
                    if (next_flow > 0)
                    {
                        matrix[current_ver, next_ver] -= next_flow;
                        matrix[next_ver, current_ver] += next_flow;
                        return next_flow;
                    }
                }
            }
            return 0;
        }
    }

    class StorageCapacity : IStorageCapacity
    {
        public int MinimizeStorageCapacity(ClassicTask task)
        {
            int current_Z = 0, Z = 0, 
                lower_bound = 0, upper_bound = task.A_sum;
            FordFulkerson ff = new FordFulkerson();
            while (lower_bound < upper_bound - 1)
            {
                current_Z = (lower_bound + upper_bound) / 2;
                task.matrix.AddStores(current_Z);

                if (ff.FindMaxFlow(task) < task.C_sum)
                    lower_bound = current_Z;
                else
                {
                    upper_bound = current_Z;
                    Z = current_Z;
                }
            }
            return  Z;
        }
    }

    class StorageCount : IStorageCount
    {
        public int MinimizeStorageCount(ClassicTask task, IStrategy strategy)
        {
            FordFulkerson ff = new FordFulkerson();
            task.matrix.AddStores(task.A_sum);
            List<Consumers> consumers = new List<Consumers>();
            for (int i = 0; i < task.M; i++)
            {
                consumers.Add(new Consumers(i, task.C[i].Sum()));
            }
            consumers = strategy.FillConsumers(consumers);
            int storage_counter = 0;
            do
            {
                task.matrix.DeleteStore(consumers[storage_counter].a);
                storage_counter++;
            }
            while ((ff.FindMaxFlow(task) == task.C_sum) && !(storage_counter == task.M));
            consumers.Clear();
            return (task.M - storage_counter + 1);
        }
    }

    public class Consumers
    {
        public int a;
        public int c;

        public Consumers(int a, int c)
        {
            this.a = a;
            this.c = c;
        }
    }
}