using UnityEngine;
using TLab.Android.WebView;

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
