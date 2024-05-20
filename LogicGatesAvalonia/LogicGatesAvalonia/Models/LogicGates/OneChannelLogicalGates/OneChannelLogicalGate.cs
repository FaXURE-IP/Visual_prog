using System;

using LogicGatesAvalonia.Models.DesiginationSymbols;
using LogicGatesAvalonia.Models.LogicGates.LogicalComponents;

namespace LogicGatesAvalonia.Models.LogicGates
{
    public class OneChannelLogicalGate : BaseLogicalGate
    {
        protected LogicalInput input;
        protected OneChannelLogicalGate(
            string elementName,
            string userElementName,
            BaseDesiginationSymbol desiginationSymbol) :
            base(elementName, userElementName, desiginationSymbol)
        {
            this.input = new LogicalInput();
            this.output = new LogicalOutput();
        }

        protected override bool IsInputNotNull()
        {
            if(this.input.Value is bool && this.input.Value != null)
            {
                return true;
            }

            return false;
        }

        public void SetInputValue(bool inputValue)
        {
            this.input.SetLogicalValue(inputValue);
        }

        public bool GetInputValue()
        {
            if(input.Value is bool val)
            {
                return val;
            }

            throw new ArgumentException($"value need to be boolean, value {this.input.Value}");
        }
    }
}
