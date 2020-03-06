using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GX.VarSystem
{
    public class VarHandleTemplateEditor : ScriptableObject
    {
        public VarHandleTemplate varHandle;
        public List<bool> fold = new List<bool>();

        public static VarHandleTemplateEditor GetCreateEditor(VarHandleTemplate vh)
        {
            VarHandleTemplateEditor vhe = SODatabase.GetSubObjectOfType<VarHandleTemplateEditor>(vh);

            if (vhe)
                return vhe;

            vhe = CreateInstance<VarHandleTemplateEditor>();
            vhe.varHandle = vh;
            vhe.name = vh.name + "Editor";
            AssetDatabase.AddObjectToAsset(vhe, vh);

            return vhe;
        }

        public void Add(System.Type varType)
        {
            if (!varType.IsSubclassOf(typeof(Variable)))
                return;

            Variable v = (Variable)CreateInstance(varType);
            SODatabase.Add(varHandle, v, varHandle.Variables);
            v.name = "Variable";
            fold.Add(true);
        }

        public void Remove(int idx)
        {
            SODatabase.Remove(varHandle, varHandle.Variables[idx], varHandle.Variables);
            fold.RemoveAt(idx);
        }

        public void Save()
        {
            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(varHandle);
            foreach (Variable v in varHandle)
                EditorUtility.SetDirty(v);

            AssetDatabase.SaveAssets();
        }
    }
}
