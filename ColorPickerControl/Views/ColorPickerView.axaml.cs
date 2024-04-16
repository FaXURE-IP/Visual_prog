using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using ColorPickerControl.ViewModels;
using ColorPickerControl.Services;
using Avalonia.Media;
using System;
using ColorPickerControl.ViewModels.ColorAssets;

namespace ColorPickerControl.Views
{
    public partial class ColorPickerView : UserControl
    {
        public ColorPickerView()
        {
            InitializeComponent();
            PointerPressed += OnPointerPressed;
        }
        private void PaletteTappedHandler(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            if(sender is Image palleteImage && palleteImage.DataContext is ColorPickerViewModel colorPicker)
            {
                var absolutePoint = this.PointToScreen(colorPicker.ClickCoord);

                colorPicker.SelectedColor = "#" +  
                    GetPixelColor.GetColorAt(new System.Drawing.Point(absolutePoint.X,absolutePoint.Y)).Name;

                Services.
                ColorConverter.
                HexToRGBConvert(colorPicker.SelectedColor, colorPicker.RgbViewModel);
            }
        }

        private void OnPointerPressed(object sender, PointerPressedEventArgs e)
        {
            if(sender is Control window && window.DataContext is ColorPickerViewModel colorPicker)
            {
                Point position = e.GetPosition(this);
                colorPicker.ClickCoord = position;
            }
        }

        private void TextBoxKeyPressedHandler(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            if(sender is TextBox textBox && textBox.DataContext is ColorPickerViewModel colorPicker)
            {
                if (!colorPicker.IsValidRGBValue(textBox.Text + e.KeySymbol))
                {
                    e.Handled = true;
                }
            }
        }

        private void AddToPalleteButtonClickHandler(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ColorPickerViewModel colorPicker)
            {
                if(colorPicker.SelectedColor != null && colorPicker.SelectedAsset.SelectedPaletteColor != null)
                {
                    var color = Color.Parse(colorPicker.SelectedColor);
                    Brush brush = new SolidColorBrush(color);
                    colorPicker.SelectedAsset.SelectedPaletteColor.Fill = brush;
                }
            }
        }

        private void NewPaletteButtonClickHandler(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if(sender is Button button && button.DataContext is ColorPickerViewModel colorPicker)
            {
                colorPicker.AddNewPalette(new EmptyColorAssetViewModel("#Asset " + 
                    (colorPicker.ColorAssets.Count + 1).ToString()));
            }
        }

        private void DeletePaletteButtonClickHandler(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if(sender is Button button && button.DataContext is ColorPickerViewModel colorPicker)
            {
                colorPicker.ColorAssets.Remove(colorPicker.SelectedAsset);
            }
        }

        private void GenerateColorClickHandler(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is ColorPickerViewModel colorPicker)
            {
                if(RedValueTextBox.Text != string.Empty && GreenValueTextBox.Text != string.Empty
                    && BlueValueTextBox.Text != string.Empty)
                {
                    colorPicker.RgbViewModel.Red = Convert.ToByte(RedValueTextBox.Text);
                    colorPicker.RgbViewModel.Green = Convert.ToByte(GreenValueTextBox.Text);
                    colorPicker.RgbViewModel.Blue = Convert.ToByte(BlueValueTextBox.Text);

                    colorPicker.HexColor = 
                        ColorConverter.
                        RGBToHexConvert(colorPicker.RgbViewModel);

                    colorPicker.SelectedColor = colorPicker.HexColor;
                }
            }
        }
    }
}

