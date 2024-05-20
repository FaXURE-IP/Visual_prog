using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicGatesAvalonia.Models.LogicGates.LogicalComponents
{
    public class BaseLogicalComponent
    {
        protected int number;
        protected string name;
        public object Value { get; protected set; }

        protected BaseLogicalComponent(int number, string name)
        {
            this.number = number;
            this.name = name;
        }

        public virtual void SetLogicalValue(object value)
        {
            this.Value = value;
        }
    }
}
