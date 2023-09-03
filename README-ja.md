# TLabWebViewVR  

UnityのOculus quest VRでWebViewを使用するためのサンプルプロジェクト  
Oculus Integration と XR Interaction Toolkitの両方をサポート.

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/tlabaltoh)

## スクリーンショット  

[Watch on Youtube](https://youtu.be/q3swlSP1mRg)  
![output](Media/tlab-webview-vr.gif)

## 動作環境
ヘッドセット: Oculus Quest 2  
OS: Android 10  
GPU: Qualcomm Adreno650  
Unity: 2021.23f1  

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
任意のディレクトリに以下のコマンドでリポジトリをクローン
```
git clone https://github.com/TLabAltoh/TLabWebViewVR.git
```

### セットアップ
1. Build Settingsからプラットフォームを Androidに変更  
2. Project Settings --> Player --> Other Settings に以下のシンボルを追加(ビルド時に使用)
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
- Minimux API Level: 29 
  
- Assets/Plugins/Android/AndroidManifest.xmlを作成し，以下のテキストをコピーする
```xml
<?xml version="1.0" encoding="utf-8" standalone="no"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" android:installLocation="auto">
  <application android:label="@string/app_name" android:icon="@mipmap/app_icon" android:allowBackup="false">
    <activity android:theme="@android:style/Theme.Black.NoTitleBar.Fullscreen" android:configChanges="locale|fontScale|keyboard|keyboardHidden|mcc|mnc|navigation|orientation|screenLayout|screenSize|smallestScreenSize|touchscreen|uiMode" android:launchMode="singleTask" android:name="com.unity3d.player.UnityPlayerActivity" android:excludeFromRecents="true">
      <intent-filter>
        <action android:name="android.intent.action.MAIN" />
        <category android:name="android.intent.category.LAUNCHER" />
      </intent-filter>
    </activity>
    <meta-data android:name="unityplayer.SkipPermissionsDialog" android:value="false" />
    <meta-data android:name="com.samsung.android.vr.application.mode" android:value="vr_only" />
  </application>
	
    <!-- For Unity-WebView -->
    <application android:allowBackup="true"/>
    <application android:supportsRtl="true"/>
    <application android:hardwareAccelerated="true"/>
    <application android:usesCleartextTraffic="true"/>

    <uses-permission android:name="android.permission.INTERNET" />
    <uses-permission android:name="android.permission.ACCESS_NETWORK_STATE"/>
    <uses-permission android:name="android.permission.CAMERA" />
    <uses-permission android:name="android.permission.MICROPHONE" />
    <uses-permission android:name="android.permission.MODIFY_AUDIO_SETTINGS" />
    <uses-permission android:name="android.permission.RECORD_AUDIO" />

    <uses-feature android:name="android.hardware.camera" />
    <uses-feature android:name="android.hardware.microphone" />
    <!-- For Unity-WebView -->
	
  <uses-feature android:name="android.hardware.vr.headtracking" android:version="1" android:required="true" />
</manifest>
```
#### Oculus Integration
3. ProjectSettings/XRPlugin-Manegement  AndroidSettings --> Plugin-Provider --> Oculus
4. Assets/TLab/TLabWebViewVR/OculusIntegration/Scenes/TLabWebViewVR.unityを開く
5. ヒエラルキーからTLabWebViewVR/TLabWebView/WebView にアタッチされている TLabWebViewのパラメータを任意で変更  
- Url: WebViewの初期化時にロードするURL  
- DlOption: ファイルをアプリケーションフォルダとダウンロードフォルダどちらにダウンロードするか  
- SubDir: アプリケーションフォルダにダウンロードする場合，```{Application folder}/{files}/{SubDir}```にダウンロードされる  
- Web (Width/Height): WebViewの解像度 (デフォルト 1024 * 1024)  
- Tex (Width/Height): Texture2Dの解像度 (デフォルト 512 * 512)  

#### XR Interaction Toolkit
3. ProjectSettings/XRPlugin-Manegement  AndroidSettings --> Plugin-Provider --> OpenXR
4. Assets/TLab/TLabWebViewVR/XRToolkit/Scenes/TLabWebViewVR_XRToolkit.unityを開く
5. ヒエラルキーからTLabWebViewVR_XRToolkit/TLabWebView/WebView にアタッチされている TLabWebViewのパラメータを任意で変更  
- Url: WebViewの初期化時にロードするURL  
- DlOption: ファイルをアプリケーションフォルダとダウンロードフォルダどちらにダウンロードするか  
- SubDir: アプリケーションフォルダにダウンロードする場合，```{Application folder}/{files}/{SubDir}```にダウンロードされる  
- Web (Width/Height): WebViewの解像度 (デフォルト 1024 * 1024)  
- Tex (Width/Height): Texture2Dの解像度 (デフォルト 512 * 512)  

## リンク
[使用したJavaプラグインのソースコード](https://github.com/TLabAltoh/TLabWebViewPlugin)
