using Microsoft.Win32;
using NAudio.Wave;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace MPlay
{
    public partial class MainWindow : Window
    {
        // Инициализируем NAudio
        bool IsPause = false;
        public WaveOutEvent outputDevice;
        private AudioFileReader audioFile;
        public MainWindow()
        {

        }
        // Тут открываем файлы
        string OFD()
        {
            OpenFileDialog ofds = new OpenFileDialog();
            ofds.ShowDialog();
            return ofds.FileName;

        }
        // Что происходит после открытия файла 
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            string path = OFD();
            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
            }
            if (audioFile == null)
            {
                audioFile = new AudioFileReader(path);
                outputDevice.Init(audioFile);
            }
            else
            {
                outputDevice.Stop();
                audioFile = new AudioFileReader(path);
                outputDevice.Init(audioFile);
            }
            outputDevice.Play();
            double sl = (Math.Round(audioFile.TotalTime.TotalSeconds)); // Длительность файла для полоски
            TimeSpan tt = TimeSpan.FromSeconds(sl);
            End.Text = tt.ToString(); // Длительность файла для текста
            SliderBar.Maximum = sl;
            SliderBar.Value = 0;
            CreateTimer(); // обновление таймера
            var tfile = TagLib.File.Create(path); // taglib для имени и исполнителя
            if (tfile.Tag.FirstAlbumArtist != null)
            {
                MusicLabel.Text = tfile.Tag.Title;
                MusicArtist.Text = tfile.Tag.FirstAlbumArtist;
            }
            else
            {
                MusicLabel.Text = System.IO.Path.GetFileName(path);
            }
            PB.Content = "Воспроизводится";
            IsPause = false;
        }

        private void CB_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Length_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void pp(object sender, RoutedEventArgs e) // Паузаааааа...
        {
            if (IsPause == false)
            {
                outputDevice?.Pause();
                IsPause = true;
                PB.Content = "Приостановлено";
            }
            else
            {
                outputDevice?.Play();
                IsPause = false;
                PB.Content = "Воспроизводится";
            }
        }

        public async void CreateTimer()
        {
            TimeSpan ts = new TimeSpan(0);
            while (true)
            {
                await Task.Delay(1000);
                Start.Text = ((TimeSpan.FromSeconds(Math.Round(audioFile.CurrentTime.TotalSeconds))).ToString());
                MTime.Value = Convert.ToInt32(audioFile.CurrentTime.TotalSeconds);
                MTime.Maximum = Convert.ToInt32(audioFile.TotalTime.TotalSeconds);
            }
        }

        private void SliderBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            audioFile.CurrentTime = TimeSpan.FromSeconds(SliderBar.Value);
            MTime.Value = Convert.ToInt32(SliderBar.Value);
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                this.outputDevice.Volume = (float)VolumeSlider.Value;
            }
           catch (Exception ex)
            {

            }
        }
    }
}
