using Avalonia.Input;
using Avalonia.Media;

using LogicGatesAvinternal.Controls;

namespace LogicGatesAvalonia.Controls.MoutionControlManupulator
{
    public class ColorHighlighterManipulator : BaseControlManipulator, IControlHighlighterManipulator
    {
        public SolidColorBrush highlightColor;
        public SolidColorBrush deafultColor;
        public ColorHighlighterManipulator(
            BaseLogicalGateControl control,
            SolidColorBrush whenHighlightColor,
            SolidColorBrush defaultColor) : base(control)
        {
            this.highlightColor = whenHighlightColor;
            this.deafultColor = defaultColor;

            this.control.PointerEntered += OnPointerEntered;
            this.control.PointerExited += OnPointerExited;
        }

        public void OnPointerExited(object? sender, PointerEventArgs e)
        {
            this.control.SelectedTextColor = deafultColor; 
        }

        public void OnPointerEntered(object? sender, PointerEventArgs e)
        {
            this.control.SelectedTextColor = highlightColor;
        }
    }
}
