using Avalonia.Input;
using Avalonia.Media;
using LogicGatesAvinternal.Controls;

namespace LogicGatesAvalonia.Controls.MoutionControlManupulator
{
    public class MoutionControlManipulator : BaseControlManipulator, IMovableWithMouseManipulator
    {
        private double offsetX;
        private double offsetY;

        private bool isDragging;

        private double startX;
        private double startY;

        public void SetInitValues(double startX, double startY, double offsetX, double offsetY)
        {
            this.offsetX = offsetX;
            this.offsetY = offsetY;
            this.startX = startX;
            this.startY = startY;
        }

        public MoutionControlManipulator(BaseLogicalGateControl body) : base(body)
        {
            this.control.PointerPressed += OnPointerPressed;
            this.control.PointerMoved += OnPointerMoved;
            this.control.PointerReleased += OnPointerReleased;
        }
        public void OnPointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (this.isEnable)
            {
                if (e.GetCurrentPoint(control).Properties.IsLeftButtonPressed)
                {
                    isDragging = true;

                    startX = e.GetPosition(control).X;
                    startY = e.GetPosition(control).Y;
                }
            }
        }
        public void OnPointerMoved(object sender, PointerEventArgs e)
        {
            if (this.isEnable)
            {
                if (isDragging)
                {
                    this.offsetX += e.GetPosition(control).X - startX;
                    this.offsetY += e.GetPosition(control).Y - startY;

                    control.RenderTransform = new TranslateTransform(offsetX,
                        offsetY);

                    startX = e.GetPosition(control).X;
                    startY = e.GetPosition(control).Y;
                }
            }
        }

        public void OnPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            if (this.isEnable)
            {
                isDragging = false;
            }
        }
    }
}
