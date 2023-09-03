using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TLab.Android.WebView;

public class AddEventListener : MonoBehaviour
{
    [SerializeField] private TLabWebView m_webview;

    public void LoadScrollEventListener()
    {
        // Added a listener to retrieve page scroll events

        /*
            const scrollNum = document.getElementById('scroll-num');

            window.addEventListener('scroll',function(){
              scrollNum.textContent = window.pageYOffset;
            });
        */
        m_webview.EvaluateJS("window.addEventListener('scroll',function(){ document.getElementById('scroll-num').textContent = window.pageYOffset;});");
    }
}
