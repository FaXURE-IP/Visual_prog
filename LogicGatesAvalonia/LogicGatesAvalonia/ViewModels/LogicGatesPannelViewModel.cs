using Avalonia.Controls;

namespace LogicGatesAvalonia.ViewModels
{
    public class LogicGatesPannelViewModel : ViewModelBase
    {
        private MainWindowViewModel viewModel;

        public LogicGatesPannelViewModel(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public void SpawnOnCanvas(Control control)
        {
            viewModel.SpawnOnCanvas(control);
        }
    }
}
