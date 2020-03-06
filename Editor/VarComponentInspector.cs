using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GX.VarSystem
{
    [CustomEditor(typeof(VarComponent))]
    public class VarComponentInspector : VariableInspector
    {
        private static readonly List<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsSubclassOf(typeof(Component)) && !x.IsAbstract).ToList();
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            VarComponent vc = (VarComponent)target;

            Type t = vc.compType != null ? Type.GetType(vc.compType) : types[0];
            if (t == null)
                t = types[0];

            t = types[EditorGUILayout.Popup("Component Type:", types.IndexOf(t), types.Select(x => x.Name).ToArray())];
            vc.compType = t.AssemblyQualifiedName;
        }
    }
}
