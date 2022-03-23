// © 2022 KlearTouch, Pierre Henri KT. Licensed under the MIT license. See the LICENSE.txt file in the project root for more information.

using Microsoft.UI.Media.Playback;

namespace Microsoft.UI.Xaml.Controls;

/// <summary>Represents an object that uses a MediaPlayer to render audio and video to the display.</summary>
internal interface IMediaPlayerElement
{
    /// <summary>Gets the MediaPlayer instance used to render media.</summary>
    /// <returns>The MediaPlayer instance used to render media.</returns>
    MediaPlayer? MediaPlayer { get; }
    /// <summary>Gets the transport controls for the media.</summary>
    /// <returns>The transport controls for the media.</returns>
    MediaTransportControls TransportControls { get; }
    /// <summary>Gets or sets a value that determines whether the standard transport controls are enabled.</summary>
    /// <returns>**true** if the standard transport controls are enabled; otherwise, **false**. The default is **false**.</returns>
    bool AreTransportControlsEnabled { get; set; }
    /// <summary>Sets the MediaPlayer instance used to render media.</summary>
    /// <param name="mediaPlayer">The new MediaPlayer instance used to render media.</param>
    void SetMediaPlayer(MediaPlayer? mediaPlayer);
}