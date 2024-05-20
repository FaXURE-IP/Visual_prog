
using Avalonia.Input;
using LogicGatesAvalonia.Controls.MoutionControlManupulator;

namespace LogicGatesAvalonia.Models.ElectricCircuit
{
    public abstract class BaseCircuitState
    {
        protected Circuit circuit;

        protected BaseCircuitState(Circuit circuit)
        {
            this.circuit = circuit;
        }

        public abstract void ConnectWithOutput(object? sender, OutputConnectionEventArgs e);

        public abstract void ConnectWithInput(object? sender, InputConnectionEventArgs e);

        public abstract void CheckOnDisconnectWithOutput(object? sender, PointerPressedEventArgs e);
    }
}
