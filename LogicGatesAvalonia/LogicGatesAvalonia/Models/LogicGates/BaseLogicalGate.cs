using Avalonia.Media;
using System;

using LogicGatesAvalonia.Models.DesiginationSymbols;

namespace LogicGatesAvalonia.Models
{
    public abstract class BaseLogicalGate : ILogicalGate
    {
        public IBrush? Background { get; set; }

        public delegate void SignalHasProcessed(object? sender);
        public event SignalHasProcessed? signalProcessedEvent;

        protected LogicGates.LogicalComponents.LogicalOutput output =
            new LogicGates.LogicalComponents.LogicalOutput();

        private string elementName = "";

        public string ElementName
        {
            get => elementName;
            protected set => elementName = value;
        }

        protected string userElementName = "";
        public virtual void ProcessSignal()
        {
            if (this.IsInputNotNull())
            {
                output.SetLogicalValue(this.DoLogic());
                signalProcessedEvent?.Invoke(this);
            }
        }
        protected abstract bool IsInputNotNull();

        protected virtual bool DoLogic()
        {
            throw new NotImplementedException();
        }

        public BaseDesiginationSymbol DesiginationSymbol
        {
            get;
            protected set;
        }

        protected BaseLogicalGate(string elementName, string userElementName,
            BaseDesiginationSymbol desiginationSymbol)
        {
            this.userElementName = userElementName;
            this.DesiginationSymbol = desiginationSymbol;
            this.elementName = elementName;
        }

        public object getOutputValue()
        {
            return this.output.Value;
        }
    }
}
