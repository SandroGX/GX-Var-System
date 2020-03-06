using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace GX.VarSystem
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class VarIdxAttribute : Attribute
    {
        public readonly Type t;
        public VarIdxAttribute(Type t)
        {
            this.t = t;
        }
    }
}
