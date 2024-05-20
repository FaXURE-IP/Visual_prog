using LogicGatesAvalonia.Models.DesiginationSymbols;
using LogicGatesAvalonia.Models.LogicGates.OneChannelLogicalGates;

namespace LogicGatesAvalonia.Models.LogicalGatesFactory.ILogicalGateCreationCommand
{
    public class CreateLogicalBufferCommand : BaseCreateLogicalGateCommand
    {
        public CreateLogicalBufferCommand(string elementName, BaseDesiginationSymbol desigination) : 
            base(elementName, desigination) { }
        public override BaseLogicalGate Create(string logicalGateUsername)
        {
            return new LogicalBuffer(
                elementName,
                logicalGateUsername,
                desigination);
        }
    }
}
