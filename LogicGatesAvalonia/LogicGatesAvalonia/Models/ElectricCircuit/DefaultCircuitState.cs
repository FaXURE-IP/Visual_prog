using LogicGatesAvalonia.Controls.MoutionControlManupulator;
using Avalonia.Input;

namespace LogicGatesAvalonia.Models.ElectricCircuit
{
    public class DefaultCircuitState : BaseCircuitState
    {
        public DefaultCircuitState(Circuit circuit) : base(circuit) { }
        public override void ConnectWithInput(object? sender, InputConnectionEventArgs e)
        {

        }

        public override void ConnectWithOutput(object? sender, OutputConnectionEventArgs e)
        {
            circuit.outputSocket = e.socket;
            circuit.SetState(CiruitStateType.WaitingInputConnectionState);
        }

        public override void CheckOnDisconnectWithOutput(object? sender, PointerPressedEventArgs e)
        {
            
        }
    }
}
