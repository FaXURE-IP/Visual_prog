using Avalonia.Controls;
using Avalonia.Input;

using LogicGatesAvalonia.Controls.MoutionControlManupulator;
using LogicGatesAvalonia.Controls;
using LogicGatesAvalonia.ViewModels;
using LogicGatesAvinternal.Controls;
using LogicGatesAvalonia.Models.DataBaseModel;

namespace LogicGatesAvalonia.Models.ElectricCircuit
{
    public class Circuit
    {  
        public delegate void DeleteConnector(object? sender, Control connectorForDelete);
        public event DeleteConnector? DeleteConnectorEvent = null;

        public BaseLogicalGateControl outputSocket = null;
        private BaseCircuitState circuitState;
        private MainWindowViewModel vm;

        public Circuit(MainWindowViewModel vm)
        {
            this.vm = vm;
            this.circuitState = new DefaultCircuitState(this);
        }

        public void SpawnConnector(Connector connector)
        {
            this.vm.SpawnConnector(connector);
        }

        public void AddGate(BaseLogicalGateControl gateControl, bool isRestoring = false)
        {
            gateControl.
                connectorManipulator.
                OutputConnectionHappendEvent += OutputConnectionHappend;

            gateControl.
                connectorManipulator.
                InputConnectionHappendEvent += InputConnectionHappend;

            if (!isRestoring)
            {
                DataBaseEntityModel.DataSet.Add(new DataBaseRecord(gateControl.ElementName,
                    gateControl.GetHashCode()));
            }
        }

        public void DeleteGate(BaseLogicalGateControl gateControl)
        {
            if (gateControl is BaseOneChannelLogicalContorl oneChannelLogicalControl)
            {
                if (oneChannelLogicalControl.outputConenctor != null)
                {
                    oneChannelLogicalControl.outputConenctor.SetInput(false);
                    DeleteConnectorEvent?.Invoke(this, oneChannelLogicalControl.outputConenctor);
                }

                if (oneChannelLogicalControl.inputConnector != null)
                {
                    DeleteConnectorEvent?.Invoke(this, oneChannelLogicalControl.inputConnector);
                }
            }

            if (gateControl is BaseTwoChannelLogicalControl twoChannelLogicalControl)
            {
                if (twoChannelLogicalControl.outputConenctor != null)
                {
                    twoChannelLogicalControl.outputConenctor.SetInput(false);
                    DeleteConnectorEvent?.Invoke(this, twoChannelLogicalControl.outputConenctor);
                }

                if (twoChannelLogicalControl.input1Connector != null)
                {
                    DeleteConnectorEvent?.Invoke(this, twoChannelLogicalControl.input1Connector);
                }

                if (twoChannelLogicalControl.input2Connector != null)
                {
                    DeleteConnectorEvent?.Invoke(this, twoChannelLogicalControl.input2Connector);
                }
            }

            DataBaseEntityModel.Remove(gateControl.Hash);
        }

        public void OutputConnectionHappend(object? sender, OutputConnectionEventArgs e)
        {
            circuitState.ConnectWithOutput(sender,e);
        }

        public void InputConnectionHappend(object? sender, InputConnectionEventArgs e)
        {
            circuitState.ConnectWithInput(sender,e);
        }

        public void CheckOnDisconnect(object? sender,PointerPressedEventArgs e)
        {
            circuitState.CheckOnDisconnectWithOutput(sender,e);
        }

        public void SetState(CiruitStateType state)
        {
            switch(state)
            {
                case CiruitStateType.DefaultState:
                    this.circuitState = new DefaultCircuitState(this);
                    break;

                case CiruitStateType.WaitingInputConnectionState:
                    this.circuitState = new WaitingInputConnectionState(this);
                    break;

                default:
                    break;
            }
        }

        public CiruitStateType GetCiruitStateType()
        {
            if (circuitState.GetType() == typeof(DefaultCircuitState))
            {
                return CiruitStateType.DefaultState;
            }

            if (circuitState.GetType() == typeof(WaitingInputConnectionState))
            {
                return CiruitStateType.WaitingInputConnectionState;
            }

            return CiruitStateType.DefaultState;
        }
    }
}
