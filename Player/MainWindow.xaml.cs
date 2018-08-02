using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Timers;
using System.Windows.Threading;


namespace Player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();

            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
           
        }

        // to do
        private void timerTick(object sender, EventArgs e)
        {
            sldRewind.Value = media.Position.TotalSeconds;
            if (media != null)
                tbFromSrart.Text = String.Format("{0:00}:{1:00}:{2:00}", media.Position.Hours, media.Position.Minutes, media.Position.Seconds);
            tbRemaining.Text = String.Format("{0:00}:{1:00}:{2:00}", media.Position.Hours-media.BufferingProgress, media.Position.Minutes, media.Position.Seconds);
        }

        private void Button_Click_Stop(object sender, RoutedEventArgs e)
        {
            media.Stop();
            btnPlay.IsEnabled = true;
        }

        private void Button_Click_Play(object sender, RoutedEventArgs e)
        {
            media.Play();
        }

        private void Button_Click_Open(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "(*.avi; *.mp4;)| *.avi; *.mp4; |(All files) |*.*";
            fileDialog.ShowDialog();
            this.Title = fileDialog.SafeFileName;
            media.Source = new Uri(fileDialog.FileName);
            media.LoadedBehavior = MediaState.Manual;
            media.MediaOpened += new RoutedEventHandler(mEl_MediaOpened);
            media.MediaEnded += new RoutedEventHandler(mEl_Ended);
            lbPlaylist.SelectionMode = SelectionMode.Single;
            lbPlaylist.Items.Add(fileDialog.FileName);
            btnPlay.IsEnabled = false;            
            sldRewind.Value = 0;
            media.Play();  
            timer.Start();

            }

        public void mEl_MediaOpened(object sender, RoutedEventArgs e)
        {
            double absvalue = media.NaturalDuration.TimeSpan.TotalSeconds;
            sldRewind.Maximum = absvalue;
            tbRemaining.Text =  media.NaturalDuration.TimeSpan.ToString();
        }
        public void mEl_Ended(object sender, RoutedEventArgs e)
        {
            media.Stop();
            sldRewind.Value = 0;
            btnPlay.IsEnabled = true;
            
        }

        private void Slider_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            media.Pause();
            media.Position = TimeSpan.FromSeconds(sldRewind.Value);
            media.Play();
        }

        private void Slider_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            media.Pause();
            media.Position = TimeSpan.FromSeconds(sldRewind.Value);
            media.Play();
        }
        
        private void Media_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
       
        }
     
        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (media != null)
                media.Volume = (double)sldVolume.Value;
        }

        private void lbPlaylist_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            btnPlay.IsEnabled = false;
            timer.Start();
            media.Source = new Uri(lbPlaylist.SelectedItem.ToString());
            media.Play();
        }

        private void media_MediaOpened(object sender, RoutedEventArgs e)
        {
         
        }
        }

        
    }

