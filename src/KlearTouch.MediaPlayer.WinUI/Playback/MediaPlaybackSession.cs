// © 2022 KlearTouch, Pierre Henri KT. Licensed under the MIT license. See the LICENSE.txt file in the project root for more information.

using System;
using Windows.Foundation;

namespace Microsoft.UI.Media.Playback;

/// <inheritdoc />
public class MediaPlaybackSession : IMediaPlaybackSession
{
    private global::Windows.Media.Playback.MediaPlaybackSession UwpInstance { get; }

    internal MediaPlaybackSession(global::Windows.Media.Playback.MediaPlaybackSession instance)
    {
        UwpInstance = instance;
        UwpInstance.PlaybackStateChanged += (_, _) => PlaybackStateChanged?.Invoke(this, null);
        UwpInstance.PlaybackRateChanged += (_, e) => PlaybackRateChanged?.Invoke(this, null);
        UwpInstance.SeekCompleted += (_, _) => SeekCompleted?.Invoke(this, null);
        UwpInstance.NaturalDurationChanged += (_, _) => NaturalDurationChanged?.Invoke(this, null);
        UwpInstance.PositionChanged += (_, _) => PositionChanged?.Invoke(this, null);
        UwpInstance.NaturalVideoSizeChanged += (_, _) => NaturalVideoSizeChanged?.Invoke(this, null);
    }

    /// <inheritdoc />
    public event TypedEventHandler<MediaPlaybackSession, object?>? PlaybackStateChanged;

    /// <inheritdoc /> 
    public event TypedEventHandler<MediaPlaybackSession, object?>? PlaybackRateChanged;

    /// <inheritdoc />
    public event TypedEventHandler<MediaPlaybackSession, object?>? SeekCompleted;

    /// <inheritdoc />
    public event TypedEventHandler<MediaPlaybackSession, object?>? NaturalDurationChanged;

    /// <inheritdoc />
    public event TypedEventHandler<MediaPlaybackSession, object?>? PositionChanged;

    /// <inheritdoc />
    public event TypedEventHandler<MediaPlaybackSession, object?>? NaturalVideoSizeChanged;

    /// <inheritdoc />
    public TimeSpan NaturalDuration => UwpInstance.NaturalDuration;

    /// <inheritdoc />
    public TimeSpan Position
    {
        get => UwpInstance.Position;
        set => UwpInstance.Position = value;
    }

    /// <inheritdoc />
    public MediaPlaybackState PlaybackState => (MediaPlaybackState)UwpInstance.PlaybackState;

    /// <inheritdoc />
    public bool CanSeek => UwpInstance.CanSeek;

    /// <inheritdoc />
    public bool CanPause => UwpInstance.CanPause;

    /// <inheritdoc />
    public double PlaybackRate
    {
        get => UwpInstance.PlaybackRate;
        set => UwpInstance.PlaybackRate = value;
    }

    /// <inheritdoc />
    public uint NaturalVideoHeight => UwpInstance.NaturalVideoHeight;

    /// <inheritdoc />
    public uint NaturalVideoWidth => UwpInstance.NaturalVideoWidth;
}