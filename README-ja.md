# TLabWebViewVR  

UnityのOculus quest VRでWebViewを使用するためのサンプルプロジェクト．Meta XR SDKとXR Interaction Toolkitそれぞれで実装したサンプルが内包されています．

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/tlabaltoh)

## ドキュメント
[ドキュメントはこちらです](https://tlabgames.gitbook.io/tlabwebview)

## スクリーンショット  

[Watch on Youtube](https://youtu.be/q3swlSP1mRg)  
![output](Media/tlab-webview-vr.gif)

## 動作環境
|                |                     |
| -------------- | ------------------- |
| Headset        | Oculus Quest 2      |
| GPU            | Qualcomm Adreno 650 |
| Unity          | 2021.37f1           |

## スタートガイド
### 必要なもの
- Unity 2021.3.26f1 (meta xr sdk が Unity Editor 2021.26f1以降を必要とします)  
- [meta-xr-all-in-one-sdk](https://assetstore.unity.com/packages/tools/integration/meta-xr-all-in-one-sdk-269657?locale=ja-JP)
- [com.unity.xr.interaction.toolkit](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@3.0/manual/index.html)
- [TLabVKeyborad](https://github.com/TLabAltoh/TLabVKeyborad)
- [TLabWebView](https://github.com/TLabAltoh/TLabWebView)

### インストール
- 任意のディレクトリに以下のコマンドでリポジトリをクローン
```
git clone https://github.com/TLabAltoh/TLabWebViewVR.git
	
cd TLabWebViewVR
	
git submodule update --init
```

### セットアップ
- Build Settings

| Property      | Value   |
| ------------- | ------- |
| Platform      | Android |

- Project Settings

| Property          | Value                                 |
| ----------------- | ------------------------------------- |
| Color Space       | Linear                                |
| Minimum API Level | 29                                    |
| Target API Level  | 30 (Unity 2021), 31 ~ 32 (Unity 2022) |

- Project Settings --> Player --> Other Settings に以下のシンボルを追加(ビルド時に使用)

``` 
UNITYWEBVIEW_ANDROID_USES_CLEARTEXT_TRAFFIC 
```
``` 
UNITYWEBVIEW_ANDROID_ENABLE_CAMERA 
```
``` 
UNITYWEBVIEW_ANDROID_ENABLE_MICROPHONE 
```

- ダウンロードフォルダのような外部ストレージにアクセスしたい場合，android 11以降はこちらのパーミッションをAndroidManifest.xmlに追加してください ([詳細](https://developer.android.com/training/data-storage/manage-all-files?hl=ja))．

```.xml
<uses-permission android:name="android.permission.MANAGE_EXTERNAL_STORAGE" />
```

### サンプルシーン

#### Meta XR SDK
```Assets/TLab/TLabWebViewVR/MetaXR/Samples/Scenes/MetaXR Sample.unity```


#### XR Interaction Toolkit
```Assets/TLab/TLabWebViewVR/XRInteractionToolkit/Samples/Scenes/XRInteractionToolkit Sample.unity```

## サンプルリポジトリ for Unity 2022
- [Oculus Integration Sample](https://github.com/TLabAltoh/TLabWebViewVR-OculusIntegration-2022)
- [XR Interaction Toolkit Sample (VR Template)](https://github.com/TLabAltoh/TLabWebViewVR-XRInteractionToolkit-2022)

## リンク
[スニペット](https://gist.github.com/TLabAltoh/e0512b3367c25d3e1ec28ddbe95da497#file-xr-composition-layers_rendering-md)
[使用したJavaプラグインのソースコード](https://github.com/TLabAltoh/TLabWebViewPlugin)
