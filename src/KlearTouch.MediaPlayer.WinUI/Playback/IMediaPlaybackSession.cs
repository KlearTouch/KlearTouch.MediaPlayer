// © 2022 KlearTouch, Pierre Henri KT. Licensed under the MIT license. See the LICENSE.txt file in the project root for more information.

using System;
using Windows.Foundation;

namespace Microsoft.UI.Media.Playback;

/// <summary>Provides information about the state of the current playback session of a MediaPlayer and provides events for responding to changes in playback session state.</summary>
internal interface IMediaPlaybackSession
{
    /// <summary>Occurs when the current playback state changes.</summary>
    event TypedEventHandler<MediaPlaybackSession, object?>? PlaybackStateChanged;
    /// <summary>Occurs when the current playback rate for the MediaPlaybackSession changes.</summary>
    event TypedEventHandler<MediaPlaybackSession, object?>? PlaybackRateChanged;
    /// <summary>Occurs when a seek operation for the MediaPlaybackSession completes.</summary>
    event TypedEventHandler<MediaPlaybackSession, object?>? SeekCompleted;
    /// <summary>Occurs when the duration of the currently playing media item changes.</summary>
    event TypedEventHandler<MediaPlaybackSession, object?>? NaturalDurationChanged;
    /// <summary>Occurs when the current playback position within the currently playing media changes.</summary>
    event TypedEventHandler<MediaPlaybackSession, object?>? PositionChanged;
    /// <summary>Occurs when the size of the video in the currently playing media item changes.</summary>
    event TypedEventHandler<MediaPlaybackSession, object?>? NaturalVideoSizeChanged;
    /// <summary>Gets a value indicating the duration of the currently playing media, when being played back at normal speed.</summary>
    /// <returns>The duration of the currently playing media.</returns>
    TimeSpan NaturalDuration { get; }
    /// <summary>Gets or sets the current playback position within the currently playing media.</summary>
    /// <returns>The current playback position within the currently playing media.</returns>
    TimeSpan Position { get; set; }
    /// <summary>Gets a value indicating the current playback state of the MediaPlaybackSession, such as buffering or playing.</summary>
    /// <returns>The current playback state of the MediaPlaybackSession</returns>
    MediaPlaybackState PlaybackState { get; }
    /// <summary>Gets a value that indicates whether the current playback position of the media can be changed by setting the value of the MediaPlayer.Position property.</summary>
    /// <returns>True if the current playback position of the media can be changed; otherwise, false.</returns>
    bool CanSeek { get; }
    /// <summary>Gets a value that indicates whether media can be paused if the MediaPlayer.Pause method is called.</summary>
    /// <returns>True if the media can be paused; otherwise, false.</returns>
    bool CanPause { get; }
    /// <summary>Gets or sets a value representing the current playback rate for the MediaPlaybackSession.</summary>
    /// <returns>The current playback rate for the MediaPlaybackSession.</returns>
    double PlaybackRate { get; set; }
    uint NaturalVideoHeight { get; }
    /// <summary>Gets the width of the video in the currently playing media item.</summary>
    /// <returns>The width of the video in the currently playing media item, in pixels.</returns>
    uint NaturalVideoWidth { get; }
}

/// <summary>Specifies the playback state of a MediaPlaybackSession.</summary>
public enum MediaPlaybackState
{
    /// <summary>No current state.</summary>
    None,
    /// <summary>A media item is opening.</summary>
    Opening,
    /// <summary>A media item is buffering.</summary>
    Buffering,
    /// <summary>A media item is playing.</summary>
    Playing,
    /// <summary>Playback of a media item is paused.</summary>
    Paused
}