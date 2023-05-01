using System;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Media;
using System.Numerics;
using System.Linq;
using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.CoreAudioApi;
using NAudio;
using System.Threading;
using System.Drawing;
using Spectrogram;

namespace IIS_Spectogram
{
    public partial class Form1 : Form
    {
     
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FillSelect();
        }

        SoundPlayer sp;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                chart1.Series[0].Points.Clear();
                string filePath = Convert.ToString(comboBox1.SelectedItem);

                sp = new SoundPlayer(@"C:/iis/" + filePath);

                sp.PlaySync();

                //draw chart
                byte[] bytes = File.ReadAllBytes(@"C:/iis/" + filePath);
                int start = 44;
                long end = bytes.LongLength;
                int[] res_int = new int[end];
                int h = 1000;
                for (int i = start; i < end; i+=h)
                    res_int[i] = BitConverter.ToInt32(bytes, i)/1000000;

                for (int i = start; i < res_int.Length; i++)
                {
                    chart1.Series[0].Points.Add(res_int[i], i);
                }

                //fill data
                //data = new Data(res_int);
                DrawSpectrogram(filePath);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            sp.Stop();
        }
        
        public void DrawSpectrogram(string filePath)
        {

            /*string file = @"C:/iis/" + filePath;
            if (Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, Handle))
            {
                _handle = Bass.BASS_StreamCreateFile(file, 0, 0, BASSFlag.BASS_DEFAULT);

                if (Bass.BASS_ChannelPlay(_handle, false))
                {
                    _visuals = new Visuals();
                    _timer = new BASSTimer((int)(1.0d / 10 * 1000));
                    _timer.Tick += timer_Tick;
                    _timer.Start();
                }
            }*/
            /*Bitmap picture = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(picture);

            int step = 5;

            for (int i = 0; i < data.Rows; i++)
            {
                for (int j = 0; j < data.Columns; j++)
                {
                    int alpha = (data.Field[i, j] * 255) / max;//not correct

                    g.FillRectangle(new SolidBrush(Color.FromArgb(50, alpha, 255)), i * step, j * step, step, step);
                }
            }*/

            (double[] audio, int sampleRate) = ReadMono(@"C:/iis/" + filePath);
            var sg = new SpectrogramGenerator(sampleRate, fftSize: 4096, stepSize: 500, maxFreq: 3000);
            sg.Add(audio);
            sg.SaveImage("hal.png");
            pictureBox1.Image = sg.GetBitmap();

        }
        private void FillSelect()
        {
            comboBox1.Items.Clear();
            var files = Directory.GetFiles(@"C:/iis/").Select(fn => Path.GetFileName(fn));
            comboBox1.Items.AddRange(files.ToArray());
        }

        /*private void timer_Tick(object sender, EventArgs e)
        {
            bool spectrum3DVoicePrint = _visuals.CreateSpectrum3DVoicePrint(_handle, pictureBox1.CreateGraphics(),
                                                                            pictureBox1.Bounds, Color.Cyan, Color.Green,
                                                                            _pos, false, true);
            _pos++;
            if (_pos >= pictureBox1.Width)
            {
                _pos = 0;
            }
        }*/


        (double[] audio, int sampleRate) ReadMono(string filePath, double multiplier = 16_000)
        {
            var afr = new AudioFileReader(filePath);

            int sampleRate = afr.WaveFormat.SampleRate;
            int bytesPerSample = afr.WaveFormat.BitsPerSample / 8;
            int sampleCount = (int)(afr.Length / bytesPerSample);
            int channelCount = afr.WaveFormat.Channels;
            var audio = new List<double>(sampleCount);
            var buffer = new float[sampleRate * channelCount];
            int samplesRead = 0;
            while ((samplesRead = afr.Read(buffer, 0, buffer.Length)) > 0)
                audio.AddRange(buffer.Take(samplesRead).Select(x => x * multiplier));
            return (audio.ToArray(), sampleRate);


        }

    }
}
