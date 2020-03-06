using System;
using UnityEngine;

namespace GX.VarSystem
{
    [System.Serializable]
    public class VarComponent : Variable<Component>
    {
        [HideInInspector]
        public string compType;

        public override Variable Duplicate(object varHolder)
        {
            if (isStatic)
            {
                if (value == null)
                    value = ((Component)varHolder).GetComponent(GetVarType());
                return this;
            }

            VarComponent v = CreateInstance<VarComponent>();
            v.compType = compType;
            v.value = ((Component)varHolder).GetComponent(GetVarType());
            return v;
        }

        public override Type GetVarType()
        {
            return Type.GetType(compType);
        }
    }
}
