// © 2022 KlearTouch, Pierre Henri KT. Licensed under the MIT license. See the LICENSE.txt file in the project root for more information.

namespace Microsoft.UI.Xaml.Controls;

/// <summary>Represents the playback controls for a media player element.</summary>
internal interface IMediaTransportControls
{
    /// <summary>Gets or sets a value that indicates whether the seek bar is shown.</summary>
    /// <returns>**true** to show the seek bar. **false** to hide the seek bar. The default is **true**.</returns>
    bool IsSeekBarVisible { get; set; }
    /// <summary>Gets or sets a value that indicates whether a user can use the seek bar to find a location in the media.</summary>
    /// <returns>**true** to allow the user to use the seek bar to find a location; otherwise, **false**. The default is **true**.</returns>
    bool IsSeekEnabled { get; set; }
    /// <summary>Shows the transport controls if they're hidden.</summary>
    void Show();
    /// <summary>Hides the transport controls if they're shown.</summary>
    void Hide();
}