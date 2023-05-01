using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.CoreAudioApi;
using NAudio;
using System.Media;
using System.Numerics;
using System.Linq;

namespace IIS_test_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FillSelect();
        }

        WaveIn waveIn;
        WaveFileWriter waveWriter;


        private void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveWriter == null) return;

            waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
            waveWriter.Flush();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            waveIn = new WaveIn();
            waveIn.DeviceNumber = 0;
            string outputFilename = @"C:/iis/";
            outputFilename += textBox1.Text + ".wav";
            if (outputFilename != @"C:/iis/")
            {
                waveIn.WaveFormat = new WaveFormat(100000, WaveIn.GetCapabilities(waveIn.DeviceNumber).Channels);

                waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(waveIn_DataAvailable);
                waveWriter = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                waveIn.StartRecording();
            }
            else
            {
                MessageBox.Show("Ви не ввели назву файлу.");
            }
        }
        SoundPlayer sp;
        private void button2_Click(object sender, EventArgs e)
        {

            if (waveIn != null)
            {
                waveIn.StopRecording();
                waveIn.Dispose();
                waveIn = null;
            }
            if (waveWriter != null)
            {
                waveWriter.Dispose();
                waveWriter = null;
            }
            FillSelect();
        }
        private void start_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = Convert.ToString(comboBox1.SelectedItem);
                sp = new SoundPlayer(@"C:/iis/" + filePath);
                
                sp.Play();
                

            }
            catch
            {
                MessageBox.Show("Виникла помилка");
            }
        }

        private void stop_Click(object sender, EventArgs e)
        {
            sp.Stop();
        }

        private void FillSelect()
        {
            comboBox1.Items.Clear();
            var files = Directory.GetFiles(@"C:/iis/").Select(fn => Path.GetFileName(fn));

            comboBox1.Items.AddRange(files.ToArray());


        }
    }
}