using LogicGatesAvalonia.Models.DesiginationSymbols;
using LogicGatesAvalonia.Models.LogicalGatesFactory.ILogicalGateCreationCommand;
using LogicGatesAvalonia.Models.LogicGates.OneChannelLogicalGates;
using LogicGatesAvalonia.Models.LogicGates.TwoChannelLogicalGates;

namespace LogicGatesAvalonia.Models.LogicalGatesFactory.LogicGatePrototypes
{
    public class GOSTLogicGatesPrototypes : BaseLogicGatesPrototypes
    {
        public GOSTLogicGatesPrototypes()
        {
            mapTypeToName.Add(typeof(LogicalBuffer),new CreateLogicalBufferCommand("Буфер",new GOSTDesigination()));
            mapTypeToName.Add(typeof(LogicalAND),new CreateLogicalANDCommand("И", new GOSTDesigination()));
            mapTypeToName.Add(typeof(LogicalInvertor),new CreateLogicalInvertorCommand("Инвертор", new GOSTDesigination()));
            mapTypeToName.Add(typeof(LogicalXOR),new CreateLogicalXORCommand("Исключающее ИЛИ",new GOSTDesigination()));
            mapTypeToName.Add(typeof(LogicalANDNOT),new CreateLogicalANDNOTCommand("И-НЕ",new GOSTDesigination()));
            mapTypeToName.Add(typeof(LogicalOR),new CreateLogicalORCommand("ИЛИ",new GOSTDesigination()));
            mapTypeToName.Add(typeof(LogicalORNOT),new CreateLogicalORNOTCommand("ИЛИ-НЕ",new GOSTDesigination()));
            mapTypeToName.Add(typeof(LogicalXNOR),new CreateLogicalXNORCommand("Исключающее ИЛИ-НЕ",new GOSTDesigination()));
        }
    }
}
