# TLabWebViewVR  

UnityのOculus quest VRでWebViewを使用するためのサンプルプロジェクト  
Oculus Integration と XR Interaction Toolkitの両方をサポート

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/tlabaltoh)

## スクリーンショット  

[Watch on Youtube](https://youtu.be/q3swlSP1mRg)  
![output](Media/tlab-webview-vr.gif)

## Note
- クラス名を変更しました．
	- ``` TLabWebViewVRTouchEventManager.cs ``` --> ``` TLabWebViewVRTouchEventListener.cs ```
	- ``` TLabWebViewXRInputManager.cs ``` --> ``` TLabWebViewXRInputListener.cs ```
- 現在，Unity 2021 ~ 2022を正式にサポートしています．
- リポジトリ内のライブラリをsubmoduleとして管理する方針に変更しました．
	- コミット ``` 4a7a833 ``` 以前からプロジェクトをクローンしていた方は，改めてリポジトリをクローンし直してください．
	- ``` git submodule update --init ```でサブモジュールのコミットをプロジェクトで推奨するバージョンに合わせてください．
- ``` TLabWebViewVRTouchEventListener ``` / ``` TLabWebViewXRInputLIstener ```を廃止し，``` WebViewInputListener ```を今後TLabWebViewのUIモジュールとすることにしました．これにより入力モジュールは，Oculus, XRToolkitなどのプラグインに依存せず動作するようになります．(2024/2/13)

## 動作環境
- Oculus Quest 2
- Qualcomm Adreno650
- Unity: 2021.23f1

## スタートガイド
### 必要な要件
- Unity 2021.3.23f1  
- Oculus Integration
- XR Interaction Toolkit
- TextMeshPro
- ProBuilder
- [TLabVKeyborad](https://github.com/TLabAltoh/TLabVKeyborad)
- [TLabWebView](https://github.com/TLabAltoh/TLabWebView)
- [TLabVRPlayerController](https://github.com/TLabAltoh/TLabVRPlayerController)

### インストール
- 任意のディレクトリに以下のコマンドでリポジトリをクローン
```
git clone https://github.com/TLabAltoh/TLabWebViewVR.git
	
cd TLabWebViewVR
	
git submodule update --init
```

### セットアップ
- Build Settings  

| Setting items | value |
| --- | --- |  
| platform | android |  

- Project Settings

| Setting items | value |
| --- | --- |  
| Color Space | Linear |  
| Graphics | OpenGLES3 |  
| Minimux API Level | 29 |  
| Target API Level | 30 (Unity 2021), 31 ~ 32 (Unity 2022) |

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

- XR Plug-in Management

| Setting items | value |
| --- | --- |  
| plugin provider | Oculus (not OpenXR) |  

#### Oculus Integration
- Assets/TLab/TLabWebViewVR/OculusIntegration/Scenes/TLabWebViewVR.unityを開く
- ヒエラルキーからTLabWebViewVR/TLabWebView/WebView にアタッチされている TLabWebViewのパラメータを任意で変更  
	- Url: WebViewの初期化時にロードするURL  
	- DlOption: ファイルをアプリケーションフォルダとダウンロードフォルダどちらにダウンロードするか  
	- SubDir: アプリケーションフォルダにダウンロードする場合，```{Application folder}/{files}/{SubDir}```にダウンロードされる  
	- Web (Width/Height): WebViewの解像度 (デフォルト 1024 * 1024)  
	- Tex (Width/Height): Texture2Dの解像度 (デフォルト 512 * 512)  

#### XR Interaction Toolkit
- Assets/TLab/TLabWebViewVR/XRToolkit/Scenes/TLabWebViewVR_XRToolkit.unityを開く
- ヒエラルキーからTLabWebViewVR_XRToolkit/TLabWebView/WebView にアタッチされている TLabWebViewのパラメータを任意で変更  
	- Url: WebViewの初期化時にロードするURL  
	- DlOption: ファイルをアプリケーションフォルダとダウンロードフォルダどちらにダウンロードするか  
	- SubDir: アプリケーションフォルダにダウンロードする場合，```{Application folder}/{files}/{SubDir}```にダウンロードされる  
	- Web (Width/Height): WebViewの解像度 (デフォルト 1024 * 1024)  
	- Tex (Width/Height): Texture2Dの解像度 (デフォルト 512 * 512)  

## チュートリアル
### アセット移行チュートリアル (Youtube)
- [Oculus Integration Sample](https://youtu.be/tAY8gM8EgvI)
- [XR Interaction ToolkitSample](https://youtu.be/1OhMEAv6Qok)

### サンプルリポジトリ for Unity 2022
- [Oculus Integration Sample](https://github.com/TLabAltoh/TLabWebViewVR-OculusIntegration-2022)
- [XR Interaction Toolkit Sample (VR Template)](https://github.com/TLabAltoh/TLabWebViewVR-XRInteractionToolkit-2022)

## リンク
[使用したJavaプラグインのソースコード](https://github.com/TLabAltoh/TLabWebViewPlugin)
