// © 2022 KlearTouch, Pierre Henri KT. Licensed under the MIT license. See the LICENSE.txt file in the project root for more information.

using System;

namespace Microsoft.UI.Media.Playback;

/// <summary>Provides the data for MediaFailed events.</summary>
public sealed class MediaPlayerFailedEventArgs : EventArgs
{
    /// <summary>Gets the MediaPlayerError value for the error that triggered the event.</summary>
    /// <returns>The MediaPlayerError value for the error that triggered the event.</returns>
    public MediaPlayerError Error { get; }
    /// <summary>Gets a string describing the error that occurred.</summary>
    /// <returns>String describing the error that occurred.</returns>
    public string ErrorMessage { get; }
    /// <summary>Gets an HResult that indicates any extra data about the error that occurred.</summary>
    /// <returns>An HResult that indicates any extra data about the error that occurred.</returns>
    public Exception? ExtendedErrorCode { get; }

    internal MediaPlayerFailedEventArgs(MediaPlayerError error, string errorMessage, Exception? extendedErrorCode)
    {
        Error = error;
        ErrorMessage = errorMessage;
        ExtendedErrorCode = extendedErrorCode;
    }
}

/// <summary>Indicates possible media player errors.</summary>
public enum MediaPlayerError // Important: Must match MediaPlaybackItemErrorCode to convert them easily
{
    /// <summary>The error is unknown.</summary>
    Unknown,
    /// <summary>The last operation was aborted.</summary>
    Aborted,
    /// <summary>A network error occurred.</summary>
    NetworkError,
    /// <summary>A media decoding error occurred.</summary>
    DecodingError,
    /// <summary>The media type is not supported.</summary>
    SourceNotSupported
}