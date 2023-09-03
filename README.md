# TLabWebViewVR

[日本語版READMEはこちら](README-ja.md)

Sample project for using WebView in Oculus quest VR in Unity  
Support for both Oculus Integration and XR Interaction Toolkit.

[!["Buy Me A Coffee"](https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png)](https://www.buymeacoffee.com/tlabaltoh)

## Screenshot  
[Watch on Youtube](https://youtu.be/q3swlSP1mRg)  
![output](Media/tlab-webview-vr.gif)

## Operating Environment
Headset: Oculus Quest 2  
OS: Android 10  
GPU: Qualcomm Adreno650  
Unity: 2021.23f1  

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
Clone the repository with the following command.
```
git clone https://github.com/TLabAltoh/TLabWebViewVR.git
```

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
- Minimux API Level: 29 
  
- Create Assets/Plugins/Android/AndroidManifest.xml and copy the following text
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
4. Open Assets/TLab/TLabWebViewVR/OculusIntegration/Scenes/TLabWebViewVR.unity
5. Change any parameter of TLabWebView attached to TLabWebViewVR/TLabWebView/WebView from the hierarchy
- Url: URL to load during WebView initialization  
- DlOption: Whether to download to the application folder or the downloads folder  
- SubDir: In case of setting download to application folder, it is downloaded to ```{Application folder}/{files}/{SubDir}```  
- Web (Width/Height):  Web page resolution (default 1024 * 1024)  
- Tex (Width/Height): Texture2D resolution used within Unity (default 512 * 512)  

#### XR Interaction Toolkit
3. ProjectSettings/XRPlugin-Manegement  AndroidSettings --> Plugin-Provider --> OpenXR
4. Open Assets/TLab/TLabWebViewVR/XRToolkit/Scenes/TLabWebViewVR_XRToolkit.unity
5. Change any parameter of TLabWebView attached to TLabWebViewVR_XRToolkit/TLabWebView/WebView from the hierarchy
- Url: URL to load during WebView initialization  
- DlOption: Whether to download to the application folder or the downloads folder  
- SubDir: In case of setting download to application folder, it is downloaded to ```{Application folder}/{files}/{SubDir}```  
- Web (Width/Height):  Web page resolution (default 1024 * 1024)  
- Tex (Width/Height): Texture2D resolution used within Unity (default 512 * 512)  

## Link
[Source code of the java plugin used](https://github.com/TLabAltoh/TLabWebViewPlugin)
