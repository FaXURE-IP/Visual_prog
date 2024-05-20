using LogicGatesAvalonia.Models.DesiginationSymbols;

namespace LogicGatesAvalonia.Models.LogicalGatesFactory
{
    public abstract class BaseLogicalGatesFactory<T> where T : BaseLogicalGate
    {
        public abstract BaseLogicalGate Create(string logicalGateUsername);
    }
}
