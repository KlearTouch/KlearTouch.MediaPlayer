// © 2022 KlearTouch, Pierre Henri KT. Licensed under the MIT license. See the LICENSE.txt file in the project root for more information.

using System;
using Microsoft.UI.Xaml;
using FastPlayFallbackBehaviour = Microsoft.UI.Xaml.Media.FastPlayFallbackBehaviour;
using MediaSource = Windows.Media.Core.MediaSource;

namespace KlearTouchApp.MediaPlayer.WinUI
{
    public sealed partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Title = "Sample using WinUI 3 MediaPlayerElement (XAML)";
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

            if (MediaPlayerElement!.MediaPlayer is null) MediaPlayerElement.SetMediaPlayer(new());

            var item = new Windows.Media.Playback.MediaPlaybackItem(MediaSource.CreateFromStorageFile(file)!);
            var props = item.GetDisplayProperties()!;
            props.Type = Windows.Media.MediaPlaybackType.Video;
            props.VideoProperties!.Title = file.DisplayName;
            props.VideoProperties.Subtitle = System.IO.Path.GetDirectoryName(file.Path);
            item.ApplyDisplayProperties(props);

            MediaPlayerElement.MediaPlayer!.AutoPlay = true;
            MediaPlayerElement.MediaPlayer.Source = item;
        }

        private void OpenUri(object sender, RoutedEventArgs e)
        {
            if (MediaPlayerElement!.MediaPlayer is null) MediaPlayerElement.SetMediaPlayer(new());

            MediaPlayerElement.MediaPlayer!.AutoPlay = true;
            MediaPlayerElement.MediaPlayer.Source = MediaSource.CreateFromUri(new("https://sec.ch9.ms/ch9/8791/ab3e65c2-619c-4d36-b7ca-abd0499a8791/WinUI.mp4", UriKind.Absolute));
        }

        private void Play(object sender, RoutedEventArgs e) => MediaPlayerElement!.MediaPlayer?.Play();

        private void Pause(object sender, RoutedEventArgs e) => MediaPlayerElement!.MediaPlayer?.Pause();

        private void CleanUpMediaPlayer(object sender, RoutedEventArgs e) => MediaPlayerElement!.SetMediaPlayer(null!);

        private void ToggleButtonsVisibility(object sender, RoutedEventArgs e)
        {
            var mtc = MediaPlayerElement!.TransportControls!;
            //mtc.IsFullWindowButtonVisible = mtc.IsFullWindowEnabled = mtc.IsCompactOverlayButtonVisible = mtc.IsCompactOverlayEnabled =
            mtc.IsZoomButtonVisible = mtc.IsZoomEnabled = mtc.IsFastForwardButtonVisible = mtc.IsFastForwardEnabled = mtc.IsFastRewindButtonVisible = mtc.IsFastRewindEnabled = mtc.IsStopButtonVisible = mtc.IsStopEnabled =
                mtc.IsVolumeButtonVisible = mtc.IsVolumeEnabled = mtc.IsPlaybackRateButtonVisible = mtc.IsPlaybackRateEnabled = mtc.IsSeekBarVisible = mtc.IsSeekEnabled =
                    mtc.IsSkipForwardButtonVisible = mtc.IsSkipForwardEnabled = mtc.IsSkipBackwardButtonVisible = mtc.IsSkipBackwardEnabled = mtc.IsNextTrackButtonVisible = mtc.IsPreviousTrackButtonVisible =
                        mtc.IsRepeatEnabled = mtc.IsRepeatButtonVisible = !mtc.IsRepeatButtonVisible;
            mtc.FastPlayFallbackBehaviour = FastPlayFallbackBehaviour.Disable;
        }

        private void ToggleAutoHide(object sender, RoutedEventArgs e) => MediaPlayerElement!.TransportControls!.ShowAndHideAutomatically = !MediaPlayerElement.TransportControls.ShowAndHideAutomatically;

        private void ToggleCompact(object sender, RoutedEventArgs e) => MediaPlayerElement!.TransportControls!.IsCompact = !MediaPlayerElement.TransportControls.IsCompact;
    }
}