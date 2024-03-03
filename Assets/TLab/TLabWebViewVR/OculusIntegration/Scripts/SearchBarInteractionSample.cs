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
            var elements = [];

            function searchShadowRoot(node, id) {
                if (node == null) {
                    return;
                }

                if (node.shadowRoot != undefined && node.shadowRoot != null) {
                    if (!elements.includes(node.shadowRoot)) {
                        elements.push(node.shadowRoot);
                    }
                    searchShadowRoot(node.shadowRoot, id);
                }

                for (var i = 0; i < node.childNodes.length; i++) {
                    searchShadowRoot(node.childNodes[i], id);
                }
            }

            elements.push(document);
            searchShadowRoot(document, 'input');
            searchShadowRoot(document, 'textarea');

            for (var i = 0; i < elements.length; i++) {
                elements[i].addEventListener('focusin', (e) => {
                    const target = e.target;
                    if (target.tagName == 'INPUT' || target.tagName == 'TEXTAREA') {
                       window.TLabWebViewActivity.unitySendMessage('SearchBar Interaction Sample', 'OnMessage', 'Foucusin');
                    }
                });
    
                elements[i].addEventListener('focusout', (e) => {
                    const target = e.target;
                    if (target.tagName == 'INPUT' || target.tagName == 'TEXTAREA') {
                       window.TLabWebViewActivity.unitySendMessage('SearchBar Interaction Sample', 'OnMessage', 'Foucusout');
                    }
                });
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
