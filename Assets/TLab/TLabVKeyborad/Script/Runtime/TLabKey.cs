using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TLab.InputField
{
    public class TLabKey : MonoBehaviour
    {
        [SerializeField] private string m_current;
        [SerializeField] private string m_lowercase;
        [SerializeField] private string m_uppercase;
        [SerializeField] private string m_lowercaseDisp;
        [SerializeField] private string m_uppercaseDisp;
        [SerializeField] private TextMeshProUGUI m_keyText;

        private List<string> m_inputBuffer;
        private bool m_isShiftOn = false;

#if UNITY_EDITOR
        public void Initialize()
        {
            UnityEditor.GameObjectUtility.RemoveMonoBehavioursWithMissingScript(this.gameObject);

            string[] split = this.gameObject.name.Split("_");
            switch (split.Length)
            {
                case 1:
                    m_lowercase = split[0];
                    m_uppercase = split[0];
                    break;
                case 2:
                    m_lowercase = split[0];
                    m_uppercase = split[1];
                    break;
            }

            m_lowercaseDisp = m_lowercase;
            m_uppercaseDisp = m_uppercase;

            m_current = m_lowercase;
            m_keyText = GetComponentInChildren<TextMeshProUGUI>();
        }
#endif

        public void SetKeyInputBuffer(List<string> inputBuffer)
        {
            m_inputBuffer = inputBuffer;
        }

        public void Press()
        {
            m_inputBuffer.Add(m_current);
        }

        public void ShiftPressed()
        {
            m_isShiftOn = !m_isShiftOn;
            m_current = m_isShiftOn ? m_uppercase : m_lowercase;
            m_keyText.text = m_isShiftOn ? m_uppercaseDisp : m_lowercaseDisp;
        }

        void OnEnable()
        {
            var button = GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(Press);
        }

        void OnDisable()
        {
            var button = GetComponent<UnityEngine.UI.Button>();
            button.onClick.RemoveAllListeners();
        }

        public static TLabKey[] Keys(GameObject target)
        {
            return target.GetComponentsInChildren<TLabKey>();
        }
    }
}
