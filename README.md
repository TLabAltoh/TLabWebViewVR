# TLabWebViewVR

[日本語版READMEはこちら](README-ja.md)

Sample project for using WebView in Oculus quest VR in Unity  
Support for both Oculus Integration and XR Interaction Toolkit.

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/tlabaltoh)

## Document
[Document is here](https://tlabgames.gitbook.io/tlabwebview)

## Screenshot  
[Watch on Youtube](https://youtu.be/q3swlSP1mRg)  
![output](Media/tlab-webview-vr.gif)

## Note
- The input system for WebView has been significantly changed
	- ``` TLabWebViewVRTouchEventManager.cs ``` --> ``` TLabWebViewVRTouchEventListener.cs ```
	- ``` TLabWebViewXRInputManager.cs ``` --> ``` TLabWebViewXRInputListener.cs ```
- Now officially compatible with Unity 2021 ~ 2022.
- The policy has been changed to manage libraries in the repository as submodules.
	- Commit ``` 4a7a833 ``` If you cloned the project before, please clone the repository again.
	- Use ``` git submodule update --init ``` to adjust the commit of the submodule to the version recommended by the project.
- We have decided to discontinue the ``` TLabWebViewVRTouchEventListener / TLabWebViewXRInputListener ``` and make ``` WebViewInputListener ``` the UI module of TLabWebView from now on. This allows the input module to work independently of plug-ins such as Oculus, XRToolkit, etc. (2024/2/13)

## Operating Environment
- Oculus Quest 2
- Qualcomm Adreno650
- Unity: 2021.23f1

## Getting Started
### Prerequisites
- Unity 2021.3.23f1  
- Oculus Integration
- XR Interaction Toolkit
- TextMeshPro
- ProBuilder
- [TLabVKeyborad](https://github.com/TLabAltoh/TLabVKeyborad)
- [TLabWebView](https://github.com/TLabAltoh/TLabWebView)
- [TLabVRPlayerController](https://github.com/TLabAltoh/TLabVRPlayerController)

### Installing
- Clone the repository with the following command
```
git clone https://github.com/TLabAltoh/TLabWebViewVR.git

cd TLabWebViewVR

git submodule update --init
```

### Set up
- Build Settings  

| Setting items | value |
| --- | --- |  
| platform | android |  

- Project Settings

| Setting items | value |
| --- | --- |  
| Color Space | Linear |  
| Graphics | OpenGLES3 |  
| Minimum API Level | 29 |  
| Target API Level | 30 (Unity 2021), 31 ~ 32 (Unity 2022) |


- Add the following symbols to Project Settings --> Player --> Other Settings (to be used at build time)  


``` 
UNITYWEBVIEW_ANDROID_USES_CLEARTEXT_TRAFFIC 
```
``` 
UNITYWEBVIEW_ANDROID_ENABLE_CAMERA 
```
``` 
UNITYWEBVIEW_ANDROID_ENABLE_MICROPHONE 
```


- XR Plug-in Management

| Setting items | value |
| --- | --- |  
| plugin provider | Oculus (not OpenXR) |  

#### Oculus Integration
- Open Assets/TLab/TLabWebViewVR/OculusIntegration/Scenes/TLabWebViewVR.unity
- Change any parameter of TLabWebView attached to TLabWebViewVR/TLabWebView/WebView from the hierarchy
	- Url: URL to load during WebView initialization  
	- DlOption: Whether to download to the application folder or the downloads folder  
	- SubDir: In case of setting download to application folder, it is downloaded to ```{Application folder}/{files}/{SubDir}```  
	- Web (Width/Height):  Web page resolution (default 1024 * 1024)  
	- Tex (Width/Height): Texture2D resolution used within Unity (default 512 * 512)  

#### XR Interaction Toolkit
- Open Assets/TLab/TLabWebViewVR/XRToolkit/Scenes/TLabWebViewVR_XRToolkit.unity
- Change any parameter of TLabWebView attached to TLabWebViewVR_XRToolkit/TLabWebView/WebView from the hierarchy
	- Url: URL to load during WebView initialization  
	- DlOption: Whether to download to the application folder or the downloads folder  
	- SubDir: In case of setting download to application folder, it is downloaded to ```{Application folder}/{files}/{SubDir}```  
	- Web (Width/Height):  Web page resolution (default 1024 * 1024)  
	- Tex (Width/Height): Texture2D resolution used within Unity (default 512 * 512)  

## Tutorial Videos
### Asset Migration Tutorial (Youtube)
- [Oculus Integration Sample](https://youtu.be/tAY8gM8EgvI)
- [XR Interaction ToolkitSample](https://youtu.be/1OhMEAv6Qok)

### Sample Repository for Unity 2022
- [Oculus Integration Sample](https://github.com/TLabAltoh/TLabWebViewVR-OculusIntegration-2022)
- [XR Interaction Toolkit Sample (VR Template)](https://github.com/TLabAltoh/TLabWebViewVR-XRInteractionToolkit-2022)

## Link
[Source code of the java plugin used](https://github.com/TLabAltoh/TLabWebViewPlugin)
