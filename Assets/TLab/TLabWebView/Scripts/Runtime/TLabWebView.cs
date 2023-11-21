#define DEBUG
//#undef DEBUG
#define ADVANCED
//#undef ADVANCED

using System.Collections;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
		public bool WebViewEnabled
		{
			get => m_webViewEnable;
            set
            {
				m_webViewEnable = value && m_webViewInitialized;
            }
		}

		private bool m_webViewEnable = false;
		private bool m_webViewInitialized = false;
		private Texture2D m_webViewTexture;
		private Coroutine m_webvieStartTask;

		private IntPtr m_texId = IntPtr.Zero;

#if UNITY_ANDROID
		private static AndroidJavaClass m_NativeClass;
		private AndroidJavaObject m_NativePlugin;
#endif

		private delegate void CheckEGLContextExist();
		private delegate void RenderEventDelegate(int eventIDint);
		private static RenderEventDelegate RenderThreadHandle = new RenderEventDelegate(RunOnRenderThread);
		private static IntPtr RenderThreadHandlePtr = Marshal.GetFunctionPointerForDelegate(RenderThreadHandle);

		private static jvalue[] m_jniArgs;

		[AOT.MonoPInvokeCallback(typeof(RenderEventDelegate))]
		private static void RunOnRenderThread(int eventID)
		{
			AndroidJNI.AttachCurrentThread();

			IntPtr jniClass = AndroidJNI.FindClass("com/tlab/libwebview/UnityConnect");
			if (jniClass != IntPtr.Zero && jniClass != null) Debug.Log("jni class found ! : " + jniClass);
			else return;

			IntPtr jniFunc;
			jniFunc = AndroidJNI.GetStaticMethodID(jniClass, "checkEGLContextExist", "()V");
			if (jniFunc != IntPtr.Zero && jniFunc != null) Debug.Log("jni function found ! : " + jniFunc);
			else return;

			AndroidJNI.CallStaticVoidMethod(jniClass, jniFunc, new jvalue[] { });
			Debug.Log("jni call method done !");

			//jniFunc = AndroidJNI.GetStaticMethodID(jniClass, "generateSharedTexture", "(Ljava/lang/Integer;Ljava/lang/Integer;)V");
			jniFunc = AndroidJNI.GetStaticMethodID(jniClass, "generateSharedTexture", "(II)V");
			if (jniFunc != IntPtr.Zero && jniFunc != null) Debug.Log("jni function found ! : " + jniFunc);
			else return;

			AndroidJNI.CallStaticVoidMethod(jniClass, jniFunc, m_jniArgs);
			Debug.Log("jni call method done !");

			AndroidJNI.DetachCurrentThread();
		}

		public void Init(
			int webWidth, int webHeight,
			int tWidth, int tHeight,
			int sWidth, int sHeight,
			string url, int dlOption, string subDir)
		{
#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			if (m_NativePlugin != null)
            {
				m_NativePlugin.Call("initialize", webWidth, webHeight, tWidth, tHeight, sWidth, sHeight, url, dlOption, subDir);
			}
#endif
		}

		public bool IsInitialized()
		{
#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			if (m_NativePlugin != null)
			{
				return m_NativePlugin.Call<bool>("IsInitialized");
			}

			return false;
#else
			return false;
#endif
		}

		private void UpdateSurface()
		{
#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("updateSurface");
#endif
		}

		/// <summary>
		/// This function is obsolete. It returns an array of 1 elements
		/// </summary>
		/// <returns></returns>
		public byte[] GetWebTexturePixel()
		{
			// If textures can be updated with a pointer, this will not be used.

			if (!m_webViewEnable) return new byte[0];

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			// https://www.dbu9.site/post/2023-03-31-androidjnihelper-getsignature-using-byte-parameters-is-obsolete-use-sbyte-parameters-instead/
			//sbyte[] sdata = m_NativePlugin.Call<sbyte[]>("getPixel");
			//byte[] data = new byte[sdata.Length];
			//Buffer.BlockCopy(sdata, 0, data, 0, sdata.Length);
			// https://stackoverflow.com/questions/829983/how-to-convert-a-sbyte-to-byte-in-c
			return (byte[])(Array)m_NativePlugin.Call<sbyte[]>("getPixel");
#else
			return new byte[0];
#endif
		}

		public IntPtr GetTexturePtr()
        {
			if (!m_webViewEnable) return IntPtr.Zero;

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			return (IntPtr)m_NativePlugin.Call<int>("getTexturePtr");
#else
			return IntPtr.Zero;
#endif
		}

		public string GetUrl()
        {
#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			return m_NativePlugin.Call<string>("getCurrentUrl");
#else
			return null;
#endif
		}

		public void CaptureHTMLSource()
        {
			if (!m_webViewEnable) return;

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("capturePage");
#endif
		}

		public void CaptureElementById(string id)
		{
			if (!m_webViewEnable) return;

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("captureElementById", id);
#endif
		}

		public string CurrentHTMLCaptured()
        {
			if (!m_webViewEnable) return null;

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			return m_NativePlugin.Call<string>("getCaptured");
#else
			return null;
#endif
		}

		public void CaptureUserAgent()
        {
			if (!m_webViewEnable) return;

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("captureUserAgent");
#endif
		}

		public string GetUserAgent()
        {
			if (!m_webViewEnable) return "";

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			return m_NativePlugin.Call<string>("getUserAgent");
#else
			return "";
#endif
		}

		public void SetUserAgent(string ua, bool reload)
        {
			if (!m_webViewEnable) return;

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("setUserAgent", ua, reload);
#endif
		}

		public void LoadUrl(string url)
		{
			if (!m_webViewEnable) return;

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("loadUrl", url);
#endif
		}

		public void LoadHTML(string html, string baseURL)
        {
			if (!m_webViewEnable) return;

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("loadHtml", html, baseURL);
#endif
		}

		public void ZoomIn()
		{
			if (!m_webViewEnable) return;

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("zoomIn");
#endif
		}

		public void ZoomOut()
		{
			if (!m_webViewEnable) return;

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("zoomOut");
#endif
		}

		public void EvaluateJS(string js)
        {
			if (!m_webViewEnable) return;

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("evaluateJS", js);
#endif
		}

		public void GoForward()
		{
			if (!m_webViewEnable) return;

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("goForward");
#endif
		}

		public void GoBack()
		{
			if (!m_webViewEnable) return;

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("goBack");
#endif
		}

		public void TouchEvent(int x, int y, int eventNum)
		{
			if (!m_webViewEnable) return;

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("touchEvent", x, y, eventNum);
#endif
		}

		public void KeyEvent(char key)
		{
			if (!m_webViewEnable) return;

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("keyEvent", key);
#endif
		}

		public void BackSpace()
		{
			if (!m_webViewEnable) return;

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("backSpaceKey");
#endif
		}

		public void SetVisible(bool visible)
		{
			WebViewEnabled = visible;

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("setVisible", visible);
#endif
		}

		public void ClearCache(bool includeDiskFiles)
        {
#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("clearCash", includeDiskFiles);
#endif
		}

		public void ClearCookie()
		{
#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("clearCookie");
#endif
		}

		public void ClearHistory()
        {
#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin.Call("clearHistory");
#endif
		}

		private IEnumerable AfterFrame(UnityEvent uEvent, float deley)
        {
			yield return new WaitForSeconds(deley);

			uEvent.Invoke();
        }

		private static void SetJNIParam(int[] args)
        {
			m_jniArgs = new jvalue[args.Length];
			for(int i = 0; i < args.Length; i++)
            {
				m_jniArgs[i] = new jvalue { i = args[i] };
            }
			// https://forum.unity.com/threads/trying-to-implement-jni.147419/
			//m_jniArgs = AndroidJNIHelper.CreateJNIArgArray();
        }

		private IEnumerator StartWebViewTask()
		{
			if (m_NativeClass == null)
			{
				Debug.Log("native class no exists");
				Debug.Log("create native class");
				m_NativeClass = new AndroidJavaClass("com.tlab.libwebview.UnityConnect");
				Debug.Log("create native class done !");
			}

			yield return new WaitForEndOfFrame();

#if UNITY_ANDROID && !UNITY_EDITOR || DEBUG
			m_NativePlugin = new AndroidJavaObject("com.tlab.libwebview.UnityConnect");

            yield return new WaitForEndOfFrame();

            switch (SystemInfo.renderingThreadingMode)
            {
                case UnityEngine.Rendering.RenderingThreadingMode.MultiThreaded:
					SetJNIParam(new int[] { m_texWidth, m_texHeight } );
                    GL.IssuePluginEvent(RenderThreadHandlePtr, 0);
                    break;
                default:
                    m_NativePlugin.CallStatic("checkEGLContextExist");
					m_NativePlugin.CallStatic("generateSharedTexture", m_texWidth, m_texHeight);
                    break;
            }

            yield return new WaitForEndOfFrame();

			m_webViewTexture = new Texture2D(m_texWidth, m_texHeight, TextureFormat.ARGB32, false, false);
			m_webViewTexture.name = "WebImage";

			m_rawImage.texture = m_webViewTexture;

            Init(
                m_webWidth, m_webHeight,
                m_texWidth, m_texHeight,
                Screen.width, Screen.height,
                m_url, (int)m_dlOption, m_subdir);

            yield return new WaitForEndOfFrame();

            while (!IsInitialized()) yield return new WaitForEndOfFrame();

			yield return new WaitForEndOfFrame();

			m_webViewInitialized = true;
            m_webViewEnable = true;

            m_webvieStartTask = null;
#endif
		}

		public void DebugTexturePtr()
		{
			IntPtr pluginNativePtr = m_webViewTexture.GetNativeTexturePtr();
			IntPtr textureNativePtr = m_rawImage.texture.GetNativeTexturePtr();
			Debug.Log("plugin ptr: " + pluginNativePtr + " , " + "texture ptr: " + textureNativePtr);
		}

		public void StartWebView()
		{
			if(m_webvieStartTask == null && !m_webViewInitialized)
            {
				m_webvieStartTask = StartCoroutine(StartWebViewTask());
			}
		}

		public void UpdateFrame()
		{
            if (!m_webViewEnable) return;

#if !ADVANCED
			byte[] data = GetWebTexturePixel();

            if (data.Length > 0)
            {
                m_webViewTexture.LoadRawTextureData(data);
                m_webViewTexture.Apply();
            }
#else
			IntPtr texId = GetTexturePtr();
			if(m_texId != texId)
            {
				m_texId = texId;
				m_webViewTexture.UpdateExternalTexture(texId);
            }
#endif
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
