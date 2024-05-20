using LogicGatesAvalonia.Models.DesiginationSymbols;
using LogicGatesAvalonia.Models.LogicGates.TwoChannelLogicalGates;

namespace LogicGatesAvalonia.Models.LogicalGatesFactory.ILogicalGateCreationCommand
{
    public class CreateLogicalORCommand : BaseCreateLogicalGateCommand
    {
        public CreateLogicalORCommand(string elementName, BaseDesiginationSymbol desiginationSymbol)
            : base(elementName, desiginationSymbol)
        {
        }
        public override BaseLogicalGate Create(string logicalGateUsername)
        {
            return new LogicalOR(
                elementName,
                logicalGateUsername,
                desigination);
        }
    }
}
