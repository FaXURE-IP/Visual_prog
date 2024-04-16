using Avalonia.Controls;
using Avalonia.Controls.Shapes;

namespace ColorPickerControl.Views.ColorAssets
{
    public partial class EmptyColorAssetView : UserControl
    {
        public EmptyColorAssetView()
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

