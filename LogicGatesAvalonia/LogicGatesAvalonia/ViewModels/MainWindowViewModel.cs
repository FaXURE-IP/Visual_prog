using Avalonia.Controls;
using ReactiveUI;
using Avalonia.Input;

using LogicGatesAvinternal.Controls;
using LogicGatesAvalonia.Models.ElectricCircuit;

namespace LogicGatesAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Circuit circuit;
        public CircuitRestorer circuitRestorer;

        public delegate void SpawnNewLogicGate(object? sender, Control controlForSwapn);
        public event SpawnNewLogicGate? NewLogicGateEvent;


        public delegate void DeleteConnector(object? sender, Control connector);
        public event DeleteConnector? DeleteConnectorEvent;

        private ViewModelBase logicGatesPannel;
        public ViewModelBase LogicGatesPannel
        {
            get => logicGatesPannel;
            set => this.RaiseAndSetIfChanged(ref logicGatesPannel, value);
        }

        public void RemoveLogicalGates(BaseLogicalGateControl control)
        {
            this.circuit.DeleteGate(control);
        }

        // view Должна подписаться на это
        public void NotifyViewAboutConnectorDeletion(object? sender,Control connector) 
        {
            DeleteConnectorEvent?.Invoke(sender,connector);
            var info = circuit;
        }

        private double canvasWidth = 1600;
        private double canvasHeight = 900;
        public double CanvasWidth
        {
            get => canvasWidth;
            set => this.RaiseAndSetIfChanged(ref canvasWidth, value);
        }

        public double CanvasHeight
        {
            get => canvasHeight;
            set => this.RaiseAndSetIfChanged(ref canvasHeight, value);
        }

        public MainWindowViewModel()
        {
            this.circuit = new Circuit(this);
            this.LogicGatesPannel = new LogicGatesPannelViewModel(this);

            circuit.DeleteConnectorEvent += NotifyViewAboutConnectorDeletion;

            this.circuitRestorer = new CircuitRestorer(this);
        }

        public void SpawnOnCanvas(object control,bool isRestoring = false)
        {
            if(control is BaseLogicalGateControl anyControl)
            {
                if (!isRestoring)
                {
                    var clone = anyControl.Clone() as BaseLogicalGateControl;

                    if (!isRestoring)
                    {
                        clone.Hash = clone.GetHashCode();
                    }
                    
                    this.circuit.AddGate(clone);
                    Canvas.SetLeft(clone, 10);
                    Canvas.SetTop(clone, 10);
                    NewLogicGateEvent?.Invoke(this, clone);
                }
                else
                {
                    this.circuit.AddGate(control as BaseLogicalGateControl, isRestoring:true);
                    NewLogicGateEvent?.Invoke(this, control as Control);
                }                
            }
        }

        public void SpawnConnector(object control)
        {
            NewLogicGateEvent?.Invoke(this, control as Control);
        }

        public void CanvasTapped(object sender, PointerPressedEventArgs e)
        {
            circuit.CheckOnDisconnect(sender, e);
        }
    }
}
