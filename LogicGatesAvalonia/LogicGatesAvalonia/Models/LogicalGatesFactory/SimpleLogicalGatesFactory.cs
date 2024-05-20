using LogicGatesAvalonia.Controls;
using LogicGatesAvalonia.Models.LogicalGatesFactory.FactoryExceptions;
using LogicGatesAvinternal.Controls;
using SkiaSharp;

namespace LogicGatesAvalonia.Models.LogicalGatesFactory
{
    public class SimpleLogicalGatesFactory
    {
        public BaseLogicalGateControl Create(string elementName)
        {
            switch(elementName)
            {
                case "Буфер":
                    return new LogicalBufferControl();
                case "И":
                    return new LogicalAndControl();
                case "ИЛИ":
                    return new LogicalOrControl();
                case "Инвертор":
                    return new LogicalInvertorControl();
                case "Исключающее ИЛИ":
                    return new LogicalXORControl();
                case "И-НЕ":
                    return new LogicalANDNOTControl();
                case "Исключающее ИЛИ-НЕ":
                    return new LogicalXNORControl();
                case "ИЛИ-НЕ":
                    return new LogicalORNOTControl();


                case "BUF":
                    return new ANSILogicalBufferControl();
                case "AND":
                    return new ANSILogicalANDControl();
                case "INV":
                    return new ANSILogicalInvertorControl();
                case "XOR":
                    return new ANSILogicalXORControl();
                case "NAND":
                    return new ANSILogicalANDNOTControl();
                case "OR":
                    return new ANSILogicalORControl();
                case "NOR":
                    return new ANSILogicalORNOTControl();
                case "XNOR":
                    return new ANSILogicalXNORControl();

                default:
                    throw new NotSupportedTypeOfArgumentException($"Can't create control with name: {elementName}");
            }
        }
    }
}
