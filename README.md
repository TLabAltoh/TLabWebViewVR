# TLabWebViewVR

[日本語版READMEはこちら](README-ja.md)

Sample project for using WebView in Oculus quest VR in Unity. Includes Meta XR SDK and XR Interaction Toolkit implementation example.

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/tlabaltoh)

## Document
[Document is here](https://tlabgames.gitbook.io/tlabwebview)

## Screenshot  
[Watch on Youtube](https://youtu.be/q3swlSP1mRg)  
![output](Media/tlab-webview-vr.gif)

## Note
<details><summary>please see here</summary>

### Oculus SDK Updated to Meta XR SDK
The Oculus SDK has now been updated for Oculus integration SDK to Meta XR All in One SDK, this sdk requires Unity Editor 2021.26f1 ~. Oculus SDK versions after 57 (Meta XR SDK) are managed by Unity Package Manager (UPM), but have near-compatibility between Oculus Integration and the Meta XR SDK. However, in the sample scene in this repository, the Meta XR SDK sample has been updated to no longer use the OVR Input Module and switched to Pointable Canvas Module based, because the UI implementation sample including the Oculus SDK is based on the Pointable Canvas Module and it's inappropriate to implement webview with the OVR Input Module as before. (2021/4/14)

### Module Management Policy Modified
The policy has been changed to manage libraries in the repository as submodules after commit ``` 4a7a833 ```. Please run ``` git submodule update --init ``` to adjust the commit of the submodule to the version recommended by the project.

### WebView Input System Updated
I have decided to discontinue the ``` TLabWebViewVRTouchEventListener / TLabWebViewXRInputListener ``` and make ``` WebViewInputListener ``` the UI module of TLabWebView from now on. This allows the input module to work independently of plug-ins such as Oculus, XRToolkit, etc. (2024/2/13)

</details>

## Operating Environment
|                |                     |
| -------------- | ------------------- |
| Headset        | Oculus Quest 2      |
| GPU            | Qualcomm Adreno 650 |
| Unity          | 2021.37f1           |

## Getting Started
### Prerequisites
- Unity 2021.26f1 (meta xr sdk requires Unity Editor 2021.26f1 ~)
- [meta-xr-all-in-one-sdk](https://assetstore.unity.com/packages/tools/integration/meta-xr-all-in-one-sdk-269657?locale=ja-JP)
- [com.unity.xr.interaction.toolkit](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@3.0/manual/index.html)
- [TLabVKeyborad](https://github.com/TLabAltoh/TLabVKeyborad)
- [TLabWebView](https://github.com/TLabAltoh/TLabWebView)

### Installing
- Clone the repository with the following command
```
git clone https://github.com/TLabAltoh/TLabWebViewVR.git

cd TLabWebViewVR

git submodule update --init
```

### Set Up
- Build Settings

| Property      | Value   |
| ------------- | ------- |
| Platform      | Android |

- Project Settings

| Property          | Value                                 |
| ----------------- | ------------------------------------- |
| Color Space       | Linear                                |
| Graphics          | OpenGLES3                             |
| Minimum API Level | 29                                    |
| Target API Level  | 30 (Unity 2021), 31 ~ 32 (Unity 2022) |


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

| Property        | Value               |
| --------------- | ------------------- |
| Plugin Provider | Oculus (not OpenXR) |

#### Oculus Integration
Open ```Assets/TLab/TLabWebViewVR/MetaXR/Scenes/MetaXR Sample.unity```


#### XR Interaction Toolkit
Open ```Assets/TLab/TLabWebViewVR/XRInteractionToolkit/Scenes/XRInteractionToolkit Sample.unity```


## Tutorial Videos
### Asset Migration Tutorial (Youtube)
- [Oculus Integration Sample](https://youtu.be/tAY8gM8EgvI)
- [XR Interaction ToolkitSample](https://youtu.be/1OhMEAv6Qok)

### Sample Repository for Unity 2022
- [Oculus Integration Sample](https://github.com/TLabAltoh/TLabWebViewVR-OculusIntegration-2022)
- [XR Interaction Toolkit Sample (VR Template)](https://github.com/TLabAltoh/TLabWebViewVR-XRInteractionToolkit-2022)

## Link
[Source code of the java plugin used](https://github.com/TLabAltoh/TLabWebViewPlugin)
