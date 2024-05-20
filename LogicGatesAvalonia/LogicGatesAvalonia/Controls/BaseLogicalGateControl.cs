using Avalonia.Controls;
using Avalonia.Media;
using System;

using LogicGatesAvalonia.Controls;
using LogicGatesAvalonia.Controls.MoutionControlManupulator;
using LogicGatesAvalonia.Models;
using LogicGatesAvalonia.Models.DesiginationSymbols;

namespace LogicGatesAvinternal.Controls
{
    public abstract class BaseLogicalGateControl : Control, ICloneable
    {
        public int Hash;

        public delegate void OutputValueChanged(object? sender,int updatedValue);
        public event OutputValueChanged? OutputValueChangedEvent = null;

        public delegate void WasDisposed();
        public event WasDisposed? GateWasDisposed = null;

        // выходной коннектор, связыввает элемент со следующими элементами в цепи
        public Connector? outputConenctor;

        protected double startPointX;
        protected double startPointY;
        protected double elipseRadious;
        protected double outputPosX;
        protected double outputPosY;
        protected double penThickness;

        protected abstract void RenderElementName(DrawingContext renderWindow);
        protected abstract void RenderDesiginationSymbol(DrawingContext renderWindow);
        protected abstract void RenderOutput(DrawingContext renderWindow);
        protected abstract void RenderMainBody(DrawingContext renderWindow);

        protected BaseLogicalGate model;

        public BaseDesiginationSymbol GetDesiginationSymbol()
        {
            return model.DesiginationSymbol;
        }

        public Avalonia.Point GetOutputChannelPos()
        {
            return new Avalonia.Point(outputPosX,outputPosY);
        }

        public bool IsOutputHitted(Avalonia.Point clickPos)
        {
            return
                Math.Pow((outputPosX - clickPos.X), 2)
                + Math.Pow((outputPosY - clickPos.Y), 2)
                <= Math.Pow(elipseRadious + penThickness, 2);
        }


        public MoutionControlManipulator manipulator;
        protected ColorHighlighterManipulator colorHighlighterManipulator;
        public ConnectorManipulator connectorManipulator;

        protected BaseLogicalGateControl()
        {
            manipulator = new MoutionControlManipulator(this);

            colorHighlighterManipulator = new ColorHighlighterManipulator(this,
                new SolidColorBrush(Colors.Green),new SolidColorBrush(Colors.Black));

            connectorManipulator = new ConnectorManipulator(this);
        }

        public void EnableMoutionManipulator()
        {
            if(manipulator != null)
            {
                this.manipulator.Enable();
            }

        }

        public void DisableMoutionManipulator()
        {
            if(manipulator != null)
            {
                this.manipulator.Disable();
            }

        }

        public void EnableOutputChannelClickableManipulator()
        {
            if(connectorManipulator != null)
            {
                connectorManipulator.Enable();
            }
            
        }

        public void DisableOutputChannelClickableManipulator()
        {
            if(connectorManipulator != null)
            {
                connectorManipulator.Disable();
            }
        }
        public int TextSize { get; set; } = 15;

        private int output;
        public int Output
        {
            get => output;
            protected set
            {
                UpdateView();
                this.OutputValueChangedEvent?.Invoke(this,value);
                output = value;
            }
        }

        private string elementName;
        public string ElementName
        {
            get => elementName;
            protected set => elementName = value;
        }

        private IBrush? selectedTextColor = new SolidColorBrush(Colors.Black);
 
        public IBrush? SelectedTextColor
        {
            get => selectedTextColor;
            set
            {
                this.UpdateView();
                selectedTextColor = value;
            }
        }
        public void UpdateView()
        {
            this.InvalidateVisual();
        }

        public abstract object Clone();

        public void Dispose()
        {
            this.GateWasDisposed?.Invoke();
        }
    }
}
