using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using WinFormsApp1;

namespace Work_System_Csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadStart startTask1 = new ThreadStart(Task1);
            Thread task1 = new Thread(startTask1);
            task1.Start();
            task1.Join();

            {
                Console.WriteLine("Task 2");
                ParameterizedThreadStart startTask2 = new ParameterizedThreadStart(Task2);
                Thread task2 = new Thread(startTask2);

                Console.Write("Enter min => ");
                int min = Int32.Parse(Console.ReadLine());
                Console.Write("Enter max => ");
                int max = Int32.Parse(Console.ReadLine());
                Nums nums = new Nums() { min = min, max = max };
                task2.Start(nums);

                task2.Join();
            }

            Console.WriteLine();

            {
                Console.WriteLine("Task 3");
                // 3
                Console.Write("Enter threads => ");
                int threads = Int32.Parse(Console.ReadLine());
                Console.Write("Enter min => ");
                int min = Int32.Parse(Console.ReadLine());
                Console.Write("Enter max => ");
                int max = Int32.Parse(Console.ReadLine());

                int nums = (max - min) / threads;

                int mintmp = min;
                int maxtmp = min + nums;

                List<Thread> threads1 = new List<Thread>();

                Thread thread;
                for (int i = 0; i < threads; i++)
                {
                    if (i == threads - 1)
                    {
                        maxtmp = max + 1;
                    }
                    thread = new Thread(new ParameterizedThreadStart(Task3));
                    thread.Start(new Interval() { min = mintmp, max = maxtmp });
                    thread.Join();
                    mintmp += nums;
                    maxtmp += nums;
                }
            }

            Console.WriteLine();

            {
                Console.WriteLine("Task 4");
                // 4
                int[] arr = new int[10000];

                Random rnd = new Random();
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = rnd.Next(0, 10000);
                }
                Thread threadmin = new Thread(new ParameterizedThreadStart(Task4min));
                Thread threadavg = new Thread(new ParameterizedThreadStart(Task4avg));
                Thread threadmax = new Thread(new ParameterizedThreadStart(Task4max));
                Task4_5 min = new Task4_5() { arr = arr };
                Task4_5 avg = new Task4_5() { arr = arr };
                Task4_5 max = new Task4_5() { arr = arr };
                threadmin.Start(min);
                threadavg.Start(avg);
                threadmax.Start(max);
                threadmin.Join();
                threadavg.Join();
                threadmax.Join();
                Console.WriteLine();
            }

            Console.WriteLine();

            {
                Console.WriteLine("Task 5");
                // 5
                int[] arr = new int[10000];

                Random rnd = new Random();
                for (int i = 0; i < arr.Length; i++)
                {
                    arr[i] = rnd.Next(0, 10000);
                }
                Thread threadmin = new Thread(new ParameterizedThreadStart(Task4min));
                Thread threadavg = new Thread(new ParameterizedThreadStart(Task4avg));
                Thread threadmax = new Thread(new ParameterizedThreadStart(Task4max));
                Thread threadRecord = new Thread(new ParameterizedThreadStart(Task5Record));
                Task4_5 min = new Task4_5() { arr = arr };
                Task4_5 avg = new Task4_5() { arr = arr };
                Task4_5 max = new Task4_5() { arr = arr };
                threadmin.Start(min);
                threadavg.Start(avg);
                threadmax.Start(max);
                threadmin.Join();
                threadavg.Join();
                threadmax.Join();
                Console.WriteLine();
                threadRecord.Start(new int[] { min.res, avg.res, max.res });
                Console.WriteLine("Recorded to hello.txt");
            }

            Console.WriteLine("");

            {
                // 6
                Console.WriteLine("Task 6");            
                Task6Form form = new Task6Form();
                form.ShowDialog();
            }

            {
                // 7
                Console.WriteLine("Task 7");
                Task7Form form = new Task7Form();
                form.ShowDialog();
            }


        }

        private static void Task1()
        {
            for (int i = 0; i <= 50; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }

        private static void Task2(object num)
        {
            Nums nums = (Nums)num;

            for (int i = nums.min; i <= nums.max; i++)
            {
                Console.Write(i + " ");
            }
        }
        private static void Task3(object num)
        {
            Interval interval = (num as Interval);

            for (int i = interval.min; i < interval.max; i++)
            {
                Console.Write(i + " ");
            }
        }
        private static void Task4min(object _arr)
        {
            Task4_5 arrClass = _arr as Task4_5;
            int[] arr = arrClass.arr;
            int min = arr[0];
            for (int i = 0; i < arr.Length; i++)
            {
                if (min > arr[i])
                {
                    min = arr[i];
                }
            }
            arrClass.res = min;
            Console.WriteLine($"Minimal number => {min}");
        }
        private static void Task4avg(object _arr)
        {
            Task4_5 arrClass = _arr as Task4_5;
            int[] arr = arrClass.arr;
            Array.Sort(arr);
            int avg = arr[arr.Length / 2];
            arrClass.res = avg;
            Console.WriteLine($"Average number => {avg}");
        }
        private static void Task4max(object _arr)
        {
            Task4_5 arrClass = _arr as Task4_5;
            int[] arr = arrClass.arr;
            int max = arr[0];
            for (int i = 0; i < arr.Length; i++)
            {
                if (max < arr[i])
                {
                    max = arr[i];
                }
            }
            arrClass.res = max;
            Console.WriteLine($"Maximum number => {max}");
        }
        private static void Task5Record(object results)
        {
            int[] result = results as int[];
            Array.Sort(result);
            using (FileStream fs = new FileStream("hello.txt", FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"Minimal => {result[0]}");
                    sw.WriteLine($"Average => {result[1]}");
                    sw.WriteLine($"Maximum => {result[2]}");
                }
            }
        }

    }
    public struct Nums
    {
        public int min;
        public int max;
    }

    class Interval
    {
        public int min;
        public int max;
    }

    class Task4_5
    {
        public int[] arr;
        public int res;
    }
}
