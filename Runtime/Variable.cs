using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GX.VarSystem
{
    //container for variables
    [System.Serializable]
    public abstract class Variable : ScriptableObject
    {
        public bool isStatic, isReadOnly;
        public abstract object Value { get; set; } 

        public abstract Variable Duplicate(object varHolder);

        public abstract System.Type GetVarType();
    }

    [System.Serializable]
    public class Variable<T> : Variable
    {
        [SerializeReference]
        protected T value;

        public override object Value { get => value; set
            {
                if (!isReadOnly)
                    this.value = (T)value;
                else Debug.LogWarning("Tried to modify readonly variable " + name);
            }
        }

        public override Variable Duplicate(object varHolder) 
        {
            Variable<T> i = CreateInstance<Variable<T>>();
            i.value = value;
            return i;
        }

        public override System.Type GetVarType() { return typeof(T); }

        public static implicit operator T(Variable<T> v) { return v.value; }
    }
}
