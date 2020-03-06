using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GX;

namespace GX.VarSystem
{
    [System.Serializable]
    public class VarHandleTemplate : ScriptableObject, IEnumerable<Variable>
    {
        //list of variables
        [SerializeField]
        private List<Variable> variables = new List<Variable>();

        public List<Variable> Variables { get => variables; }

        public int Count { get => variables.Count; }

        public Variable this[int i] { get => variables[i]; }

        public VarHandle Build(object varHolder)
        {
            return new VarHandle(this, varHolder);
        }

        public IEnumerator<Variable> GetEnumerator()
        {
            return ((IEnumerable<Variable>)variables).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Variable>)variables).GetEnumerator();
        }
    }

}