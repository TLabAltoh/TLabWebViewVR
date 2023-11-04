using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;

namespace TLab.Android.WebView
{
	public class TLabWebView : MonoBehaviour
	{
		private enum DownloadOption
		{
			applicationFolder,
			downloadFolder
		}

		[SerializeField] private RawImage m_rawImage;
		[SerializeField] private string m_url = "https://youtube.com";

		[Header("File Download Settings")]
		[SerializeField] private DownloadOption m_dlOption;
		[SerializeField] private string m_subdir = "downloads";

		[Header("Resolution setting")]
		[SerializeField] private int m_webWidth = 1024;
		[SerializeField] private int m_webHeight = 1024;
		[SerializeField] private int m_texWidth = 512;
		[SerializeField] private int m_texHeight = 512;

		public int WebWidth { get => m_webWidth; }
		public int WebHeight { get => m_webHeight; }
		public int TexWidth { get => m_texWidth; }
		public int TexHeight { get => m_texHeight; }

		private bool m_webViewEnable;
		private Texture2D m_webViewTexture;

#if UNITY_ANDROID
		private AndroidJavaObject m_NativePlugin;
#endif

		public void Init(int webWidth, int webHeight, int tWidth, int tHeight, int sWidth, int sHeight, string url, int dlOption, string subDir)
		{
#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin = new AndroidJavaObject("com.tlab.libwebview.UnityConnect");
			m_NativePlugin.Call("initialize", webWidth, webHeight, tWidth, tHeight, sWidth, sHeight, url, dlOption, subDir);
#endif
		}

		public byte[] GetWebTexturePixel()
		{
#if UNITY_ANDROID
			if (Application.isEditor) return new byte[0];

			// https://www.dbu9.site/post/2023-03-31-androidjnihelper-getsignature-using-byte-parameters-is-obsolete-use-sbyte-parameters-instead/
			//sbyte[] sdata = m_NativePlugin.Call<sbyte[]>("getPixel");
			//byte[] data = new byte[sdata.Length];
			//Buffer.BlockCopy(sdata, 0, data, 0, sdata.Length);
			// https://stackoverflow.com/questions/829983/how-to-convert-a-sbyte-to-byte-in-c
			return (byte[])(Array)m_NativePlugin.Call<sbyte[]>("getPixel");
#else
			return null;
#endif
		}

		public void CaptureHTMLSource()
		{
			if (m_webViewEnable == false)
				return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("capturePage");
#endif
		}

		public void CaptureElementById(string id)
		{
			if (m_webViewEnable == false)
				return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("captureElementById", id);
#endif
		}

		public string CurrentHTMLCaptured()
		{
			if (m_webViewEnable == false)
				return null;

#if UNITY_ANDROID
			if (Application.isEditor) return null;

			return m_NativePlugin.Call<string>("getCaptured");
#else
			return null;
#endif
		}

		public void CaptureUserAgent()
		{
			if (m_webViewEnable == false)
				return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("captureUserAgent");
#endif
		}

		public string GetUserAgent()
		{
			if (m_webViewEnable == false)
				return "";

#if UNITY_ANDROID
			if (Application.isEditor) return "";

			return m_NativePlugin.Call<string>("getUserAgent");
#endif
		}

		public void SetUserAgent(string ua)
		{
			if (m_webViewEnable == false)
				return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("setUserAgent", ua);
#endif
		}

		public void LoadUrl(string url)
		{
			if (m_webViewEnable == false)
				return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("loadUrl", url);
#endif
		}

		public void LoadHTML(string html, string baseURL)
		{
			if (m_webViewEnable == false)
				return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("loadHtml", html, baseURL);
#endif
		}

		public void ZoomIn()
		{
			if (m_webViewEnable == false)
				return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("zoomIn");
#endif
		}

		public void ZoomOut()
		{
			if (m_webViewEnable == false)
				return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("zoomOut");
#endif
		}

		public void EvaluateJS(string js)
		{
			if (m_webViewEnable == false)
				return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("evaluateJS", js);
#endif
		}

		public void GoForward()
		{
			if (m_webViewEnable == false)
				return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("goForward");
#endif
		}

		public void GoBack()
		{
			if (m_webViewEnable == false)
				return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("goBack");
#endif
		}

		public void TouchEvent(int x, int y, int eventNum)
		{
			if (m_webViewEnable == false) return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("touchEvent", x, y, eventNum);
#endif
		}

		public void KeyEvent(char key)
		{
			if (m_webViewEnable == false) return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("keyEvent", key);
#endif
		}

		public void BackSpace()
		{
			if (m_webViewEnable == false) return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("backSpaceKey");
#endif
		}

		public void SetVisible(bool visible)
		{
			m_webViewEnable = visible;
#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("setVisible", visible);
#endif
		}

		public void ClearCache(bool includeDiskFiles)
		{
#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("clearCash", includeDiskFiles);
#endif
		}

		public void ClearCookie()
		{
#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("clearCookie");
#endif
		}

		public void ClearHistory()
		{
#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.Call("clearHistory");
#endif
		}

		public void StartWebView()
		{
			if (m_webViewEnable) return;

			m_webViewEnable = true;

#if UNITY_ANDROID
			Init(m_webWidth, m_webHeight, m_texWidth, m_texHeight, Screen.width, Screen.height, m_url, (int)m_dlOption, m_subdir);
			m_webViewTexture = new Texture2D(m_texWidth, m_texHeight, TextureFormat.ARGB32, false);
			m_webViewTexture.name = "WebImage";
			m_rawImage.texture = m_webViewTexture;
#endif
		}

		public void UpdateFrame()
		{
			if (!m_webViewEnable) return;

			byte[] data = GetWebTexturePixel();

			if (data.Length > 0)
			{
				m_webViewTexture.LoadRawTextureData(data);
				m_webViewTexture.Apply();
			}
		}

		private void OnDestroy()
		{
#if UNITY_ANDROID
			if (m_NativePlugin == null) return;

			m_NativePlugin.Call("Destroy");
			m_NativePlugin = null;

			Debug.Log("[tlabwebview] destroy webview");
#endif
		}

		private void OnApplicationQuit()
		{
#if UNITY_ANDROID
			if (m_NativePlugin == null) return;

			m_NativePlugin.Call("Destroy");
			m_NativePlugin = null;

			Debug.Log("[tlabwebview] destroy webview");
#endif
		}
	}
}
