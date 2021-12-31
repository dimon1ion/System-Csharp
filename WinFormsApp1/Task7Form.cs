using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Task7Form : Form
    {
        class Task4_5
        {
            public int[] arr;
            public int res;
        }

        public Task7Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
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
            textBox1.Text += $"Minimal number => {min.res}" +
                $"\n\n   Average number => {avg.res}" +
                $"\n\n   Maximum number => {max.res}";
            threadRecord.Start(new int[] { min.res, avg.res, max.res });
            MessageBox.Show("Recorded to hello.txt");
        }

        private void Task4min(object _arr)
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
        private void Task4avg(object _arr)
        {
            Task4_5 arrClass = _arr as Task4_5;
            int[] arr = arrClass.arr;
            Array.Sort(arr);
            int avg = arr[arr.Length / 2];
            arrClass.res = avg;
            Console.WriteLine($"Average number => {avg}");
        }
        private void Task4max(object _arr)
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
        private void Task5Record(object results)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
