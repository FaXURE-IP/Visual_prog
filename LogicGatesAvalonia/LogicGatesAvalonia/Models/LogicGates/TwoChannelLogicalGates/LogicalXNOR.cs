using System;

using LogicGatesAvalonia.Models.DesiginationSymbols;

namespace 
    LogicGatesAvalonia.
    Models.
    LogicGates.
    TwoChannelLogicalGates
{
    public class LogicalXNOR : TwoChannelLogicalGate
    {
        public LogicalXNOR(string elementName,
            string userElementName,
            BaseDesiginationSymbol desiginationSymbol) :
            base(elementName, userElementName, desiginationSymbol)
        {
        }
        protected override bool DoLogic()
        {
            if (this.firstLogicalInput.Value is bool value &&
                this.secondLogicalInput.Value is bool value2)
            {
                return !(value ^ value2);
            }
            else
            {
                throw new ArgumentException($"value need to be boolean, value " +
                    $"{this.firstLogicalInput.Value} or value " +
                    $"{this.secondLogicalInput.Value} isn't boolean");
            }
        }
    }
}
