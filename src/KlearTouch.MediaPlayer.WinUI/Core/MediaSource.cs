// © 2022 KlearTouch, Pierre Henri KT. Licensed under the MIT license. See the LICENSE.txt file in the project root for more information.

using System;
#if WINDOWS_UWP
using IStorageFile = Windows.Storage.IStorageFile;
#else
using IStorageFile = Windows.Storage.IStorageFile;
#endif

using Microsoft.UI.Media.Playback;

namespace Microsoft.UI.Media.Core;

/// <summary>Represents a media source. Provides a common way to reference media from different sources and exposes a common model for accessing media data regardless of the underlying media format.</summary>
public static class MediaSource
{
    /// <summary>Creates an instance of MediaSource from the provided IStorageFile.</summary>
    /// <param name="file">The IStorageFile from which the MediaSource is created.</param>
    /// <returns>The new media source.</returns>
    public static IMediaPlaybackSource CreateFromStorageFile(IStorageFile file) => new FileMediaSource(file);

    /// <summary>Creates an instance of MediaSource from the provided Uri.</summary>
    /// <param name="uri">The URI from which the MediaSource is created.</param>
    /// <returns>The new media source.</returns>
    public static IMediaPlaybackSource CreateFromUri(Uri uri) => new UriMediaSource(uri);
}

internal class FileMediaSource : IMediaPlaybackSource
{
    public IStorageFile File { get; }

    public FileMediaSource(IStorageFile file)
    {
        File = file;
    }
}

internal class UriMediaSource : IMediaPlaybackSource
{
    public Uri Uri { get; }

    public UriMediaSource(Uri uri)
    {
        Uri = uri;
    }
}