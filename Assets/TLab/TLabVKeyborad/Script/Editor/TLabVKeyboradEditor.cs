using UnityEngine;
using UnityEditor;

namespace TLab.InputField.Editor
{
    [CustomEditor(typeof(TLabVKeyborad))]
    public class TLabVKeyboradEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.Space();

            TLabVKeyborad inputField = target as TLabVKeyborad;
            if (GUILayout.Button("Initialize"))
            {
                inputField.InitializeTLabKey();
                EditorUtility.SetDirty(inputField);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}