// © 2022 KlearTouch, Pierre Henri KT. Licensed under the MIT license. See the LICENSE.txt file in the project root for more information.

using System;
using Windows.Foundation;
using UwpMediaPlayer = Windows.Media.Playback.MediaPlayer;
using UwpMediaSource = Windows.Media.Core.MediaSource;
using Microsoft.UI.Media.Core;

namespace Microsoft.UI.Media.Playback;

/// <inheritdoc />
public class MediaPlayer : IMediaPlayer
{
    internal UwpMediaPlayer UwpInstance { get; }

    /// <summary>Initializes a new instance of the MediaPlayer object.</summary>
    public MediaPlayer()
    {
        UwpInstance = new();
        PlaybackSession = new(UwpInstance.PlaybackSession!);

        UwpInstance.VolumeChanged += (_, _) => VolumeChanged?.Invoke(this, null);
        UwpInstance.IsMutedChanged += (_, _) => IsMutedChanged?.Invoke(this, null);
        UwpInstance.SourceChanged += (_, _) => SourceChanged?.Invoke(this, null);
        UwpInstance.MediaOpened += (_, _) => MediaOpened?.Invoke(this, null);
        UwpInstance.MediaEnded += (_, _) => MediaEnded?.Invoke(this, null);
        UwpInstance.MediaFailed += (_, e) => MediaFailed?.Invoke(this, new((MediaPlayerError)e.Error, e.ErrorMessage ?? string.Empty, e.ExtendedErrorCode));
    }

    /// <inheritdoc />
    public MediaPlaybackSession PlaybackSession { get; }

    /// <inheritdoc />
    public bool AutoPlay
    {
        get => UwpInstance.AutoPlay;
        set => UwpInstance.AutoPlay = value;
    }

    /// <inheritdoc />
    public bool IsLoopingEnabled
    {
        get => UwpInstance.IsLoopingEnabled;
        set => UwpInstance.IsLoopingEnabled = value;
    }

    /// <inheritdoc />
    public double Volume
    {
        get => UwpInstance.Volume;
        set => UwpInstance.Volume = value;
    }

    /// <inheritdoc />
    public event TypedEventHandler<MediaPlayer, object?>? VolumeChanged;

    /// <inheritdoc />
    public bool IsMuted
    {
        get => UwpInstance.IsMuted;
        set => UwpInstance.IsMuted = value;
    }

    /// <inheritdoc />
    public event TypedEventHandler<MediaPlayer, object?>? IsMutedChanged;

    private IMediaPlaybackSource? _source;
    /// <inheritdoc />
    public IMediaPlaybackSource? Source
    {
        get => _source;
        set
        {
            UwpInstance.Source = value switch
            {
                null => null,
                FileMediaSource ms => UwpMediaSource.CreateFromStorageFile(ms.File),
                UriMediaSource ms => UwpMediaSource.CreateFromUri(ms.Uri),
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
            _source = value;
        }
    }

    /// <inheritdoc />
    public event TypedEventHandler<MediaPlayer, object?>? SourceChanged;

    /// <inheritdoc />
    public event TypedEventHandler<MediaPlayer, object?>? MediaOpened;

    /// <inheritdoc />
    public event TypedEventHandler<MediaPlayer, object?>? MediaEnded;

    /// <inheritdoc />
    public event TypedEventHandler<MediaPlayer, MediaPlayerFailedEventArgs>? MediaFailed;

    /// <inheritdoc />
    public void Play()
    {
        UwpInstance.Play();
    }

    /// <inheritdoc />
    public void Pause()
    {
        UwpInstance.Pause();
    }
}