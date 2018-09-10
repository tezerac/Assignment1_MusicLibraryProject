using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Media.Editing;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;



namespace Habsha_Player.Pages
{
      public sealed partial class AddPlaylist : Page
    {
        private MediaComposition composition;


        public AddPlaylist()
        {
            this.InitializeComponent();

        }
        private async void PlaylistImage(object sender, TappedRoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            StorageFile file = await openPicker.PickSingleFileAsync();

            if (file != null)
            {
                var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
                var Image = new BitmapImage();
                Image.SetSource(stream);
                ImagePlace.Children.Add(new Image() { Source = Image, Width = 300, Height = 300 });
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var msg = new MessageDialog("Are You sure close the playlist").ShowAsync();


        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {

        }
        private ObservableCollection<Habsha_Music> MusicList1 = new ObservableCollection<Habsha_Music>();
        async private void btnSelectMusic_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker fileOpenPicker1 = new FileOpenPicker();
            fileOpenPicker1.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            fileOpenPicker1.FileTypeFilter.Add(".mp3");
            fileOpenPicker1.FileTypeFilter.Add(".mp4");
            fileOpenPicker1.FileTypeFilter.Add(".Wav");
            fileOpenPicker1.FileTypeFilter.Add(".Mkv");
            fileOpenPicker1.FileTypeFilter.Add(".AVI");
            StorageFile storagefile1 = await fileOpenPicker1.PickSingleFileAsync();
            if (storagefile1 != null)


            {

                var musicProperties = await storagefile1.Properties.GetMusicPropertiesAsync();
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
                MusicList1.Add(new Habsha_Music
                {
                    Title = title,
                    Artist = artist,
                    Album = album,
                    Duration = musicdur,


                });

            }
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {


            if (!String.IsNullOrEmpty(txtPlaylistName.Text))
            {
                composition = new MediaComposition();

               FileSavePicker fileSavePicker = new FileSavePicker();
                fileSavePicker.SuggestedStartLocation = PickerLocationId.Downloads;
                fileSavePicker.FileTypeChoices.Add("Wpl", new List<string>() { ".wpl" });
                fileSavePicker.FileTypeChoices.Add("zpl", new List<string>() { ".zpl" });
                fileSavePicker.FileTypeChoices.Add("m3u", new List<string>() { ".m3u" });
                fileSavePicker.SuggestedFileName = "Playlist_" + txtPlaylistName.Text;


                Windows.Storage.StorageFile pickedFile = await fileSavePicker.PickSaveFileAsync();
                StorageFolder folder = KnownFolders.MusicLibrary;

                // var storageItemAccessList = Windows.Storage.AccessCache.StorageApplicationPermissions.FutureAccessList;
                // storageItemAccessList.Add(pickedFile);

                //var clip = await MediaClip.CreateFromFileAsync(pickedFile);
                //composition.Clips.Add(clip);


                if (pickedFile != null)
                {
                    try
                    {
                        myPlaylist.IsEnabled = true;

                    }
                    catch (Exception ex)
                    {
                        MessageDialog messageDialog = new MessageDialog(ex.Message);
                        await messageDialog.ShowAsync();
                        myPlaylist.IsEnabled = false;

                    }
                }                


            }

        }

    }
}




        

   
