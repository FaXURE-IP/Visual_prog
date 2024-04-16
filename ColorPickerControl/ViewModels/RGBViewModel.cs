using ReactiveUI;

namespace ColorPickerControl.ViewModels
{
    public class RGBViewModel : ViewModelBase
    {
        private byte red = 0;
        public delegate void StateChanged();
        public event StateChanged? StateChangedEvent;
        public byte Red
        {
            get => red;
            set
            {
                this.RaiseAndSetIfChanged(ref red, value);
                StateChangedEvent?.Invoke();
            }
        }

        private byte green = 0;
        public byte Green
        {
            get => green;
            set
            {
                this.RaiseAndSetIfChanged(ref green, value);
                StateChangedEvent?.Invoke();

            }
        }

        private byte blue = 0;
        public byte Blue
        {
            get => blue;
            set
            {
                this.RaiseAndSetIfChanged(ref blue, value);
                StateChangedEvent?.Invoke();
            }
        }
    }
}
