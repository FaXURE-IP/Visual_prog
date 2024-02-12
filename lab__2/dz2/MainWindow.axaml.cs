using Avalonia.Interactivity;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;


namespace dz2;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
    private string[] _colorNames = {
            "Khaki", "Red", "MediumVioletRed", "Bisque", "LemonChiffon",
            "PowderBlue", "MintCream", "Maroon", "RosyBrown", "LightPink"
        };
    private void SetRectangleColor(int index)
    {
        var colorName = _colorNames[index];
        var color = (Color)Color.Parse(colorName);
        ColorRectangle.Fill = new SolidColorBrush(color);
    }
    public void Button_Click(object sender, RoutedEventArgs but)
    {
        var button = (Button)sender;
        var index = int.Parse(button.Tag.ToString());
        SetRectangleColor(index);
    }
}