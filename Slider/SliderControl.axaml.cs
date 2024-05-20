using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Styling;
using System;

namespace Slider;

public partial class SliderControl : UserControl
{
    public SliderControl()
    {
        InitializeComponent();
    }

    private Animation SmoothSwitch(double state)
    {
        return new Animation
        {
            Easing = new SplineEasing(0.5, 0, 0.5, 1),
            Children =
                {
                    new KeyFrame
                    {
                        Setters = { new Setter { Property = OpacityProperty, Value = state } },
                        Cue = new Cue(1)
                    }
                },
            Duration = TimeSpan.FromSeconds(0.5),
            FillMode = FillMode.Forward
        };
    }

    public async void OpenEvent(object sender, RoutedEventArgs e)
    {
        if(OpenButton.Opacity == 1.0)
        {
            await SmoothSwitch(0.0).RunAsync(OpenButton);
            await SmoothSwitch(1.0).RunAsync(Slider);
        }
    }

    public async void CloseEvent(object sender, RoutedEventArgs e)
    {
        if(Slider.Opacity == 1.0)
        {
            await SmoothSwitch(0.0).RunAsync(Slider);
            await SmoothSwitch(1.0).RunAsync(OpenButton);
        }
    }
}