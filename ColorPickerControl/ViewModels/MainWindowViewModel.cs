using ReactiveUI;

namespace ColorPickerControl.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private object content;

        public object Content
        {
            get => content;
            set
            {
                this.RaiseAndSetIfChanged(ref content, value);
            }
        }

        public MainWindowViewModel()
        {
            Content = new ColorPickerViewModel("Black","palette.png");
        }
    }
}
