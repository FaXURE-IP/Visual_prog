using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using LogicGatesAvinternal.Controls;
using System;

// когда меняется инупт у любого контрола -> пересчитываем значение, меняем выход -> генерируем событие во вне
// коннектор хранит в себе ссылку на то output и input в который он воткнут, и подписывается на собтыие соответстующего output 
// logic gate'а с которого было сгенерированно событие, после этого обновляет input logic gate в котрый он воткнут 


namespace LogicGatesAvalonia.Controls
{
    public enum InputSockets
    {
        firstSocket,
        secondSocket
    }

    public class Connector : Control
    {
        private BaseLogicalGateControl output;// from what connected 
        private BaseLogicalGateControl input;// to what connected

        InputSockets inputSocketId;

        public void SetInput(bool value)
        {
            
            if(input is BaseOneChannelLogicalContorl oneChannelControl)
            {
                oneChannelControl.Input = Convert.ToBoolean(value);
            }

            if(input is BaseTwoChannelLogicalControl twoChannelControl)
            {
                if(inputSocketId == InputSockets.firstSocket)
                {
                    twoChannelControl.Input_1 = Convert.ToBoolean(value);
                }
                    
                if(inputSocketId == InputSockets.secondSocket)
                {
                    twoChannelControl.Input_2 = Convert.ToBoolean(value);
                }
            }
        }

        public Connector(BaseLogicalGateControl output, BaseLogicalGateControl input,InputSockets socketId)
        {
            output.PointerMoved += OutputOnPointerMoved;
            input.PointerMoved += InputOnPointerMoved;

            inputSocketId = socketId;

            output.OutputValueChangedEvent += OnOutputValueChangedEventHandler;

            this.output = output;
            this.input = input;

            SetInput(Convert.ToBoolean(output.Output));
        }

        private void OnOutputValueChangedEventHandler(object sender,int updatedValue)
        {
            this.SetInput(Convert.ToBoolean(updatedValue));
        }

        private void OutputOnPointerMoved(object sender, PointerEventArgs e)
        {
            InvalidateVisual();
        }
        private void InputOnPointerMoved(object sender, PointerEventArgs e)
        {
            InvalidateVisual();
        }

        public sealed override void Render(DrawingContext context)
        {
            base.Render(context);
            var pen = new Pen(new SolidColorBrush(Colors.Green), 1);

            if (input is BaseOneChannelLogicalContorl oneChannelControl)
            {
                var outputTransform = output.RenderTransform as TranslateTransform;
                var inputTransform = input.RenderTransform as TranslateTransform;

                var updatedOutPos = output.GetOutputChannelPos();
                if (outputTransform != null)
                {
                    var oldX = output.GetOutputChannelPos().X;
                    var oldY = output.GetOutputChannelPos().Y;
                    updatedOutPos = new Avalonia.Point(outputTransform.X + oldX, outputTransform.Y + oldY);
                }

                var updatedInPos = oneChannelControl.GetInputChannelPos();
                if (inputTransform != null)
                {
                    updatedInPos = new Avalonia.Point(inputTransform.X + oneChannelControl.GetInputChannelPos().X,
                        inputTransform.Y + oneChannelControl.GetInputChannelPos().Y);
                }

                context.DrawLine(pen,
                    updatedOutPos,
                    updatedInPos);
            }

            if (input is BaseTwoChannelLogicalControl twoChannelControl)
            {
                Avalonia.Point inputChannelPos;
                switch (inputSocketId)
                {
                    case InputSockets.firstSocket:
                        inputChannelPos = twoChannelControl.GetInput1ChannelPos();
                        break;

                    case InputSockets.secondSocket:
                        inputChannelPos = twoChannelControl.GetInput2ChannelPos();
                        break;

                    default:
                        inputChannelPos = new Avalonia.Point();
                        break;

                }

                var outputTransform = output.RenderTransform as TranslateTransform;
                var inputTransform = input.RenderTransform as TranslateTransform;


                var updatedOutPos = output.GetOutputChannelPos();
                if (outputTransform != null)
                {
                    var oldX = output.GetOutputChannelPos().X;
                    var oldY = output.GetOutputChannelPos().Y;
                    updatedOutPos = new Avalonia.Point(outputTransform.X + oldX, outputTransform.Y + oldY);
                }

                var updatedInPos = new Avalonia.Point();
                if (inputTransform != null)
                {

                    double oldX;
                    double oldY;
                    switch (inputSocketId)
                    {
                        case InputSockets.firstSocket:
                            oldX = twoChannelControl.GetInput1ChannelPos().X;
                            oldY = twoChannelControl.GetInput1ChannelPos().Y;
                            break;

                        case InputSockets.secondSocket:
                            oldX = twoChannelControl.GetInput2ChannelPos().X;
                            oldY = twoChannelControl.GetInput2ChannelPos().Y;
                            break;

                        default:
                            oldX = 0;
                            oldY = 0;
                            break;

                    }

                    updatedInPos = new Avalonia.Point(inputTransform.X + oldX, inputTransform.Y + oldY);
                }
                else
                {
                    updatedInPos = inputChannelPos;
                }

                context.DrawLine(pen, updatedOutPos, updatedInPos);
            }
        }
    }
}
