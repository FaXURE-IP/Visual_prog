using LogicGatesAvalonia.Models.DesiginationSymbols;
using LogicGatesAvalonia.Models.LogicGates.TwoChannelLogicalGates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicGatesAvalonia.Models.LogicalGatesFactory.ILogicalGateCreationCommand
{
    public class CreateLogicalORNOTCommand : BaseCreateLogicalGateCommand
    {
        public CreateLogicalORNOTCommand(string elementName, BaseDesiginationSymbol desiginationSymbol)
            : base(elementName, desiginationSymbol)
        {
        }
        public override BaseLogicalGate Create(string logicalGateUsername)
        {
            return new LogicalORNOT(
                elementName,
                logicalGateUsername,
                desigination);
        }
    }
}
