using UnityEngine;
using UnityEditor;

namespace TLab.InputField.Editor
{
    [CustomEditor(typeof(TLabVKeyborad))]
    public class TLabVKeyboradEditor : UnityEditor.Editor
    {
        private TLabVKeyborad m_instance;

        private void OnEnable()
        {
            m_instance = target as TLabVKeyborad;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            if (GUILayout.Button("CheckTLabKeyExist"))
            {
                m_instance.CheckTLabKeyExist();
                EditorUtility.SetDirty(m_instance);
            }
        }
    }
}