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
        [SerializeField] private AudioClip m_lockKeyborad;

        private AudioSource m_audioSource;

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

            if (m_keyborad.mobile)
            {
                foreach (GameObject hideObject in m_hideObjects)
                    hideObject.SetActive(!active);

                m_keyborad.HideKeyborad(!active);
            }

            AudioUtility.ShotAudio(m_audioSource, m_lockKeyborad, IMMEDIATELY);
        }

        #endregion FOUCUS_EVENET

        private void SwitchPlaseholder()
        {
            var color = m_placeholder.color;

            if (m_inputText.text == "")
            {
                color.a = 0.5f;
                m_placeholder.color = color;
            }
            else
            {
                color.a = 0f;
                m_placeholder.color = color;
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
            base.Start();

            text = m_inputText.text;

            SwitchPlaseholder();

            if (m_audioSource == null)
                m_audioSource = GetComponent<AudioSource>();
        }
    }
}
