# TLabWebViewVR

[日本語版READMEはこちら](README-ja.md)

Sample Unity project for using [```TLabWebView```](https://github.com/TLabAltoh/TLabWebView) (3D web browser / 3D WebView plugin) in OculusQuest. Includes [```Meta XR SDK```](https://developers.meta.com/horizon/downloads/package/meta-xr-sdk-all-in-one-upm) and [```XR Interaction Toolkit```](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@3.0/manual/index.html) implementation example.

[Document is here](https://tlabgames.gitbook.io/tlabwebview)  
[Snippets is here](https://gist.github.com/TLabAltoh/e0512b3367c25d3e1ec28ddbe95da497#file-tlabwebview-snippets-md)  

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/tlabaltoh)

> [!WARNING]
> Note that this project only works on Android devices; it will not display web pages when run on the Unity Editor.

> [!WARNING]
> Meta XR All-in-One SDK is having a reference error at version 74 (Please see [this forum](https://www.reddit.com/r/oculusdev/comments/1jn4l5k/error_implementing_meta_allinone_sdk/)), so I will keep version 72 for now.

> [!WARNING]
> I do not recommend building this project on Unity 6000.x when deploying to Oculus Quest (or Meta Quest). Right now, I have found that `HardwareBuffer` mode is unstable when this project is built with Unity 6000.x ([See this issue for details](https://github.com/TLabAltoh/TLabWebViewVR/issues/39)). If you do, please refer to the "Configuration for Unity 6000.x" section in the [Setup section](https://github.com/TLabAltoh/TLabWebViewVR?tab=readme-ov-file#set-up).

> [!WARNING]
> This project uses `HardwareBuffer` as the default `CaptureMode` for WebView and `Vulkan` is used as the default `Graphics API`. This combination of `HardwareBuffer` option and `Vulkan` API is confirmed to work with Oculus Quest, but may not work with some other devices (e.g., the WebView screen may go blank). In that case, Please change the project's `Graphics API` from `Vulkan`, which is set by default, to `OpenGLES`. Or change the `CaptureMode` from `HardwareBuffer` to `ByteBuffer`.
>
> <details>
> <img src="Media/image.1.png" width="256"></img><br>
> <img src="Media/image.2.png" width="256"></img>
> </details>

## Screenshot  
[Watch on Youtube](https://youtu.be/q3swlSP1mRg)  
![output](Media/tlab-webview-vr.gif)

## Operating Environment
|         |                     |
| ------- | ------------------- |
| Headset | Oculus Quest 2      |
| GPU     | Qualcomm Adreno 650 |
| Unity   | 2021.37f1           |

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
Please see the setup section [here](https://github.com/TLabAltoh/TLabWebView?tab=readme-ov-file#set-up)

<details><summary>When using the OpenXR plugin</summary>

Please disable `Force Remove Internet Permission` from XR Plugin Manegement

<img src="Media/image.0.png" width="512"></img>

</details>

<details><summary>Configuration for Unity 6000.x</summary>

When opening this project in Unity 6000.x, a compile error occurs in the TMPro resource. To resolve this, re-import the TMPro resources.

<img src="Media/image.3.png" width="256"></img>  

Also, please change the capture mode from the default `HardwareBuffer` to `ByteBuffer`.

<img src="Media/image.1.png" width="256"></img>  
<img src="Media/image.2.png" width="256"></img>

</details>

### Sample Scene

#### Meta XR SDK
```Assets/TLab/TLabWebViewVR/MetaXR/Samples/Scenes/MetaXR Sample.unity```

#### XR Interaction Toolkit
```Assets/TLab/TLabWebViewVR/XRInteractionToolkit/Samples/Scenes/XRInteractionToolkit Sample.unity```

## Sample Repository for Unity 2022
- [Oculus Integration Sample](https://github.com/TLabAltoh/TLabWebViewVR-OculusIntegration-2022)
- [XR Interaction Toolkit Sample (VR Template)](https://github.com/TLabAltoh/TLabWebViewVR-XRInteractionToolkit-2022)
