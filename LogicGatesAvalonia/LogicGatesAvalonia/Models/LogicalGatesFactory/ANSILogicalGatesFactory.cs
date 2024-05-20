using LogicGatesAvalonia.Models.LogicalGatesFactory.FactoryExceptions;
using LogicGatesAvalonia.Models.LogicalGatesFactory.LogicGatePrototypes;

namespace LogicGatesAvalonia.Models.LogicalGatesFactory
{
    public class ANSILogicalGatesFactory<T> : BaseLogicalGatesFactory<T> where T : BaseLogicalGate
    {
        public override BaseLogicalGate Create(string logicalGateUsername)
        {
            try
            {
                var prototypes = new ANSILogicGatesPrototypes();
                var creatiomCommand = prototypes.GetCreationCommand(typeof(T));
                return creatiomCommand.Create(logicalGateUsername);
            }
            catch (NotSupportedTypeOfArgumentException ex)
            {
                throw new NotSupportedTypeOfArgumentException($"Can't create object of " +
                $"argument type: {typeof(T).Name}");
            }
        }
    }
}
