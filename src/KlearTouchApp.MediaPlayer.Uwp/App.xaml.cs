// © 2022 KlearTouch, Pierre Henri KT. Licensed under the MIT license. See the LICENSE.txt file in the project root for more information.

using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace KlearTouchApp.MediaPlayer.Uwp;

public sealed partial class App
{
    public App()
    {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs e)
    {
        if (Window.Current!.Content is not Frame rootFrame)
        {
            rootFrame = new();
            Window.Current.Content = rootFrame;
        }

        if (!e.PrelaunchActivated)
        {
            if (rootFrame.Content is null)
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            Window.Current.Activate();
        }
    }
}