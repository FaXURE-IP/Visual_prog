using LogicGatesAvalonia.Models.DesiginationSymbols;
using LogicGatesAvalonia.Models.LogicalGatesFactory.ILogicalGateCreationCommand;
using LogicGatesAvalonia.Models.LogicGates.OneChannelLogicalGates;
using LogicGatesAvalonia.Models.LogicGates.TwoChannelLogicalGates;

namespace LogicGatesAvalonia.Models.LogicalGatesFactory.LogicGatePrototypes
{
    public class ANSILogicGatesPrototypes : BaseLogicGatesPrototypes
    {
        public ANSILogicGatesPrototypes()
        {
            mapTypeToName.Add(typeof(LogicalBuffer), new CreateLogicalBufferCommand("BUF", new ANSIDesigination()));
            mapTypeToName.Add(typeof(LogicalAND), new CreateLogicalANDCommand("AND", new ANSIDesigination()));
            mapTypeToName.Add(typeof(LogicalInvertor), new CreateLogicalInvertorCommand("INV", new ANSIDesigination()));
            mapTypeToName.Add(typeof(LogicalXOR), new CreateLogicalXORCommand("XOR", new ANSIDesigination()));
            mapTypeToName.Add(typeof(LogicalANDNOT),new CreateLogicalANDNOTCommand("NAND",new ANSIDesigination()));
            mapTypeToName.Add(typeof(LogicalOR), new CreateLogicalORCommand("OR",new ANSIDesigination()));
            mapTypeToName.Add(typeof(LogicalORNOT), new CreateLogicalORNOTCommand("NOR",new ANSIDesigination()));
            mapTypeToName.Add(typeof(LogicalXNOR), new CreateLogicalXNORCommand("XNOR",new ANSIDesigination()));
        }
    }
}
