using System;
using System.Collections.Generic;

using LogicGatesAvalonia.Models.LogicalGatesFactory.FactoryExceptions;

namespace LogicGatesAvalonia.Models.LogicalGatesFactory.LogicGatePrototypes
{
    public abstract class BaseLogicGatesPrototypes
    {
        protected Dictionary<Type, ILogicalGateCreationCommand.ILogicalGateCreationCommand> mapTypeToName
            = new Dictionary<Type, ILogicalGateCreationCommand.ILogicalGateCreationCommand>();

        private bool ContainsPrototype(Type type)
        {
            return mapTypeToName.ContainsKey(type);
        }

        public ILogicalGateCreationCommand.ILogicalGateCreationCommand GetCreationCommand(Type type)
        {
            if (ContainsPrototype(type))
            {
                return mapTypeToName[type];
            }

            throw new NotSupportedTypeOfArgumentException($"Not matches for object of type:" +
                $"{type.Name}");
        }
    }
}
