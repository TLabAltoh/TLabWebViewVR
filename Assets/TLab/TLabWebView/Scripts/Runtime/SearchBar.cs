using UnityEngine;
using TMPro;
using TLab.InputField;

namespace TLab.Android.WebView
{
    public class SearchBar : TLabInputFieldBase
    {
        [Header("WebView")]
        [SerializeField] private TLabWebView m_webview;

        [Header("TextMeshPro")]
        [SerializeField] private TextMeshProUGUI m_searchBar;

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

        public override void OnEnterPressed()
        {
            LoadUrl();
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

        public void LoadUrl()
        {
            var https = "https://";
            var http = "http://";
            var hedder = "";
            if(m_searchBar.text.Length > http.Length)
            {
                if (m_searchBar.text.Substring(0, https.Length - 1) != https &&
                    m_searchBar.text.Substring(0, http.Length - 1) != http)
                {
                    hedder = "https://www.google.com/search?q=";
                }
            }
            else
            {
                hedder = "https://www.google.com/search?q=";
            }

            m_webview.LoadUrl(hedder + m_searchBar.text);
        }

        public void Display()
        {
            m_searchBar.text = m_text;
        }

        public void AddKey(string key)
        {
            m_text += key;
            Display();
        }
    }
}
