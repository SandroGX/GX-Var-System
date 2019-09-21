using UnityEngine;

namespace GX.VarSystem
{
    public class VarComponent : Variable
    {
        public System.Type compType;

        public override Variable Duplicate(object varHolder)
        {
            if (isStatic)
            {
                if (va == null)

                    return this;
            }

            VarComponent v = CreateInstance<VarComponent>();
            v.va = ((Component)varHolder).GetComponent(compType);
            return v;
        }
    }
}
