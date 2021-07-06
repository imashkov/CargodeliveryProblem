using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spec_Laba_4
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] test_file_paths = { 
                @"task_4_01_n2_m2_T2.txt", 
                @"task_4_02_n2_m2_T2.txt", 
                @"task_4_03_n3_m2_T2.txt", 
                @"task_4_04_n3_m2_T2.txt", 
                @"task_4_05_n20_m15_T6.txt",
                @"task_4_06_n20_m15_T6.txt",
                @"task_4_07_n30_m15_T12.txt",
                @"task_4_08_n30_m15_T12.txt",
                @"task_4_09_n50_m20_T24.txt", 
                @"task_4_10_n50_m20_T24.txt" };

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("|{0, 11}{0, 11}{0, 11}{0, 11}{0, 11}", "|".PadLeft(11, '-'));
            Console.WriteLine("|{0, 10}|{1, 10}|{2, 10}|{3, 10}|{4, 10}|",
                "Test", "FF","Min. Cap.", "Stor. Base", "Stor. My");
            Console.WriteLine("|{0, 11}{0, 11}{0, 11}{0, 11}{0, 11}", "|".PadLeft(11, '-'));

            for (int i = 0; i < 10; i++)
            {
                ClassicTask task = new ClassicTask(test_file_paths[i]);
                ISolver solver = new Solver();
                Console.ForegroundColor -= 0;
                Console.Write("|{0, 10}|", i + 1);
                Console.Write("{0,10}|", solver.SolveByFordFulkerson(task));
                Console.Write("{0,10}|", solver.MinimizeStorageCapacity(task));
                Console.Write("{0,10}|", solver.MinimizeStorageCount(task, new BaseStrategy()));
                Console.Write("{0,10}|", solver.MinimizeStorageCount(task, new MyStrategy()));
                Console.WriteLine();
            }
            Console.WriteLine("|{0, 11}{0, 11}{0, 11}{0, 11}{0, 11}", "|".PadLeft(11, '-'));
            Console.ReadKey();
        }
    }
}
