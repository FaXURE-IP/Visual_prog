using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace ColorPickerControl.Views.ColorAssets
{
    public partial class ColorAsset1View : UserControl
    {
        public ColorAsset1View()
        {
            InitializeComponent();
        }

        private void RectangleTappedHandler(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            if (sender is Rectangle rect && rect.DataContext is ViewModels.ColorAssets.BaseColorAssetViewModel vm)
            {
                vm.ChooseColor(rect);
            }
        }
    }
}

