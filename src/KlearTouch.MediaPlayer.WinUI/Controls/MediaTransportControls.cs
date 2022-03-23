// © 2022 KlearTouch, Pierre Henri KT. Licensed under the MIT license. See the LICENSE.txt file in the project root for more information.

using System;
using System.Globalization;
#if WINDOWS_UWP
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
#else
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Windows.UI;
#endif
using Microsoft.UI.Media.Playback;

namespace Microsoft.UI.Xaml.Controls;

/// <summary>Represents the playback controls for a media player element.</summary>
public class MediaTransportControls : UserControl, IMediaTransportControls
{
    private MediaPlayer? _mediaPlayer;
    internal MediaPlayer? MediaPlayer
    {
        get => _mediaPlayer;
        set
        {
            if (ReferenceEquals(_mediaPlayer, value)) return;
            if (_mediaPlayer is not null)
            {
                _mediaPlayer.PlaybackSession.PlaybackStateChanged -= OnPlaybackSessionOnPlaybackStateChanged;
                _mediaPlayer.PlaybackSession.NaturalDurationChanged -= OnNaturalDurationChanged;
                _mediaPlayer.PlaybackSession.PositionChanged -= OnPositionChanged;
            }
            _mediaPlayer = value;
            if (_mediaPlayer is not null)
            {
                _mediaPlayer.PlaybackSession.PlaybackStateChanged += OnPlaybackSessionOnPlaybackStateChanged;
                _mediaPlayer.PlaybackSession.NaturalDurationChanged += OnNaturalDurationChanged;
                _mediaPlayer.PlaybackSession.PositionChanged += OnPositionChanged;
            }
        }
    }

    /// <summary>Identifies the IsSeekBarVisible dependency property.</summary>
    /// <returns>The identifier for the IsSeekBarVisible dependency property.</returns>
    public static DependencyProperty IsSeekBarVisibleProperty { get; } =
        DependencyProperty.Register(nameof(IsSeekBarVisible), typeof(bool), typeof(MediaTransportControls), new(true, OnIsSeekBarVisibleChanged))!;

    /// <inheritdoc />
    public bool IsSeekBarVisible
    {
        get => (bool)GetValue(IsSeekBarVisibleProperty);
        set => SetValue(IsSeekBarVisibleProperty, value);
    }

    /// <summary>Identifies the IsSeekEnabled dependency property.</summary>
    /// <returns>The identifier for the IsSeekEnabled dependency property.</returns>
    public static DependencyProperty IsSeekEnabledProperty { get; } =
        DependencyProperty.Register(nameof(IsSeekEnabled), typeof(bool), typeof(MediaTransportControls), new(true, OnIsSeekEnabledChanged))!;

    /// <inheritdoc />
    public bool IsSeekEnabled
    {
        get => (bool)GetValue(IsSeekEnabledProperty);
        set => SetValue(IsSeekEnabledProperty, value);
    }

    private static void OnIsSeekBarVisibleChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        ((MediaTransportControls)dependencyObject).TimelineBorder.Visibility = (bool)args.NewValue ? Visibility.Visible : Visibility.Collapsed;
    }

    private static void OnIsSeekEnabledChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        ((MediaTransportControls)dependencyObject).ProgressSlider.IsEnabled = (bool)args.NewValue;
    }

    /// <inheritdoc />
    public void Show()
    {
        Opacity = 1;
    }

    /// <inheritdoc />
    public void Hide()
    {
        Opacity = 0;
    }



    private Border TimelineBorder { get; }
    private Slider ProgressSlider { get; }
    private TextBlock TimeRemainingElement { get; }
    private TextBlock TimeElapsedElement { get; }
    private FontIcon PlayPauseSymbol { get; }

    internal MediaTransportControls()
    {
        const double buttonSize = 48;

        FlowDirection = FlowDirection.LeftToRight;
        UseSystemFocusVisuals = true;

        var rootGrid = new Grid { Background = new SolidColorBrush(Colors.Transparent) };

        var controlPanelGrid = new Grid { Background = new SolidColorBrush(Color.FromArgb(0x99, 0, 0, 0)), VerticalAlignment = VerticalAlignment.Bottom };
        rootGrid.Children!.Add(controlPanelGrid);

        controlPanelGrid.RowDefinitions!.Add(new RowDefinition { Height = new(1, GridUnitType.Star) });
        controlPanelGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        TimelineBorder = new();
        Grid.SetRow(TimelineBorder, 0);
        controlPanelGrid.Children!.Add(TimelineBorder);

        Grid timelineGrid = new();
        TimelineBorder.Child = timelineGrid;

        timelineGrid.ColumnDefinitions!.Add(new ColumnDefinition { Width = GridLength.Auto });
        timelineGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new(1, GridUnitType.Star) });
        timelineGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        ProgressSlider = new Slider { Height = 32, VerticalAlignment = VerticalAlignment.Center, IsThumbToolTipEnabled = false };
        ProgressSlider.ValueChanged += OnProgressSliderValueChanged;
        Grid.SetColumn(ProgressSlider, 1);
        timelineGrid.Children!.Add(ProgressSlider);

        TimeRemainingElement = new TextBlock { VerticalAlignment = VerticalAlignment.Center, Foreground = new SolidColorBrush(Colors.White), FontSize = 12, Margin = new(12, 0, 12, 0) };
        Grid.SetColumn(TimeRemainingElement, 0);
        timelineGrid.Children.Add(TimeRemainingElement);
        TimeElapsedElement = new TextBlock { VerticalAlignment = VerticalAlignment.Center, Foreground = new SolidColorBrush(Colors.White), FontSize = 12, Margin = new(12, 0, 12, 0) };
        Grid.SetColumn(TimeElapsedElement, 2);
        timelineGrid.Children.Add(TimeElapsedElement);

        var commandBar = new Grid { Height = buttonSize, Background = new SolidColorBrush(Colors.Transparent) };
        Grid.SetRow(commandBar, 1);
        controlPanelGrid.Children.Add(commandBar);

        var playPauseButton = new AppBarButton { Width = buttonSize, Height = buttonSize, Icon = PlayPauseSymbol = new FontIcon { Glyph = "\xE102" }, HorizontalAlignment = HorizontalAlignment.Center };
        playPauseButton.Click += OnPlayPause;
        commandBar.Children!.Add(playPauseButton);

        Content = rootGrid;
    }

    private void OnProgressSliderValueChanged(object sender, RangeBaseValueChangedEventArgs e)
    {
        if (_mediaPlayer is null)
            return;
        var newPosition = TimeSpan.FromSeconds(e.NewValue);
        var diff = _mediaPlayer.PlaybackSession.Position - newPosition;
        if (Math.Abs(diff.TotalSeconds) > 1)
            _mediaPlayer.PlaybackSession.Position = newPosition;
    }

    private void OnPlayPause(object sender, RoutedEventArgs e)
    {
        if (_mediaPlayer is null)
            return;
        if (_mediaPlayer.PlaybackSession.PlaybackState is MediaPlaybackState.Buffering or MediaPlaybackState.Playing)
            _mediaPlayer.Pause();
        else
            _mediaPlayer.Play();
    }

    private void OnPlaybackSessionOnPlaybackStateChanged(MediaPlaybackSession sender, object? args)
    {
#if WINDOWS_UWP
        _ = Dispatcher?.TryRunAsync(CoreDispatcherPriority.Normal, () =>
#else
        DispatcherQueue?.TryEnqueue(() =>
#endif
        {
            PlayPauseSymbol.Glyph = sender.PlaybackState is MediaPlaybackState.None or MediaPlaybackState.Opening or MediaPlaybackState.Paused ? "\xE102" : "\xE103";
        });
    }

    private void OnNaturalDurationChanged(IMediaPlaybackSession sender, object? args)
    {
#if WINDOWS_UWP
        _ = Dispatcher?.TryRunAsync(CoreDispatcherPriority.Normal, () =>
#else
        DispatcherQueue?.TryEnqueue(() =>
#endif
        {
            ProgressSlider.Minimum = 0;
            ProgressSlider.Maximum = sender.NaturalDuration.TotalSeconds;
        });
    }

    private void OnPositionChanged(IMediaPlaybackSession sender, object? args)
    {
#if WINDOWS_UWP
        _ = Dispatcher?.TryRunAsync(CoreDispatcherPriority.Normal, () =>
#else
        DispatcherQueue?.TryEnqueue(() =>
#endif
        {
            var duration = sender.NaturalDuration;
            var elapsed = sender.Position;
            var remaining = elapsed - duration;
            TimeRemainingElement.Text = ToString(remaining);
            TimeElapsedElement.Text = ToString(elapsed) + " / " + ToString(duration);
            ProgressSlider.Value = elapsed.TotalSeconds;
        });
    }

    private static string ToString(TimeSpan value)
    {
        value = new TimeSpan(value.Ticks - value.Ticks % TimeSpan.TicksPerSecond); // Remove sub-second precision
        return value.ToString("g", CultureInfo.CurrentCulture);
    }
}