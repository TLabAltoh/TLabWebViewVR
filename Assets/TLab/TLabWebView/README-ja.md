# TLabWebView  

Unityで動作するWebViewのプラグイン．WebViewの結果をTexture2Dとして表示できます  
ハードウェアアクセラレーションによる描画も取得可能  
キーボード入力をサポート  

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/tlabaltoh)

## スクリーンショット  
Android13, Adreno 619で実行した画面  


<img src="https://github.com/TLabAltoh/TLabWebView/assets/121733943/b5428eef-7e21-452b-a348-d2064e712156" width="256">


## 動作環境
OS: Android 10 ~ 13  
GPU: Qualcomm Adreno 505, 619  
Unity: 2021.23f1  

## スタートガイド
### 必要な要件
- Unity 2021.3.23f1  
- [TLabVKeyborad](https://github.com/TLabAltoh/TLabVKeyborad)
### インストール
UnityProjectのAssets配下で任意のディレクトリに以下のコマンドからリポジトリをクローン
```
git clone https://github.com/TLabAltoh/TLabWebView.git
```
UnityProjectが既にgitの管理下の場合は以下のコマンドでクローン
```
git submodule add https://github.com/TLabAltoh/TLabWebView.git
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
- Minimux API Level: 23 
  
- Assets/Plugins/Android/AndroidManifest.xmlを作成し，以下のテキストをコピーする
```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest
    xmlns:android="http://schemas.android.com/apk/res/android"
    package="com.unity3d.player"
    xmlns:tools="http://schemas.android.com/tools">
    <application>
        <activity android:name="com.unity3d.player.UnityPlayerActivity"
                  android:theme="@style/UnityThemeSelector">
            <intent-filter>
                <action android:name="android.intent.action.MAIN" />
                <category android:name="android.intent.category.LAUNCHER" />
            </intent-filter>
            <meta-data android:name="unityplayer.UnityActivity" android:value="true" />
        </activity>
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
</manifest>
```
3. Assets/Scenes/main.unity を開く
4. ヒエラルキーからTLabWebView/WebView にアタッチされている TLabWebViewのパラメータを任意で変更  
- Url: WebViewの初期化時にロードするURL
- Texture2D デフォルトサイズ: 512 * 512
- WebView デフォルトサイズ: 1024 * 1024

### プレハブから使用
TLabWebView/TLabWebView.prefabをシーンに追加するだけで，ビルド後WebViewを実行することができます(入力の制御がモバイル向きに構成されていることに注意してください TouchEventManager.csで制御)

## お知らせ
- VRでのプレイに対応しました([link](https://github.com/TLabAltoh/TLabWebViewVR))

## リンク
[使用したJavaプラグインのソースコード](https://github.com/TLabAltoh/TLabWebViewPlugin)
