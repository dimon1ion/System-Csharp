using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Task6Form : Form
    {
        Thread thread;
        class Interval
        {
            public int min;
            public int max;
            public string text;
        }
        public Task6Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            int min = Convert.ToInt32(numericUpDownMin.Value);
            int max = Convert.ToInt32(numericUpDownMax.Value);
            int threads = Convert.ToInt32(numericUpDownThreads.Value);
            int nums = (max - min) / threads;

            int mintmp = min;
            int maxtmp = min + nums;

            List<Thread> threads1 = new List<Thread>();

            Interval interval;
            for (int i = 0; i < threads; i++)
            {
                if (i == threads - 1)
                {
                    maxtmp = max + 1;
                }
                interval = new Interval() { min = mintmp, max = maxtmp };
                thread = new Thread(new ParameterizedThreadStart(Task3));
                thread.Start(interval);
                thread.Join();
                textBox1.Text += interval.text;
                mintmp += nums;
                maxtmp += nums;
            }
        }
        private void Task3(object num)
        {
            Interval interval = (num as Interval);
            string text = String.Empty;
            for (int i = interval.min; i < interval.max; i++)
            {
                text += i + " ";
            }
            interval.text = text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
