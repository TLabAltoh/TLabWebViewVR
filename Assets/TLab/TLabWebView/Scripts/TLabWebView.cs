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

		[SerializeField] private RawImage WebViewRawImage;
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

		private bool m_WebViewEnable;
		private Texture2D m_webViewTexture;

#if UNITY_ANDROID
		private AndroidJavaObject m_NativePlugin;
#endif

		public void Init(int webWidth, int webHeight, int tWidth, int tHeight, int sWidth, int sHeight, string url, int dlOption, string subDir)
		{
#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin = new AndroidJavaObject("com.tlab.libwebview.UnityConnect");
			m_NativePlugin.CallStatic("initialize", webWidth, webHeight, tWidth, tHeight, sWidth, sHeight, url, dlOption, subDir);
#endif
		}

		public byte[] GetWebTexturePixel()
		{
#if UNITY_ANDROID
			if (Application.isEditor) return new byte[0];

			return m_NativePlugin.CallStatic<byte[]>("getPixel");
#else
		return null;
#endif
		}

		public void LoadUrl(string url)
		{
			if (m_WebViewEnable == false)
				return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.CallStatic("loadUrl", url);
#endif
		}

		public void ZoomIn()
		{
			if (m_WebViewEnable == false)
				return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.CallStatic("zoomIn");
#endif
		}

		public void ZoomOut()
		{
			if (m_WebViewEnable == false)
				return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.CallStatic("zoomOut");
#endif
		}

		public void EvaluateJS(string js)
        {
			if (m_WebViewEnable == false)
				return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.CallStatic("evaluateJS", js);
#endif
		}

		public void GoForward()
		{
			if (m_WebViewEnable == false)
				return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.CallStatic("goForward");
#endif
		}

		public void GoBack()
		{
			if (m_WebViewEnable == false)
				return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.CallStatic("goBack");
#endif
		}

		public void TouchEvent(int x, int y, int eventNum)
		{
			if (m_WebViewEnable == false) return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.CallStatic("touchEvent", x, y, eventNum);
#endif
		}

		public void KeyEvent(char key)
		{
			if (m_WebViewEnable == false) return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.CallStatic("keyEvent", key);
#endif
		}

		public void BackSpace()
		{
			if (m_WebViewEnable == false) return;

#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.CallStatic("backSpaceKey");
#endif
		}

		public void SetVisible(bool visible)
		{
			m_WebViewEnable = visible;
#if UNITY_ANDROID
			if (Application.isEditor) return;

			m_NativePlugin.CallStatic("setVisible", visible);
#endif
		}

		public void StartWebView()
		{
			if (m_WebViewEnable) return;

			m_WebViewEnable = true;

#if UNITY_ANDROID
			Init(m_webWidth, m_webHeight, m_texWidth, m_texHeight, Screen.width, Screen.height, m_url, (int)m_dlOption, m_subdir);
			m_webViewTexture = new Texture2D(m_texWidth, m_texHeight, TextureFormat.ARGB32, false);
			m_webViewTexture.name = "WebImage";
			WebViewRawImage.texture = m_webViewTexture;
#endif
		}

		public void UpdateFrame()
		{
			if (!m_WebViewEnable) return;

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
