using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GX.VarSystem
{
    [System.Serializable]
    public class VarHandle
    {
        //list of variables
        [SerializeField]
        private List<Variable> variables;

        public VarHandle(VarHandleTemplate template, object varHolder)
        {
            variables = new List<Variable>();
            foreach (Variable v in template)
            {
                Variable x = v.Duplicate(varHolder);
                variables.Add(x);
            }
        }
        
    
        public V Get<V>(int idx) { return (V)variables[idx].Value; }
        public void Set<V>(int idx, V v) { variables[idx].Value = v; }
    }
}
