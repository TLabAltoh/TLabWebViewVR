# TLabWebViewVR  

UnityのOculus quest VRでWebViewを使用するためのサンプルプロジェクト 

## スクリーンショット  

![output](https://user-images.githubusercontent.com/121733943/236464782-8fc7518c-5bde-4778-935c-1bf8850b7c9d.gif)

## 動作環境
ヘッドセット: Oculus Quest 2
OS: Android 10  
GPU: Qualcomm Adreno650  
Unity: 2021.23f1  

## スタートガイド
### 必要な要件
- Unity 2021.3.23f1 
- Oculus Integration
- [TLabVKeyborad](https://github.com/TLabAltoh/TLabVKeyborad)
- [TLabWebView](https://github.com/TLabAltoh/TLabWebView)
- TextMeshPro
- ProBuilder
### インストール
任意のディレクトリに以下のコマンドでリポジトリをクローン
```
git clone https://github.com/TLabAltoh/TLabWebView.git
```
クローンしたプロジェクトの中で以下のコマンドを実行(必要なSubmoduleのインストール)
```
git submodule init
git submodule update
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
3. Assets/TLab/TLabWebViewVR/Scenes/TLabWebViewVR.unityを開く
4. ヒエラルキーからTLabWebViewVR/TLabWebView/WebView にアタッチされている TLabWebViewのパラメータを任意で変更  
- Url: WebViewの初期化時にロードするURL
- WebWidth, WebHeight: Webページのサイズ
- TexWidth, TexHeight: Texture2Dのサイズ

## リンク
[使用したJavaプラグインのソースコード](https://github.com/TLabAltoh/TLabWebViewPlugin)
