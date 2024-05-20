using Avalonia.Input;
using LogicGatesAvinternal.Controls;

namespace LogicGatesAvalonia.Controls.MoutionControlManupulator
{
    public class OutputConnectionEventArgs
    {
        public BaseLogicalGateControl socket; // from what connected
        public Avalonia.Point socketPos;

        public OutputConnectionEventArgs(BaseLogicalGateControl socket,Avalonia.Point pos)
        {
            this.socket = socket;
            this.socketPos = pos;
        }
    }
    public class InputConnectionEventArgs
    {
        public BaseLogicalGateControl socket; // where connected
        public Avalonia.Point socketPos;
        public InputSockets socketId;

        public InputConnectionEventArgs(BaseLogicalGateControl socket,
            Avalonia.Point pos,
            InputSockets socketId)
        {
            this.socket = socket;
            this.socketPos = pos;
            this.socketId = socketId;
        }
    }

    public class ConnectorManipulator : BaseControlManipulator
    {
        public delegate void OutputConnectionHappend(object? sender, OutputConnectionEventArgs e);
        public event OutputConnectionHappend? OutputConnectionHappendEvent;

        public delegate void InputConnectionHappend(object? sender, InputConnectionEventArgs e);
        public event InputConnectionHappend? InputConnectionHappendEvent;

        public ConnectorManipulator(BaseLogicalGateControl control) : base(control)
        {
            this.control.Tapped += OnControlTapped;
        }
        private void OnControlTapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            if (isEnable)
            {
                var clickedPosX = e.GetPosition(this.control).X;
                var clickedPosY = e.GetPosition(this.control).Y;

                if(control is BaseOneChannelLogicalContorl oneChannelGateControl)
                {
                    
                    if (oneChannelGateControl.IsOutputHitted(new Avalonia.Point(clickedPosX, clickedPosY)))
                    {
                        // waiting for connection with other control input 
                        OutputConnectionHappendEvent?.Invoke(this,
                            new OutputConnectionEventArgs(this.control,
                            new Avalonia.Point(clickedPosX,clickedPosY)));
                        return;
                    }

                    if (oneChannelGateControl.IsInputHitted(new Avalonia.Point(clickedPosX,clickedPosY)))
                    {
                        InputConnectionHappendEvent?.Invoke(this,
                            new InputConnectionEventArgs(this.control,
                            new Avalonia.Point(clickedPosX, clickedPosY),InputSockets.firstSocket));
                        return;
                    }
                }

                if(control is BaseTwoChannelLogicalControl twoChannelGateControl)
                {
                    if (twoChannelGateControl.IsOutputHitted(new Avalonia.Point(clickedPosX,clickedPosY))){

                        OutputConnectionHappendEvent?.Invoke(this,
                            new OutputConnectionEventArgs(this.control,
                            new Avalonia.Point(clickedPosX, clickedPosY)));
                        return;
                    }

                    if (twoChannelGateControl.IsInput1Hitted(new Avalonia.Point(clickedPosX,clickedPosY)))
                    {
                        InputConnectionHappendEvent?.Invoke(this,
                            new InputConnectionEventArgs(this.control,
                            new Avalonia.Point(clickedPosX, clickedPosY),InputSockets.firstSocket));
                        return;
                    }

                    if(twoChannelGateControl.IsInput2Hitted(new Avalonia.Point(clickedPosX, clickedPosY)))
                    {
                        InputConnectionHappendEvent?.Invoke(this,
                            new InputConnectionEventArgs(this.control,
                            new Avalonia.Point(clickedPosX, clickedPosY), InputSockets.secondSocket));
                        return;
                    }
                }
            } 
        }

    }
}
