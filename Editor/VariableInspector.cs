using UnityEngine;
using UnityEditor;

namespace GX.VarSystem
{
    [CustomEditor(typeof(Variable), true)]
    public class VariableInspector : Editor
    {
        protected Variable v;

        public override void OnInspectorGUI()
        {
            Variable v = target as Variable;
            v.name = EditorGUILayout.DelayedTextField("Name: ", v.name);

            v.isStatic = EditorGUILayout.Toggle("Static", v.isStatic);
            v.isReadOnly = EditorGUILayout.Toggle("Readonly", v.isReadOnly);
        }
    }
}
