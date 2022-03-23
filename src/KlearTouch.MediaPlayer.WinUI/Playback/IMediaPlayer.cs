// © 2022 KlearTouch, Pierre Henri KT. Licensed under the MIT license. See the LICENSE.txt file in the project root for more information.

using Windows.Foundation;

namespace Microsoft.UI.Media.Playback;

/// <summary>Provides access to media playback functionality such as play, pause, fast-forward, rewind, and volume.</summary>
internal interface IMediaPlayer
{
    /// <summary>Gets the MediaPlaybackSession associated with the MediaPlayer, which provides information about the state of the current playback session and provides events for responding to changes in playback session state.</summary>
    /// <returns>The MediaPlaybackSession associated with the MediaPlayer.</returns>
    public MediaPlaybackSession PlaybackSession { get; }

    /// <summary>Gets or sets a Boolean value indicating if playback automatically starts after the media is loaded.</summary>
    /// <returns>True is playback start automatically, otherwise false.</returns>
    public bool AutoPlay { get; set; }
    /// <summary>Gets or sets a Boolean value indicating if the media will playback in a repeating loop.</summary>
    /// <returns>True is looping is enabled, otherwise false.</returns>
    public bool IsLoopingEnabled { get; set; }

    /// <summary>Get or sets the audio volume for media playback.</summary>
    /// <returns>The audio volume for media playback. The allowed range of values is 0 to 100. Values outside of this range will be clamped.</returns>
    public double Volume { get; set; }
    /// <summary>Occurs when the volume of the audio has changed.</summary>
    public event TypedEventHandler<MediaPlayer, object?>? VolumeChanged;
    /// <summary>Gets or sets a Boolean value indicating if the audio is muted.</summary>
    /// <returns>True if the audio is muted, otherwise false.</returns>
    public bool IsMuted { get; set; }
    /// <summary>Occurs when the current muted status of the MediaPlayer changes.</summary>
    public event TypedEventHandler<MediaPlayer, object?>? IsMutedChanged;

    /// <summary>Sets the playback source of the media player.</summary>
    /// <returns>The playback source of the media player.</returns>
    public IMediaPlaybackSource? Source { get; set; }
    /// <summary>Occurs when the media source for the MediaPlayer changes.</summary>
    public event TypedEventHandler<MediaPlayer, object?>? SourceChanged;

    /// <summary>Occurs when the media is opened.</summary>
    public event TypedEventHandler<MediaPlayer, object?>? MediaOpened;
    /// <summary>Occurs when the media has finished playback.</summary>
    public event TypedEventHandler<MediaPlayer, object?>? MediaEnded;
    /// <summary>Occurs when an error is encountered.</summary>
    public event TypedEventHandler<MediaPlayer, MediaPlayerFailedEventArgs>? MediaFailed;
    /// <summary>Starts media playback.</summary>
    public void Play();
    /// <summary>Pauses media playback.</summary>
    public void Pause();
}