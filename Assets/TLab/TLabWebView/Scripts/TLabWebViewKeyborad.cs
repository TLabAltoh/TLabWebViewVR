using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TLab.InputField;

namespace TLab.Android.WebView
{
    public class TLabWebViewKeyborad : MonoBehaviour
    {
        [Header("Audio")]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip keyStroke;
        [SerializeField] private AudioClip lockButtonPress;

        [Header("KeyBOX")]
        [SerializeField] private GameObject keyBOX;
        [SerializeField] private GameObject romajiBOX;
        [SerializeField] private GameObject symbolBOX;
        [SerializeField] private GameObject operatorBOX;

        [Header("WebView")]
        [SerializeField] private TLabWebView webview;

        private List<string> inputBuffer = new List<string>();

        private TLabKey[] Keys(GameObject target)
        {
            return target.GetComponentsInChildren<TLabKey>();
        }

#if UNITY_EDITOR
        public void InitializeTLabKey()
        {
            foreach (TLabKey key in Keys(keyBOX))
            {
                key.Initialize();
                EditorUtility.SetDirty(key);
            }
        }
#endif

        private void KeyStrokeAudio()
        {
            audioSource.PlayOneShot(keyStroke);
        }

        public void OnBackSpacePressed()
        {
            webview.BackSpace();
        }

        public void OnEnterPressed()
        {
            AddKey("\n");
        }

        private void OnShiftPressed()
        {
            foreach (TLabKey key in Keys(keyBOX)) key.ShiftPressed();
        }

        private void OnSpacePressed()
        {
            AddKey(" ");
        }

        private void OnTabPressed()
        {
            AddKey("\t");
        }

        private void OnSymbolPressed()
        {
            bool active = romajiBOX.activeSelf;

            romajiBOX.SetActive(!active);
            symbolBOX.SetActive(active);
        }

        public void OnInput(string input)
        {
            KeyStrokeAudio();

            switch (input)
            {
                case "BACKSPACE":
                    OnBackSpacePressed();
                    return;
                case "ENTER":
                    OnEnterPressed();
                    return;
                case "SHIFT":
                    OnShiftPressed();
                    return;
                case "SPACE":
                    OnSpacePressed();
                    return;
                case "TAB":
                    OnTabPressed();
                    return;
                case "SYMBOL":
                    OnSymbolPressed();
                    return;
            }

            AddKey(input);
        }

        public void AddKey(string key)
        {
            KeyStrokeAudio();

            webview.KeyEvent(key.ToCharArray()[0]);
        }

        private void InitalizeVirtualKeyborad()
        {
            bool romajiBOXActive = romajiBOX.activeSelf;
            bool symbolBOXActive = symbolBOX.activeSelf;
            bool operatorBoxActive = operatorBOX.activeSelf;
            romajiBOX.SetActive(true);
            symbolBOX.SetActive(true);
            operatorBOX.SetActive(true);
            foreach (TLabKey key in Keys(keyBOX)) key.SetKeyInputBuffer(inputBuffer);
            romajiBOX.SetActive(romajiBOXActive);
            symbolBOX.SetActive(symbolBOXActive);
            operatorBOX.SetActive(operatorBoxActive);
        }

        private void UpdateKeyboradInMobile()
        {
            foreach (string input in inputBuffer) OnInput(input);

            inputBuffer.Clear();
        }

        private void Start()
        {
            InitalizeVirtualKeyborad();
        }

        private void Update()
        {
            UpdateKeyboradInMobile();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TLabWebViewKeyborad))]
    [CanEditMultipleObjects]

    public class TLabWebViewKeyboradEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            TLabWebViewKeyborad inputField = target as TLabWebViewKeyborad;
            if (GUILayout.Button("Initialize"))
            {
                inputField.InitializeTLabKey();
                EditorUtility.SetDirty(inputField);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}