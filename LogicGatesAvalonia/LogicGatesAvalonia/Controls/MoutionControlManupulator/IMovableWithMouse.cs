using Avalonia.Input;

namespace LogicGatesAvalonia.Controls.MoutionControlManupulator
{
    public interface IMovableWithMouseManipulator
    {
        public void OnPointerPressed(object sender, PointerPressedEventArgs e);
        public void OnPointerMoved(object sender, PointerEventArgs e);
        public void OnPointerReleased(object sender, PointerReleasedEventArgs e);
    }
}
