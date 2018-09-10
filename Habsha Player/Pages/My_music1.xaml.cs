using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Habsha_Player.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class My_music1 : Page
    {
        private ObservableCollection<Habsha_Music> MusicList = new ObservableCollection<Habsha_Music>();
        // String filterfile = "(*.mp3; *.wav; *.mkv; *.avi; *.mp4; *.mkv; *.3gp; *.flv; *.ifo; *.vob;)|*.mp3; *.wav; *.mkv; *.avi; *.mp4; *.mkv; *.3gp; *.flv; *.ifo; *.vob;";
       
        public My_music1()
        {
            
            this.InitializeComponent();
        }

       
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
           StorageFolder musicLib = KnownFolders.MusicLibrary;
        
               
          var files = await musicLib.GetFilesAsync();
            

            foreach (var file in files)
            {
                StorageItemThumbnail currentThumb = await file.GetThumbnailAsync(ThumbnailMode.MusicView, 30, ThumbnailOptions.UseCurrentScale);
                var albumCover = new BitmapImage();
                albumCover.SetSource(currentThumb);
                

                var musicProperties = await file.Properties.GetMusicPropertiesAsync();
                var musicname = musicProperties.Title;
                var musicdur = musicProperties.Duration;


                var title = musicProperties.Title;
                if (title == "")
                {
                    title = "Unknown Title";
                }

                var artist = musicProperties.Artist;
                if (artist == "")
                {
                    artist = "Unknown Artist";
                }

                var album = musicProperties.Album;
                if (album == "")
                {
                    album = "Unknown Album";
                }
                MusicList.Add(new Habsha_Music
                {
                    Title = title,
                    Artist = artist,
                    Album = album,
                    Duration = musicdur,
                    AlbumCover = albumCover,
                    
                });

            }
        }

        private void mylist_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
