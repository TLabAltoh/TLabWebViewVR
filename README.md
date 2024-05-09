# TLabWebViewVR

[日本語版READMEはこちら](README-ja.md)

Sample project for using WebView in Oculus quest VR in Unity. Includes Meta XR SDK and XR Interaction Toolkit implementation example.

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/tlabaltoh)

## Document
[Document is here](https://tlabgames.gitbook.io/tlabwebview)

## Screenshot  
[Watch on Youtube](https://youtu.be/q3swlSP1mRg)  
![output](Media/tlab-webview-vr.gif)

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

- If you want to access files that are in external storage (like download, picture). you need to add follow manifest in Android 11 ([detail](https://developer.android.com/training/data-storage/manage-all-files?hl=en)).

```.xml
<uses-permission android:name="android.permission.MANAGE_EXTERNAL_STORAGE" />
```

### Sample Scene

#### Meta XR SDK
```Assets/TLab/TLabWebViewVR/MetaXR/Scenes/MetaXR Sample.unity```


#### XR Interaction Toolkit
```Assets/TLab/TLabWebViewVR/XRInteractionToolkit/Scenes/XRInteractionToolkit Sample.unity```


## Sample Repository for Unity 2022
- [Oculus Integration Sample](https://github.com/TLabAltoh/TLabWebViewVR-OculusIntegration-2022)
- [XR Interaction Toolkit Sample (VR Template)](https://github.com/TLabAltoh/TLabWebViewVR-XRInteractionToolkit-2022)

## Link
[Source code of the java plugin used](https://github.com/TLabAltoh/TLabWebViewPlugin)
