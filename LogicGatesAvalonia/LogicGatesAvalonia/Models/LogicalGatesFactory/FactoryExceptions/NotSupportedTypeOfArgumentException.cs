using System;

namespace LogicGatesAvalonia.Models.LogicalGatesFactory.FactoryExceptions
{
    public class NotSupportedTypeOfArgumentException : Exception
    {
        public NotSupportedTypeOfArgumentException(string message)
        : base(message) { }
    }
}
