using Avalonia.Media;
using System;

using LogicGatesAvalonia.Models.LogicGates;
using LogicGatesAvinternal.Controls;

namespace LogicGatesAvalonia.Controls
{
    public abstract class BaseTwoChannelLogicalControl : BaseLogicalGateControl
    {
        protected double input1PosX;
        protected double input1PosY;
        protected double input2PosX;
        protected double input2PosY;

        public Connector? input1Connector;// коннектор связывающий 1 вход элемента
        public Connector? input2Connector;

        private IBrush? input_1ChannelActiveColor = new SolidColorBrush(Colors.Red);
        public IBrush? Input_1ChannelActiveColor
        {
            get => input_1ChannelActiveColor;
            set
            {
                this.input_1ChannelActiveColor = value;
                this.UpdateView();
            }
        }

        private IBrush? input_2ChannelActiveColor = new SolidColorBrush(Colors.Red);
        public IBrush? Input_2ChannelActiveColor
        {
            get => input_2ChannelActiveColor;
            set
            {
                this.input_2ChannelActiveColor = value;
                this.UpdateView();
            }
        }

        public bool IsInput1Hitted(Avalonia.Point clickPos)
        {
            return
                Math.Pow((input1PosX - clickPos.X), 2)
                + Math.Pow((input1PosY - clickPos.Y), 2)
                <= Math.Pow(elipseRadious + penThickness, 2);
        }

        public bool IsInput2Hitted(Avalonia.Point clickPos)
        {
            return
                Math.Pow((input2PosX - clickPos.X), 2)
                + Math.Pow((input2PosY - clickPos.Y), 2)
                <= Math.Pow(elipseRadious + penThickness, 2);
        }

        public Avalonia.Point GetInput1ChannelPos()
        {
            return new Avalonia.Point(input1PosX, input1PosY);
        }

        public Avalonia.Point GetInput2ChannelPos()
        {
            return new Avalonia.Point(input2PosX, input2PosY);
        }

        protected abstract void RenderInputLine_1(DrawingContext renderWindow);
        protected abstract void RenderInputLine_2(DrawingContext renderWindow);
        protected abstract void RenderInputChannel_1(DrawingContext renderWindow);
        protected abstract void RenderInputChannel_2(DrawingContext renderWindow);

        private bool input_1;
        public bool Input_1
        {
            get => input_1;
            set
            {
                if (this.model is TwoChannelLogicalGate twoChannelModel)
                {
                    twoChannelModel.SetInputValue(value, input_2);
                    input_1 = twoChannelModel.GetFirstInputValue();
                    this.Update();
                }

                if(value == true)
                {
                    Input_1ChannelActiveColor = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    Input_1ChannelActiveColor = new SolidColorBrush(Colors.Red);
                }


                
            }
        }

        private bool input_2;
        public bool Input_2
        {
            get => input_2;
            set
            {
                if (this.model is TwoChannelLogicalGate twoChannelModel)
                {
                    twoChannelModel.SetInputValue(input_1, value);
                    input_2 = twoChannelModel.GetSecondInputValue();
                    this.Update();
                }

                if (value == true)
                {
                    Input_2ChannelActiveColor = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    Input_2ChannelActiveColor = new SolidColorBrush(Colors.Red);
                }

                
            }
        }

        protected virtual void Update()
        {
            this.model.ProcessSignal();
            if (this.model.getOutputValue() is bool value)
            {
                this.Output = Convert.ToInt32(value);
            }
        }

        protected int drawingInput_1
        {
            set;
            get;
        }

        protected int drawingInput_2
        {
            set;
            get;
        }
        public sealed override void Render(DrawingContext context)
        {
            base.Render(context);

            this.RenderDesiginationSymbol(context);
            this.RenderInputChannel_1(context);
            this.RenderInputChannel_2(context);
            this.RenderInputLine_1(context);
            this.RenderInputLine_2(context);
            this.RenderMainBody(context);
            this.RenderOutput(context);
            this.RenderElementName(context);
        }
    }
}
