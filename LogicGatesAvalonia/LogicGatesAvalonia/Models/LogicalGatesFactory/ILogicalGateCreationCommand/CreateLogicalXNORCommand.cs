using LogicGatesAvalonia.Models.DesiginationSymbols;
using LogicGatesAvalonia.Models.LogicGates.TwoChannelLogicalGates;

namespace LogicGatesAvalonia.Models.LogicalGatesFactory.ILogicalGateCreationCommand
{
    public class CreateLogicalXNORCommand : BaseCreateLogicalGateCommand
    {
        public CreateLogicalXNORCommand(string elementName, BaseDesiginationSymbol desiginationSymbol)
            : base(elementName, desiginationSymbol)
        {
        }
        public override BaseLogicalGate Create(string logicalGateUsername)
        {
            return new LogicalXNOR(
                elementName,
                logicalGateUsername,
                desigination);
        }
    }
}
