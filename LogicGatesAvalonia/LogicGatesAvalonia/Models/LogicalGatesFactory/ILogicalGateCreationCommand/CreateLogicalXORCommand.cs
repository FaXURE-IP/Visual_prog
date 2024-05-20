using LogicGatesAvalonia.Models.DesiginationSymbols;
using LogicGatesAvalonia.Models.LogicGates.TwoChannelLogicalGates;

namespace LogicGatesAvalonia.Models.LogicalGatesFactory.ILogicalGateCreationCommand
{
    public class CreateLogicalXORCommand : BaseCreateLogicalGateCommand
    {
        public CreateLogicalXORCommand(string elementName, BaseDesiginationSymbol desiginationSymbol)
            : base(elementName, desiginationSymbol)
        {
        }
        public override BaseLogicalGate Create(string logicalGateUsername)
        {
            return new LogicalXOR(
                elementName,
                logicalGateUsername,
                desigination);
        }
    }
}
