using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using System;
using System.Reactive.Linq;

namespace TwoWindows
{
    public static class WindowFactory
    {
        public static IObservable<RoutedEventArgs> CreateObservable(Window window)
        {
            IObservable<RoutedEventArgs> windowOpenedEvent = Observable.FromEventPattern<RoutedEventArgs>(
                h => window.AddHandler(Window.WindowOpenedEvent, h),
                h => window.RemoveHandler(Window.WindowOpenedEvent, h))
                .Select(ep => ep.EventArgs);

            IObservable<RoutedEventArgs> windowClosedEvent = Observable.FromEventPattern<RoutedEventArgs>(
                h => window.AddHandler(Window.WindowClosedEvent, h),
                h => window.RemoveHandler(Window.WindowClosedEvent, h))
                .Select(ep => ep.EventArgs);

            IObservable<RoutedEventArgs> buttonEvent = Observable.FromEventPattern<RoutedEventArgs>(
                h => window.AddHandler(Button.ClickEvent, h),
                h => window.RemoveHandler(Button.ClickEvent, h))
                .Select(ep => ep.EventArgs);

            IObservable<RoutedEventArgs> inputEvent = Observable.FromEventPattern<RoutedEventArgs>(
                h => window.AddHandler(TextBox.TextChangedEvent, h),
                h => window.RemoveHandler(TextBox.TextChangedEvent, h))
                .Select(ep => ep.EventArgs);

            IObservable<RoutedEventArgs> sliderEvent = Observable.FromEventPattern<RoutedEventArgs>(
                h => window.AddHandler(RangeBase.ValueChangedEvent, h),
                h => window.RemoveHandler(RangeBase.ValueChangedEvent, h))
                .Select(ep => ep.EventArgs);

            return windowOpenedEvent
            .Merge(windowClosedEvent)
            .Merge(buttonEvent)
            .Merge(inputEvent)
            .Merge(sliderEvent);
        }
    }
}