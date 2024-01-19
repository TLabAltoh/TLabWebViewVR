using UnityEngine;
using TLab.InputField;

namespace TLab.Android.WebView
{
    public class TLabWebViewInputField : TLabInputFieldBase
    {
        [Header("WebView")]
        [SerializeField] private TLabWebView m_webview;

        #region KEY_EVENT

        public override void OnBackSpacePressed()
        {
            m_webview.BackSpace();
        }

        public override void OnEnterPressed()
        {
            AddKey("\n");
        }

        public override void OnSpacePressed()
        {
            AddKey(" ");
        }

        public override void OnTabPressed()
        {
            AddKey("\t");
        }

        public override void OnKeyPressed(string input)
        {
            AddKey(input);
        }

        #endregion KEY_EVENT

        public override void OnFocus()
        {
            var notActive = !inputFieldIsActive;

            if (m_keyborad.mobile && notActive)
            {
                m_keyborad.SwitchInputField(this);
                m_keyborad.HideKeyborad(false);
            }
        }

        public void AddKey(string key)
        {
            m_webview.KeyEvent(key.ToCharArray()[0]);
        }
    }
}