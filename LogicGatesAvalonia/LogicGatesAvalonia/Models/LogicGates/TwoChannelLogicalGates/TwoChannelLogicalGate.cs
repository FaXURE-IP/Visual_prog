using LogicGatesAvalonia.Models.DesiginationSymbols;
using LogicGatesAvalonia.Models.LogicGates.LogicalComponents;
using System;

namespace LogicGatesAvalonia.Models.LogicGates
{
    public class TwoChannelLogicalGate : BaseLogicalGate
    {
        protected LogicalInput firstLogicalInput;
        protected LogicalInput secondLogicalInput;
        protected TwoChannelLogicalGate(
            string elementName,
            string userElementName,
            BaseDesiginationSymbol desiginationSymbol) :
            base(elementName, userElementName, desiginationSymbol)
        {
            this.firstLogicalInput = new LogicalInput(1, "first logical input");
            this.secondLogicalInput = new LogicalInput(2, "second logical input");
        }

        protected override bool IsInputNotNull()
        {
            if (this.firstLogicalInput.Value is bool && this.firstLogicalInput.Value != null)
            {
                if(this.secondLogicalInput.Value is bool && this.secondLogicalInput.Value != null)
                {
                    return true;
                }
            }

            return false;
        }

        public void SetInputValue(bool inputValue,bool inputValue2) 
        {
            this.firstLogicalInput.SetLogicalValue(inputValue);
            this.secondLogicalInput.SetLogicalValue(inputValue2);
        }

        public bool GetFirstInputValue()
        {
            if (firstLogicalInput.Value is bool val)
            {
                return val;
            }

            throw new ArgumentException($"value need to be boolean, value {this.firstLogicalInput.Value}");
        }

        public bool GetSecondInputValue()
        {
            if (secondLogicalInput.Value is bool val)
            {
                return val;
            }

            throw new ArgumentException($"value need to be boolean, value {this.secondLogicalInput.Value}");
        }
    }
}
