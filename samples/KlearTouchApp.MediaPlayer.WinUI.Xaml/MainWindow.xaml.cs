// © 2022 KlearTouch, Pierre Henri KT. Licensed under the MIT license. See the LICENSE.txt file in the project root for more information.

using System;
using Microsoft.UI.Xaml;
using MediaSource = Microsoft.UI.Media.Core.MediaSource;

namespace KlearTouchApp.MediaPlayer.WinUI
{
    public class MediaPlayerElement : Microsoft.UI.Xaml.Controls.MediaPlayerElement { }

    public sealed partial class MainWindow
    {
        public Microsoft.UI.Xaml.Controls.MediaPlayerElement MediaPlayerElement { get; } = new() { AreTransportControlsEnabled = true };

        public MainWindow()
        {
            InitializeComponent();

            Title = "KlearTouch MediaPlayer for WinUI 3 (XAML)";

            MediaPlayerElementContainer!.Child = MediaPlayerElement;
        }

        private async void OpenFile(object sender, RoutedEventArgs e)
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

        private void OpenUri(object sender, RoutedEventArgs e)
        {
            if (MediaPlayerElement.MediaPlayer is null) MediaPlayerElement.SetMediaPlayer(new());

            MediaPlayerElement.MediaPlayer!.AutoPlay = true;
            MediaPlayerElement.MediaPlayer.Source = MediaSource.CreateFromUri(new("https://sec.ch9.ms/ch9/8791/ab3e65c2-619c-4d36-b7ca-abd0499a8791/WinUI.mp4", UriKind.Absolute));
        }

        private void Play(object sender, RoutedEventArgs e) => MediaPlayerElement.MediaPlayer?.Play();

        private void Pause(object sender, RoutedEventArgs e) => MediaPlayerElement.MediaPlayer?.Pause();

        private void CleanUpMediaPlayer(object sender, RoutedEventArgs e) => MediaPlayerElement.SetMediaPlayer(null!);

        private void ToggleTransportControls(object sender, RoutedEventArgs e) => MediaPlayerElement.AreTransportControlsEnabled = !MediaPlayerElement.AreTransportControlsEnabled;
    }
}