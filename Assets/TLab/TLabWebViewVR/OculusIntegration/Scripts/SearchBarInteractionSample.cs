using UnityEngine;
using TMPro;
using TLab.Android.WebView;

public class SearchBarInteractionSample : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private TLabWebView m_webview;

    /// <summary>
    /// Press button to execute
    /// </summary>
    public void AddEventListener()
    {
        string jsCode = @"
            var inputs = [];

            function querySelectorAllForAllTree(node, id) {
                if (node == null) {
                    return;
                }

                if (node.shadowRoot != undefined && node.shadowRoot != null) {
                    querySelectorAllForAllTree(node.shadowRoot, id);
                }

                for (var i = 0; i < node.childNodes.length; i++) {
                    querySelectorAllForAllTree(node.childNodes[i], id);
                }

                if (typeof node.querySelector == 'function') {
                    var element = node.querySelector(id);
                    if (element != undefined && element != null) {
                        if (!inputs.includes(element)) {
                            inputs.push(element);
                        }
                    }
                }
            }

            querySelectorAllForAllTree(document, 'textarea');
            querySelectorAllForAllTree(document, 'input');

            function focusin() {
                window.TLabWebViewActivity.unitySendMessage('SearchBar Interaction Sample', 'OnMessage', 'Foucusin');
            }

            function focusout() {
                window.TLabWebViewActivity.unitySendMessage('SearchBar Interaction Sample', 'OnMessage', 'Foucusout');
            }

            for (const input of inputs) {
                input.addEventListener('focusin', focusin);
                input.addEventListener('focusout', focusout);
            }

            window.TLabWebViewActivity.unitySendMessage('SearchBar Interaction Sample', 'OnMessage', 'Executed');
            ";

        m_webview.EvaluateJS(jsCode);
    }

    public void OnMessage(string message)
    {
        Debug.Log(message);

        m_text.text = message;
    }
}
