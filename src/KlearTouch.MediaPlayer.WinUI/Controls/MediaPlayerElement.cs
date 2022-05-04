// © 2022 KlearTouch, Pierre Henri KT. Licensed under the MIT license. See the LICENSE.txt file in the project root for more information.

using System;
using UwpMediaPlayer = Windows.Media.Playback.MediaPlayer;
#if WINDOWS_UWP
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
#else
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
#endif
using Microsoft.UI.Media.Playback;

#pragma warning disable CS0618 // 'UwpMediaPlayer' is obsolete

namespace Microsoft.UI.Xaml.Controls;

/// <summary>Represents an object that uses a MediaPlayer to render audio and video to the display.</summary>
public class MediaPlayerElement : UserControl, IMediaPlayerElement
{
    private SwapChainPanel SwapChainPanel { get; }
    private SwapChainSurface? SwapChainSurface { get; set; }

    /// <summary>Gets the UWP MediaPlayer.</summary>
    [Obsolete("Access the full UWP API at your own risk. The MediaPlayer API may be different in the future WinUI version.")]
    public UwpMediaPlayer? UwpMediaPlayer => MediaPlayer?.UwpInstance;

    /// <inheritdoc />
    public MediaPlayer? MediaPlayer
    {
        get => TransportControls.MediaPlayer;
        private set => TransportControls.MediaPlayer = value;
    }

    /// <inheritdoc />
    public MediaTransportControls TransportControls { get; }

    /// <summary>Identifies the AreTransportControlsEnabled dependency property.</summary>
    /// <returns>The identifier for the AreTransportControlsEnabled dependency property.</returns>
    public static DependencyProperty AreTransportControlsEnabledProperty { get; } =
        DependencyProperty.Register(nameof(AreTransportControlsEnabled), typeof(bool), typeof(MediaPlayerElement), new(false))!;

    /// <inheritdoc />
    public bool AreTransportControlsEnabled
    {
        get => (bool)GetValue(AreTransportControlsEnabledProperty);
        set => SetValue(AreTransportControlsEnabledProperty, value);
    }


    /// <summary>Initializes a new instance of the MediaPlayerElement class.</summary>
    public MediaPlayerElement()
    {
        HorizontalAlignment = HorizontalAlignment.Stretch;
        VerticalAlignment = VerticalAlignment.Stretch;
        IsTabStop = false;

        Grid layoutRoot = new() { Background = new SolidColorBrush(Colors.Transparent) };

        layoutRoot.Children!.Add(SwapChainPanel = new());

        Border transportControlsPresenter = new();
        layoutRoot.Children.Add(transportControlsPresenter);
        transportControlsPresenter.SetBinding(VisibilityProperty!, new Binding { Source = this, Path = new(nameof(AreTransportControlsEnabled)) });
        transportControlsPresenter.Child = TransportControls = new();

        Content = layoutRoot;
    }

    /// <inheritdoc />
    public void SetMediaPlayer(MediaPlayer? mediaPlayer)
    {
        if (MediaPlayer == mediaPlayer)
            return;
        if (MediaPlayer is not null) // Dispose the previous one
        {
            SwapChainSurface?.Dispose();
            SwapChainSurface = null;
            UwpMediaPlayer!.VideoFrameAvailable -= OnVideoFrameAvailable;
            UwpMediaPlayer.Dispose();
        }
        MediaPlayer = mediaPlayer;
        if (MediaPlayer is not null)
        {
            SwapChainSurface = new(SwapChainPanel, OnResize);
            UwpMediaPlayer!.IsVideoFrameServerEnabled = true;
            UwpMediaPlayer.VideoFrameAvailable += OnVideoFrameAvailable;
        }
    }

    private void OnResize() // if (MediaPlayer is paused) we need to ask it to re-render a frame // TODO: Find a better solution
    {
        var oneFrameTimeAt24Fps = TimeSpan.FromSeconds(1.0 / 24.0);
        if (MediaPlayer?.PlaybackSession.PlaybackState is not MediaPlaybackState.Paused) return;
        var duration = MediaPlayer.PlaybackSession.NaturalDuration;
        if (duration < oneFrameTimeAt24Fps * 2) return;
        if (MediaPlayer.PlaybackSession.Position < oneFrameTimeAt24Fps || MediaPlayer.PlaybackSession.Position + oneFrameTimeAt24Fps <= duration)
        {
            MediaPlayer.PlaybackSession.Position += oneFrameTimeAt24Fps;
            MediaPlayer.PlaybackSession.Position -= oneFrameTimeAt24Fps;
        }
        else
        {
            MediaPlayer.PlaybackSession.Position -= oneFrameTimeAt24Fps;
            MediaPlayer.PlaybackSession.Position += oneFrameTimeAt24Fps;
        }
    }

    private void OnVideoFrameAvailable(UwpMediaPlayer sender, object? args)
    {
        SwapChainSurface?.OnNewSurfaceAvailable(sender.CopyFrameToVideoSurface);
    }
}