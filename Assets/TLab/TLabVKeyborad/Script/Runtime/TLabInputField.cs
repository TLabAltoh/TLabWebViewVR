using UnityEngine;
using TMPro;

namespace TLab.InputField
{
    public class TLabInputField : TLabInputFieldBase
    {
        [Header("Text (TMPro)")]
        [SerializeField] private TextMeshProUGUI m_inputText;
        [SerializeField] private TextMeshProUGUI m_placeholder;

        [Header("Image")]
        [SerializeField] private GameObject m_openImage;
        [SerializeField] private GameObject m_lockImage;

        [Header("Button")]
        [SerializeField] private GameObject m_inputFieldButton;

        [Header("HideObject")]
        [SerializeField] private GameObject[] m_hideObjects;

        [Header("Audio")]
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_lockKeyborad;

        [System.NonSerialized] public string m_text = "";

        #region KEY_EVENT

        public override void OnBackSpacePressed()
        {
            if (m_text != "")
            {
                m_text = m_text.Remove(m_text.Length - 1);
                Display();
            }
        }

        public override void OnSpacePressed()
        {
            AddKey(" ");
        }

        public override void OnTabPressed()
        {
            AddKey("    ");
        }

        public override void OnKeyPressed(string input)
        {
            AddKey(input);
        }

        #endregion KEY_EVENT

        #region FOUCUS_EVENET

        public override void OnFocus(bool active)
        {
            base.OnFocus(active);

            m_openImage.SetActive(active);
            m_lockImage.SetActive(!active);
            m_inputFieldButton.SetActive(!active);

            if (m_keyborad.isMobile)
            {
                foreach (GameObject hideObject in m_hideObjects)
                {
                    hideObject.SetActive(!active);
                }

                m_keyborad.HideKeyborad(!active);
            }

            AudioUtility.ShotAudio(m_audioSource, m_lockKeyborad, 0.0f);
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
            m_inputText.text = m_text;

            SwitchPlaseholder();
        }

        public void AddKey(string key)
        {
            m_text += key;
            Display();
        }

        public void SetPlaceHolder(string text)
        {
            this.m_text = "";
            m_placeholder.text = text;
            Display();
        }

        protected override void Start()
        {
            OnFocus(false);

            m_text = m_inputText.text;

            SwitchPlaseholder();
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            m_audioSource = GetComponent<AudioSource>();
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}
