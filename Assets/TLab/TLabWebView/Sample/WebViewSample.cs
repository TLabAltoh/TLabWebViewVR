using UnityEngine;
using TLab.Android.WebView;
using System.Threading.Tasks;

public class WebViewSample : MonoBehaviour
{
    [SerializeField] TLabWebView m_webView;

    public void LoadHTML()
    {
        string html = "";
        string baseHRL = "";

        m_webView.LoadHTML(html, baseHRL);
    }

    public void ClearCache()
    {
        m_webView.ClearCache(true);
    }

    public void ClearCookies()
    {
        m_webView.ClearCookie();
    }

    public void ClearHistory()
    {
        m_webView.ClearHistory();
    }

    public void CaptureHTMLSource()
    {
        // capture html source

#if true
        m_webView.CaptureHTMLSource();
#else
        m_webView.CaptureElementById("swell_blocks-css");
#endif
    }

    public void CurrentHTMLCaptured()
    {
        // get current source captured

        Debug.Log("Current Captured: " +
#if true
            m_webView.CurrentHTMLCaptured()
#else
            m_webView.CurrentHTMLCaptured().Length.ToString()
#endif
        );
    }

    public async void GetUserAgent()
    {
        m_webView.CaptureUserAgent();

        await Task.Delay(500);
        Debug.Log("User Agent: " + m_webView.GetUserAgent());
    }

    public void SetUserAgent(string ua)
    {
        m_webView.SetUserAgent("Mozilla/5.0 (X11; Linux i686; rv:10.0) Gecko/20100101 Firefox/10.0");
    }

    public void AddEventListener()
    {
        // Added a listener to retrieve page scroll events

        /*
            const scrollNum = document.getElementById('scroll-num');

            window.addEventListener('scroll',function(){
              scrollNum.textContent = window.pageYOffset;
            });
        */

        string eventName = "scroll";
        string tagName = "window";
        string callback = "window.addEventListener('scroll',function(){ });";

        m_webView.EvaluateJS(tagName + ".addEventListener('" + eventName + "'), function() {" + callback + "});");
    }
}
