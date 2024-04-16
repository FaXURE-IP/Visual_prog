using Avalonia.Controls;
using ColorPickerControl.ViewModels.ColorAssets;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace ColorPickerControl.ViewModels
{
    public class ColorPickerViewModel : ViewModelBase
    {
        private ObservableCollection<BaseColorAssetViewModel> colorAssets = 
            [];

        public ObservableCollection<BaseColorAssetViewModel> ColorAssets
        {
            get => colorAssets;
            private set => this.RaiseAndSetIfChanged(ref colorAssets, value);
        }

        private BaseColorAssetViewModel selectedAsset;
        public BaseColorAssetViewModel SelectedAsset
        {
            get => selectedAsset;
            set => this.RaiseAndSetIfChanged(ref selectedAsset, value);
        }

        private RGBViewModel _RgbViewModel = new RGBViewModel();
        public RGBViewModel RgbViewModel
        {
            get => _RgbViewModel;
            set => this.RaiseAndSetIfChanged(ref _RgbViewModel, value);
        }

        private Avalonia.Point clickCoord;
        public Avalonia.Point ClickCoord
        {
            get => clickCoord;
            set => clickCoord = value;
        }

        private Avalonia.Media.Imaging.Bitmap drawingPalette;
        public Avalonia.Media.Imaging.Bitmap DrawingPalette
        {
            get => drawingPalette;
            private set => drawingPalette = value;
        }

        public ColorPickerViewModel(string selectedColorValue,string paletteImagePath)
        {
            ColorAssets.Add(new ColorAsset1ViewModel("#Asset 1"));

            ColorAssets[0].ColorSelectedEvent += ColorAssetViewModelColorSelectedEventHandler;

            SelectedAsset = ColorAssets[0];

            if (File.Exists(paletteImagePath))
            {
                drawingPalette = new Avalonia.Media.Imaging.Bitmap(paletteImagePath);
            }
            else
            {
                throw new FileNotFoundException();
            }

            SelectedColor = selectedColorValue;
            RgbViewModel.StateChangedEvent += RgbViewModelChangedEventHandler;
        }

        private void RgbViewModelChangedEventHandler()
        {
            HexColor =
                ColorPickerControl.
                Services.
                ColorConverter.
                RGBToHexConvert(RgbViewModel);

            SelectedColor = HexColor;
        }

        private void ColorAssetViewModelColorSelectedEventHandler(object? sender)
        {
            if(sender is BaseColorAssetViewModel baseColorAssetViewModel)
            {
                SelectedColor = baseColorAssetViewModel.SelectedPaletteColor.Fill.ToString();

                ColorPickerControl.
                    Services.
                    ColorConverter.
                    HexToRGBConvert(SelectedColor,RgbViewModel);

                HexColor = SelectedColor;
            }
        }

        private string selectedColor = "#000000";
        public string SelectedColor
        {
            get => selectedColor;
            set => this.RaiseAndSetIfChanged(ref selectedColor, value);
        }

        public void AddNewPalette(BaseColorAssetViewModel newPalette)
        {
            newPalette.ColorSelectedEvent += ColorAssetViewModelColorSelectedEventHandler;
            ColorAssets.Add(newPalette);
        }

        private string hexColor = "#000000";
        public string HexColor
        {
            get => hexColor;
            set => this.RaiseAndSetIfChanged(ref hexColor, value);
        }
        public bool IsValidRGBValue(string value)
        {
            if (value.Length == 2 && value[0] == '0')
            {
                return false;
            }

            if (int.TryParse(value, out int intValue))
            {
                return intValue >= 0 && intValue <= 255;
            }

            return false;
        }

        public void GenerateColor(byte r,byte g,byte b)
        {
            RgbViewModel.Red = r;
            RgbViewModel.Green = g;
            RgbViewModel.Blue = b;

            HexColor =
                Services.
                ColorConverter.
                RGBToHexConvert(RgbViewModel);

            SelectedColor = HexColor;
        }
    }
}
