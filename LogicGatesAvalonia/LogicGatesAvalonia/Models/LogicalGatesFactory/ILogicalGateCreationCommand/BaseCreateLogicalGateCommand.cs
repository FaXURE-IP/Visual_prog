using LogicGatesAvalonia.Models.DesiginationSymbols;

namespace LogicGatesAvalonia.Models.LogicalGatesFactory.ILogicalGateCreationCommand
{
    public abstract class BaseCreateLogicalGateCommand : ILogicalGateCreationCommand
    {
        protected string elementName = "";
        protected BaseDesiginationSymbol desigination;

        protected BaseCreateLogicalGateCommand(string elementName,BaseDesiginationSymbol desigination)
        {
            this.elementName = elementName;
            this.desigination = desigination;
        }
        public abstract BaseLogicalGate Create(string logicalGateUsername);
    }
}
