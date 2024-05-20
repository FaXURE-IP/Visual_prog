using LogicGatesAvinternal.Controls;

namespace LogicGatesAvalonia.Controls.MoutionControlManupulator
{
    public abstract class BaseControlManipulator
    {
        protected bool isEnable = true;

        protected BaseLogicalGateControl control;

        protected BaseControlManipulator(BaseLogicalGateControl control)
        {
            this.control = control;
        }

        public void Enable()
        {
            this.isEnable = true;
        }
        public void Disable()
        {
            this.isEnable = false;
        }
    }
}
