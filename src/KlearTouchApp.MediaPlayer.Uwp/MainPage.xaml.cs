// © 2022 KlearTouch, Pierre Henri KT. Licensed under the MIT license. See the LICENSE.txt file in the project root for more information.

//#define NATIVE // Use the native UWP MediaPlayerElement
using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
#if NATIVE
using MediaSource = Windows.Media.Core.MediaSource;
#else
using MediaPlayerElement = Microsoft.UI.Xaml.Controls.MediaPlayerElement;
using MediaSource = Microsoft.UI.Media.Core.MediaSource;
#endif

namespace KlearTouchApp.MediaPlayer.Uwp
{
    public sealed partial class MainPage
    {
        private MediaPlayerElement MediaPlayerElement { get; }

        public MainPage()
        {
            InitializeComponent();

            Windows.UI.ViewManagement.ApplicationView.GetForCurrentView()!.SetPreferredMinSize(new(192, 48));

            Root!.Children!.Add(MediaPlayerElement = new MediaPlayerElement { AreTransportControlsEnabled = true });

            var controls = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top, Background = new SolidColorBrush(Color.FromArgb(0x99, 0, 0, 0)) };
            Root.Children.Add(controls);

            var openFile = new Button { Content = "Open File..." };
            openFile.Click += (_, _) => OpenFile();
            controls.Children!.Add(openFile);

            var openUri = new Button { Content = "Open Uri" };
            openUri.Click += (_, _) => OpenUri();
            controls.Children.Add(openUri);

            var play = new Button { Content = "Play" };
            play.Click += (_, _) => MediaPlayerElement.MediaPlayer?.Play();
            controls.Children.Add(play);

            var pause = new Button { Content = "Pause" };
            pause.Click += (_, _) => MediaPlayerElement.MediaPlayer?.Pause();
            controls.Children.Add(pause);

            var cleanUpMediaPlayer = new Button { Content = "CleanUp MediaPlayer" };
            cleanUpMediaPlayer.Click += (_, _) => MediaPlayerElement.SetMediaPlayer(null!);
            controls.Children.Add(cleanUpMediaPlayer);

            var toggleTransportControls = new Button { Content = "Toggle TransportControls" };
            toggleTransportControls.Click += (_, _) => MediaPlayerElement.AreTransportControlsEnabled = !MediaPlayerElement.AreTransportControlsEnabled;
            controls.Children.Add(toggleTransportControls);
        }

        private async void OpenFile()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
#if !WINDOWS_UWP
            WinRT.Interop.InitializeWithWindow.Initialize(picker, WinRT.Interop.WindowNative.GetWindowHandle(this));
#endif
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.VideosLibrary;
            picker.FileTypeFilter!.Add(".avi");
            picker.FileTypeFilter.Add(".mkv");
            picker.FileTypeFilter.Add(".mp4");

            var file = await picker.PickSingleFileAsync()!;
            if (file is null) return;

            if (MediaPlayerElement.MediaPlayer is null) MediaPlayerElement.SetMediaPlayer(new());

            MediaPlayerElement.MediaPlayer!.AutoPlay = true;
            MediaPlayerElement.MediaPlayer.Source = MediaSource.CreateFromStorageFile(file);
        }

        private void OpenUri()
        {
            if (MediaPlayerElement.MediaPlayer is null) MediaPlayerElement.SetMediaPlayer(new());

            MediaPlayerElement.MediaPlayer!.AutoPlay = true;
            MediaPlayerElement.MediaPlayer.Source = MediaSource.CreateFromUri(new("https://sec.ch9.ms/ch9/8791/ab3e65c2-619c-4d36-b7ca-abd0499a8791/WinUI.mp4", UriKind.Absolute));
        }
    }
}