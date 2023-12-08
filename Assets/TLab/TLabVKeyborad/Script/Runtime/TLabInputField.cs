using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TLab.InputField
{
    [RequireComponent(typeof(AudioSource))]
    public class TLabInputField : TLabInputFieldBase
    {
        [Header("Text (TMPro)")]
        [SerializeField] private TextMeshProUGUI m_inputText;
        [SerializeField] private TextMeshProUGUI m_placeholder;

        [Header("Image")]
        [SerializeField] private GameObject m_openImage;
        [SerializeField] private GameObject m_lockImage;

        [Header("Button")]
        [SerializeField] private Button m_focusButton;

        [Header("HideObject")]
        [SerializeField] private GameObject[] m_hideObjects;

        [Header("Audio")]
        [SerializeField] private AudioSource m_audioSource;
        [SerializeField] private AudioClip m_lockKeyborad;

        [System.NonSerialized] public string text = "";

        private const float IMMEDIATELY = 0f;

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

        public override void OnFocus(bool active)
        {
            base.OnFocus(active);

            m_openImage.SetActive(active);
            m_lockImage.SetActive(!active);

            m_focusButton.enabled = !active;

            if (m_keyborad.isMobile)
            {
                foreach (GameObject hideObject in m_hideObjects)
                {
                    hideObject.SetActive(!active);
                }

                m_keyborad.HideKeyborad(!active);
            }

            AudioUtility.ShotAudio(m_audioSource, m_lockKeyborad, IMMEDIATELY);
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
