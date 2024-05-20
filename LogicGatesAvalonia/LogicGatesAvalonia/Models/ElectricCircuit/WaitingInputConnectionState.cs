using Avalonia.Input;
using LogicGatesAvalonia.Controls;
using LogicGatesAvalonia.Controls.MoutionControlManupulator;
using LogicGatesAvalonia.Models.DataBaseModel;
using LogicGatesAvinternal.Controls;


namespace LogicGatesAvalonia.Models.ElectricCircuit
{
    public class WaitingInputConnectionState : BaseCircuitState
    {
        public WaitingInputConnectionState(Circuit circuit) : base(circuit) {}

        public override void ConnectWithInput(object? sender, InputConnectionEventArgs e)
        {
            var connector = new Connector(
                    circuit.outputSocket,
                    e.socket,
                    e.socketId);


            // задаем коннектор для выходного подключения
            circuit.outputSocket.outputConenctor = connector;

            if (e.socket is BaseOneChannelLogicalContorl oneChannelControl)
            {
                    oneChannelControl.inputConnector = connector;
            }

            // задаем коннекторы для входного подключения
            if (e.socket is BaseTwoChannelLogicalControl twoChannelControl) 
            {
                // воткнули в первый сокет
                if (e.socketId == InputSockets.firstSocket)
                {
                    twoChannelControl.input1Connector = connector;
                }

                // воткнули во второй сокет
                if (e.socketId == InputSockets.secondSocket)
                {
                    twoChannelControl.input2Connector = connector;
                }
            }

            DataBaseEntityModel.WriteConnectorInfoToRecord(
                circuit.outputSocket,
                e.socket,
                e.socketId);

            circuit.SpawnConnector(connector);

            this.circuit.SetState(CiruitStateType.DefaultState);
        }

        public override void ConnectWithOutput(object? sender, OutputConnectionEventArgs e)
        {

        }

        public override void CheckOnDisconnectWithOutput(object? sender, PointerPressedEventArgs e)
        {
            if(e.Source is BaseLogicalGateControl control)
            {
                var clickPosX = e.GetPosition(control).X;
                var clickPosY = e.GetPosition(control).Y;

                if (control is BaseOneChannelLogicalContorl oneChannelLogicalControl)
                {
                    if (!oneChannelLogicalControl.IsInputHitted(new Avalonia.Point(clickPosX, clickPosY)))
                    {
                        this.circuit.SetState(CiruitStateType.DefaultState);
                        return;
                    }
                }

                if(control is BaseTwoChannelLogicalControl twoChannelControl)
                {
                    if (!twoChannelControl.IsInput1Hitted(new Avalonia.Point(clickPosX,clickPosY)))
                    {
                        if(!twoChannelControl.IsInput2Hitted(new Avalonia.Point(clickPosX, clickPosY)))
                        {
                            this.circuit.SetState(CiruitStateType.DefaultState);
                        }
                    }
                }
            }
            else
            {
                this.circuit.SetState(CiruitStateType.DefaultState);
            }
        }
    }
}
