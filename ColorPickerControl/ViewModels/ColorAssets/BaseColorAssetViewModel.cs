using ReactiveUI;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace ColorPickerControl.ViewModels.ColorAssets
{
    public class BaseColorAssetViewModel : ViewModelBase
    {
        public BaseColorAssetViewModel(string assetName)
        {
            Header = assetName;
        }

        private Rectangle selectedPaletteColor = null;
        public Rectangle SelectedPaletteColor
        {
            get => selectedPaletteColor;
            set => this.RaiseAndSetIfChanged(ref selectedPaletteColor, value);
        }

        private Rectangle lastSelectedColor = null;
        public Rectangle LastSelectedColor
        {
            get => lastSelectedColor;
            set => this.RaiseAndSetIfChanged(ref lastSelectedColor, value);
        }

        public delegate void ColorSelected(object? sender);
        public event ColorSelected? ColorSelectedEvent;

        public void InvokeColorSelectedEvent()
        {
            ColorSelectedEvent?.Invoke(this);
        }

        private string header;
        public string Header
        {
            get => header;
            protected set => this.RaiseAndSetIfChanged(ref header, value);
        }

        public void ChooseColor(Rectangle color)
        {
            color.Stroke = new SolidColorBrush(Colors.Black);
            color.StrokeThickness = 2;

            if (LastSelectedColor != null)
            {

                if(LastSelectedColor ==  color)
                {
                    color.StrokeThickness = 0;
                    LastSelectedColor = null;
                    return;
                }

                LastSelectedColor.StrokeThickness = 0;
                LastSelectedColor = color;
            }
            else
            {
                LastSelectedColor = color;
            }

            SelectedPaletteColor = color;
            InvokeColorSelectedEvent();
        }
    }
}
