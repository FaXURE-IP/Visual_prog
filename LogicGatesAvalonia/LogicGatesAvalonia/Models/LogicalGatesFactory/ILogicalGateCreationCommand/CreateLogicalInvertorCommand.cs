using LogicGatesAvalonia.Models.DesiginationSymbols;
using LogicGatesAvalonia.Models.LogicGates.OneChannelLogicalGates;

namespace LogicGatesAvalonia.Models.LogicalGatesFactory.ILogicalGateCreationCommand
{
    public class CreateLogicalInvertorCommand : BaseCreateLogicalGateCommand
    {
        public CreateLogicalInvertorCommand(string elementName,BaseDesiginationSymbol desiginationSymbol) 
            : base(elementName, desiginationSymbol)
        {
        }
        public override BaseLogicalGate Create(string logicalGateUsername)
        {
            return new LogicalInvertor(
                elementName,
                logicalGateUsername,
                desigination);
        }
    }
}
