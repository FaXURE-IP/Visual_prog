using Avalonia.Input;

namespace LogicGatesAvalonia.Controls.MoutionControlManupulator
{
    interface IControlHighlighterManipulator
    {
        public void OnPointerEntered(object? sender,PointerEventArgs e);
        public void OnPointerExited(object? sender,PointerEventArgs e);
    }
}
