using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test_4
{
    public partial class Form1 : Form
    {
        const int N = 1024; // количество разбиений
        double[] InnerArray = new double[N];
        Complex[] Furie = new Complex[N];

        public class Complex
        {
            public double Re;// Реальная часть
            public double Im; // Мнимая
            public double Amplitude; // Амплитуда для АЧХ
            public double Faza; // Фаза для ФЧХ
            public double Frecuensy; // Частота гармоники
        }
        public Form1()
        {
            for (int i = 0; i < N; i++)
                InnerArray[i] = 5 * Math.Sin(2 * Math.PI * i / 100) * Math.Cos(2 * Math.PI * i / N); // задаем форму сигнала

            DPF(InnerArray);
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        void DPF(double[] Inner)
        {
            int N = Inner.Length;
            double Arg;

            for (int k = 0; k < N; k++)
            {
                Furie[k] = new Complex();
                for (int n = 0; n < Inner.Length; n++)
                {
                    Arg = 2 * Math.PI * k * n / N;
                    Furie[k].Re += Inner[n] * Math.Cos(Arg);
                    Furie[k].Im -= Inner[n] * Math.Sin(Arg);
                }
                Furie[k].Amplitude = (Math.Sqrt(Math.Pow(Furie[k].Re, 2) + Math.Pow(Furie[k].Im, 2))) / N;
                Furie[k].Faza = Math.Atan(Furie[k].Im / Furie[k].Re / Math.PI * 180);
                Furie[k].Frecuensy = ((N - 1) * (k));

            }
        }
    }
}
