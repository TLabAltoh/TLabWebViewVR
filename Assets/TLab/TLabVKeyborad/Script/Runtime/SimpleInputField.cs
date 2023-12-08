using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TLab.InputField
{
    [RequireComponent(typeof(AudioSource))]
    public class SimpleInputField : TLabInputFieldBase
    {
        [Header("Text (TMPro)")]
        [SerializeField] private TextMeshProUGUI m_inputText;
        [SerializeField] private TextMeshProUGUI m_placeholder;

        [Header("Button")]
        [SerializeField] private Button m_focusButton;

        [Header("Audio")]
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_lockKeyborad;

        private const float IMMEDIATELY = 0f;

        [System.NonSerialized] public string text = "";

        #region KEY_EVENT

        public override void OnBackSpacePressed()
        {
            if (text != "")
            {
                text = text.Remove(text.Length - 1);
                Display();
            }
        }

        public override void OnSpacePressed() => AddKey(" ");

        public override void OnTabPressed() => AddKey("    ");

        public override void OnKeyPressed(string input) => AddKey(input);

        #endregion KEY_EVENT

        #region FOUCUS_EVENET

        private void OnFocusAudio() => AudioUtility.ShotAudio(m_audioSource, m_lockKeyborad, IMMEDIATELY);

        public override void OnFocus(bool active)
        {
            base.OnFocus(active);

            m_focusButton.enabled = !active;

            var hide = !inputFieldIsActive;

            if (m_keyborad.isMobile)
            {
                m_keyborad.HideKeyborad(hide);
            }

            OnFocusAudio();
        }

        public override void OnFocus()
        {
            base.OnFocus();

            var hide = !inputFieldIsActive;

            if (m_keyborad.isMobile)
            {
                m_keyborad.HideKeyborad(hide);
            }

            OnFocusAudio();
        }

        #endregion FOUCUS_EVENET

        private void SwitchPlaseholder()
        {
            if (m_inputText.text == "")
            {
                m_placeholder.color = new Color(0.196f, 0.196f, 0.196f, 0.5f);
            }
            else
            {
                m_placeholder.color = new Color(0.196f, 0.196f, 0.196f, 0.0f);
            }
        }

        public void Display()
        {
            m_inputText.text = text;

            SwitchPlaseholder();
        }

        public void AddKey(string key)
        {
            text += key;
            Display();
        }

        public void SetPlaceHolder(string text)
        {
            this.text = "";
            m_placeholder.text = text;
            Display();
        }

        protected override void Start()
        {
            OnFocus(false);

            text = m_inputText.text;

            SwitchPlaseholder();
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            if (m_audioSource == null)
            {
                m_audioSource = GetComponent<AudioSource>();
                UnityEditor.EditorUtility.SetDirty(this);
            }
        }
#endif
    }
}
