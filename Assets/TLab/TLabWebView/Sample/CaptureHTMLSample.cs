using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLab.Android.WebView;

public class CaptureHTMLSample : MonoBehaviour
{
    [SerializeField] TLabWebView m_webView;

    public void Capture()
    {
#if true
        m_webView.CaptureHTMLSource();
#else
        m_webView.CaptureElementById("swell_blocks-css");
#endif
    }

    public void GetCaptured()
    {
        Debug.Log("Current Captured: " +
#if true
            m_webView.CurrentHTMLCaptured()
#else
            m_webView.CurrentHTMLCaptured().Length.ToString()
#endif
        );
    }
}
