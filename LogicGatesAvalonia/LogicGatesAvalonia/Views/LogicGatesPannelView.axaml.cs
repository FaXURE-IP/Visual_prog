using Avalonia.Controls;

using LogicGatesAvalonia.ViewModels;
using LogicGatesAvinternal.Controls;

namespace LogicGatesAvalonia.Views
{
    public partial class LogicGatesPannelView : UserControl
    {
        public LogicGatesPannelView()
        {
            InitializeComponent();

            foreach(var child in GostGatesCanvas.Children)
            {
                if(child is BaseLogicalGateControl control)
                {
                    control.DisableMoutionManipulator();
                    control.DisableOutputChannelClickableManipulator();
                }
            }

            foreach(var child in AnsiGatesCanvas.Children)
            {
                if(child is BaseLogicalGateControl control)
                {
                    control.DisableMoutionManipulator();
                    control.DisableOutputChannelClickableManipulator();
                }
            }
        }

        private void LogicGateTapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            if(sender is Control control && control.DataContext is LogicGatesPannelViewModel vm)
            {
                vm.SpawnOnCanvas(control);
            } 
        }
    }
}

