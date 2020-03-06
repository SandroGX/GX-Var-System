using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace GX.VarSystem {
    //popup to add variable to template
    public class AddVariablePopup : PopupWindowContent
    {
        //List of all types inheriting from Variable
        private static readonly List<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsSubclassOf(typeof(Variable)) && !x.IsAbstract)
            .ToList();
        //handle to add variable
        private readonly VarHandleTemplateEditor handle;
        //currently selected type
        private Type variableType = types[0];

        //create popup for handle
        public AddVariablePopup(VarHandleTemplateEditor handle) { this.handle = handle; }

        //popup gui, TODO: add code to separate variables types into categories, probably use Attributes and/or namespaces
        public override void OnGUI(Rect rect)
        {
            variableType = types[EditorGUILayout.Popup("New Variable Type:", types.IndexOf(variableType), types.Select(x => x.Name).ToArray())]; //list all types and select one

            if(GUILayout.Button("Add Variable"))
            {
                handle.Add(variableType);
                editorWindow.Close();
            }
        }
    }
}