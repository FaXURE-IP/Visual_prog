using Avalonia.Media;
using System;

using LogicGatesAvalonia.Models.LogicGates;
using LogicGatesAvinternal.Controls;

namespace LogicGatesAvalonia.Controls
{
    public abstract class BaseOneChannelLogicalContorl : BaseLogicalGateControl
    {
        protected double inputPosX;
        protected double inputPosY;

        // коннектор связывающий элемент с предыдущими элементами, необходимо его удалить при удалении элемента 
        public Connector? inputConnector;
        

        private IBrush? inputChannelActiveColor = new SolidColorBrush(Colors.Red);
        public IBrush? InputChannelActiveColor
        {
            get => inputChannelActiveColor;
            set
            {
                this.inputChannelActiveColor = value;
                this.UpdateView();
            }
        }

        public Avalonia.Point GetInputChannelPos()
        {
            return new Avalonia.Point(inputPosX, inputPosY);
        }
        public bool IsInputHitted(Avalonia.Point clickPos)
        {
            return
                Math.Pow((inputPosX - clickPos.X), 2)
                + Math.Pow((inputPosY - clickPos.Y), 2)
                <= Math.Pow(elipseRadious + penThickness, 2);
        }

        protected abstract void RenderInputLine(DrawingContext context);
        protected abstract void RenderInputChannel(DrawingContext channel);

        private bool input;
        public bool Input
        {
            get => input;
            set
            {
                if(this.model is OneChannelLogicalGate oneChannelModel)
                {
                    oneChannelModel.SetInputValue(value);
                    input = oneChannelModel.GetInputValue();
                    this.Update();
                }

                if(value == true)
                {
                    InputChannelActiveColor = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    InputChannelActiveColor = new SolidColorBrush(Colors.Red);
                }

                
            }
        }

        protected int drawingInput
        {
            set;
            get;
        }

        protected virtual void Update()
        {
            this.model.ProcessSignal();
            if (this.model.getOutputValue() is bool value)
            {
                this.Output = Convert.ToInt32(value);
            }
        }

        public sealed override void Render(DrawingContext context)
        {
            base.Render(context);

            this.RenderDesiginationSymbol(context);
            this.RenderInputChannel(context);
            this.RenderInputLine(context);
            this.RenderMainBody(context);
            this.RenderOutput(context);
            this.RenderElementName(context);
        }
    }
}
