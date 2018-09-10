using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Habsha_Player.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Now_Playing : Page
    {
        public Now_Playing()
        {
            this.InitializeComponent();
        }

        private async void openfile_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            openPicker.FileTypeFilter.Add(".MP3");
            openPicker.FileTypeFilter.Add(".MP4");

            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);
                mediaElement.SetSource(stream, file.ContentType);
                mediaElement.Play();
            }
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            if(mediaElement.DefaultPlaybackRate !=1)
            {
                mediaElement.DefaultPlaybackRate = 1.0;
            }
            mediaElement.Play();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.DefaultPlaybackRate = -2.0;
            mediaElement.Play();
        }

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.DefaultPlaybackRate = 2.0;
            mediaElement.Play();
        }

        private void Mute_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.IsMuted = !mediaElement.IsMuted;
        }

        private void VolumePlus_Click(object sender, RoutedEventArgs e)
        {
            if(mediaElement.IsMuted)
            {
                mediaElement.IsMuted = false;
            }
            if(mediaElement.Volume>0)
            {
                mediaElement.Volume += .1;
            }
        }

        private void VolumeMinus_Click(object sender, RoutedEventArgs e)
        {
            if (mediaElement.IsMuted)
            {
                mediaElement.IsMuted = false;
            }
            if (mediaElement.Volume < 1)
            {
                mediaElement.Volume -= .1;
            }
        }

        private void Back_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.My_Music));
        }

        private void Element_MediaOpened(object sender, EventArgs e)
        {
            timelineSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

      
        private void Element_MediaEnded(object sender, EventArgs e)
        {
            mediaElement.Stop();
        }

        private void SeekToMediaPosition(object sender, RangeBaseValueChangedEventArgs e)
        {
            
                int SliderValue = (int)timelineSlider.Value;

                TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
                mediaElement.Position = ts;
            

        }
    }
    
}
