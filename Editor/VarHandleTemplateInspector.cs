using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace GX.VarSystem
{
    [CustomEditor(typeof(VarHandleTemplateEditor))]
    public class VarHandleTemplateInspector : Editor
    {
        private Rect buttonRect;
        
        public override void OnInspectorGUI()
        {
            VarHandleTemplateEditor varHandle = (VarHandleTemplateEditor)target;
            int toRemove = -1;
            //show variable settings (if not hidden)
            for (int i = 0; i < varHandle.varHandle.Count; ++i)
            {
                if (varHandle.fold[i] = EditorGUILayout.Foldout(varHandle.fold[i], varHandle.varHandle[i].name))
                {
                    EditorGUI.indentLevel++;

                    CreateEditor(varHandle.varHandle[i]).OnInspectorGUI();
                    if (GUILayout.Button("Remove"))
                        toRemove = i;

                    EditorGUI.indentLevel--;
                }
            }

            if (toRemove >= 0)
                varHandle.Remove(toRemove);

            GUILayout.FlexibleSpace();
            //show add variable popup
            if (GUILayout.Button("Add"))
                PopupWindow.Show(buttonRect, new AddVariablePopup(varHandle));
            if (Event.current.type == EventType.Repaint)
                buttonRect = GUILayoutUtility.GetLastRect();
        }

        public static void ShowVars(object o, VarHandleTemplate vh)
        {
            foreach(FieldInfo fi in o.GetType().GetFields())
            {
                ShowVarAcc(o, vh, fi);
                ShowVarAtt(o, vh, fi);
            }
        }

        public static void ShowVarAcc(object o, VarHandleTemplate vh, FieldInfo fi)
        {
            
            if (!(fi.FieldType.IsGenericType && fi.FieldType.GetGenericTypeDefinition() == typeof(VarAccesser<>)))
                return;

            List<Variable> var = vh.Variables;
            List<Variable> u = var.Where(x => fi.FieldType.GenericTypeArguments[0].IsAssignableFrom(x.GetVarType())).ToList();

            if (u.Count <= 0)
            {
                EditorGUILayout.LabelField($"{fi.Name}:  No variables of type {fi.FieldType.GenericTypeArguments[0].Name} found");
                return;
            }

            object va = fi.GetValue(o);
            FieldInfo idxF = fi.FieldType.GetField("idx");

            int varIdx = (int)idxF.GetValue(va);
            int uIdx = varIdx >= 0 && varIdx < var.Count ? u.IndexOf(var[varIdx]) : 0;
            uIdx = EditorGUILayout.Popup(fi.Name, uIdx >= 0 && uIdx < u.Count ? uIdx : 0, u.Select(x => x.name).ToArray());

            //idxF.SetValue(va, var.IndexOf(u[uIdx]));
            object r = System.Activator.CreateInstance(fi.FieldType, var.IndexOf(u[uIdx]));
            fi.SetValue(o, r);
            
        }

        public static void ShowVarAtt(object o, VarHandleTemplate vh, FieldInfo fi)
        {
            VarIdxAttribute a = fi.GetCustomAttribute<VarIdxAttribute>();
            if (typeof(int) != fi.FieldType || a == null)
                return;

            List<Variable> var = vh.Variables;
            List<Variable> u = var.Where(x => a.t.IsAssignableFrom(x.GetVarType())).ToList();

            int varIdx = (int)fi.GetValue(o);
            int uIdx = varIdx >= 0 && varIdx < var.Count ? u.IndexOf(var[varIdx]) : 0;
            uIdx = EditorGUILayout.Popup(fi.Name, uIdx >= 0 && uIdx < u.Count ? uIdx : 0, u.Select(x => x.name).ToArray());

            fi.SetValue(o, var.IndexOf(u[uIdx]));
        }
    }
}
