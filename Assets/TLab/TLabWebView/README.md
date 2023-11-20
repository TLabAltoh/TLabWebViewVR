# TLabWebView

[日本語版READMEはこちら](README-ja.md)

Plug-in for WebView that runs in Unity and can display WebView results as Texture2D  
- Hardware-accelerated rendering is also available  
- Key input support
- File Download Support
- Supports javascript execution  

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/tlabaltoh)

## Note
- Now officially compatible with Unity 2021 ~ 2022.

## Screenshot  
Screenshot run on Android 13, Adreno 619  

<img src="Media/tlab-webview.png" width="256">

## Operating Environment
OS: Android 10 ~ 13  
GPU: Qualcomm Adreno 505, 619  
Unity: 2021.23f1  

## Getting Started
### Prerequisites
- Unity 2021.3.23f1  
- [TLabVKeyborad](https://github.com/TLabAltoh/TLabVKeyborad)
### Installing
Clone the repository or download it from the release and place it in the Asset folder of Unity
### Set up
1. Change platform to Android from Build Settings  
2. Add the following symbols to Project Settings --> Player --> Other Settings (to be used at build time)  
```
UNITYWEBVIEW_ANDROID_USES_CLEARTEXT_TRAFFIC
```
```
UNITYWEBVIEW_ANDROID_ENABLE_CAMERA
```
```
UNITYWEBVIEW_ANDROID_ENABLE_MICROPHONE
```
- Color Space: Linear
- Graphics: OpenGLES3
- Minimux API Level: 26 
- Target API Level: 30 (Unity 2021), 31 ~ 32 (Unity 2022)

3. Add TLabWebView/TLabWebView.prefab to scene
4. Change the setting of WebView
Setting items in TLabWebView.cs (located in TLabWebView.prefab/WebView)  

<img src="Media/tlab-webview-settings.png" width="256">  

- Url: URL to load during WebView initialization  
- DlOption: Whether to download to the application folder or the downloads folder  
- SubDir: In case of setting download to application folder, it is downloaded to ```{Application folder}/{files}/{SubDir}```  
- Web (Width/Height):  Web page resolution (default 1024 * 1024)  
- Tex (Width/Height): Texture2D resolution used within Unity (default 512 * 512)  

## Scripting API
### Initialize
- public void Init(int webWidth, int webHeight, int tWidth, int tHeight, int sWidth, int sHeight, string url, int dlOption, string subDir)
- public bool IsInitialized()
- public void StartWebView()
### Update Frame
- public byte[] GetWebTexturePixel() <span style="color: red; ">(obsolete)</span>
- public IntPtr GetTexturePtr()
- public void UpdateFrame()
### Capture Element
- public void CaptureHTMLSource()
- public void CaptureElementById(string id)
- public string CurrentHTMLCaptured()
### Load URL
- public void LoadUrl(string url)
- public void LoadHTML(string html, string baseURL)
- public void GoForward()
- public void GoBack()
### Zoom In/Out
- public void ZoomIn()
- public void ZoomOut()
### User Agent
- public void CaptureUserAgent()
- public string GetUserAgent()
- public void SetUserAgent(string ua, bool reload)
### Evaluate Javascript
- public void EvaluateJS(string js)
### Touch Event
- public void TouchEvent(int x, int y, int eventNum)
### Key Event
- public void KeyEvent(char key)
- public void BackSpace()
### Clear Cache
- public void ClearCache(bool includeDiskFiles)
- public void ClearCookie()
- public void ClearHistory()

## NOTICE
- Now supports play in VR ([link](https://github.com/TLabAltoh/TLabWebViewVR)).

## Link
[Source code of the java plugin used](https://github.com/TLabAltoh/TLabWebViewPlugin)
