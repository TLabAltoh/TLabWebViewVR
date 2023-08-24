using System.Collections.Generic;
using UnityEngine;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TLab.InputField
{
    [RequireComponent(typeof(AudioSource))]
    public class TLabInputField : MonoBehaviour
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

        [Header("Text(TMPro)")]
        [SerializeField] private TextMeshProUGUI inputText;
        [SerializeField] private TextMeshProUGUI placeholder;

        [Header("Image")]
        [SerializeField] private GameObject openImage;
        [SerializeField] private GameObject lockImage;

        [Header("Button")]
        [SerializeField] private GameObject inputFieldButton;

        [Header("HideObject")]
        [SerializeField] private GameObject[] hideObjects;

        [Header("IsThisMobile")]
        [SerializeField] private bool isMobile = false;

        [System.NonSerialized] public string text = "";

        private float backSpaceKeyTime = 0.0f;
        private bool inputFieldFocused = true;
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

        private void LockButtonPressAudio()
        {
            audioSource.PlayOneShot(lockButtonPress, 0.5f);
        }

        private void KeyStrokeAudio()
        {
            audioSource.PlayOneShot(keyStroke);
        }

        private void SwitchKeyborad(bool isActive)
        {
            keyBOX.SetActive(isActive && isMobile);
            inputFieldFocused = isActive;
            openImage.SetActive(isActive);
            lockImage.SetActive(!isActive);
            inputFieldButton.SetActive(!isActive);

            if (isMobile == true)
                foreach (GameObject hideObject in hideObjects) hideObject.SetActive(!isActive);
        }

        public void OnFocus(bool active)
        {
            SwitchKeyborad(active);
            LockButtonPressAudio();
        }

        public void SetPlaceHolder(string text)
        {
            this.text = "";
            placeholder.text = text;
            Display();
        }

        public void OnBackSpacePressed()
        {
            if (text != "")
            {
                text = text.Remove(text.Length - 1);
                Display();
            }
        }

        public void OnEnterPressed() { }

        private void OnShiftPressed()
        {
            foreach (TLabKey key in Keys(keyBOX)) key.ShiftPressed();
        }

        private void OnSpacePressed()
        {
            AddKey(" ");
        }

        private void OnSymbolPressed()
        {
            bool active = romajiBOX.activeSelf;

            romajiBOX.SetActive(!active);
            symbolBOX.SetActive(active);
        }

        private void OnTabPressed()
        {
            AddKey("    ");
        }

        public void OnInput(string input)
        {
            if (!inputFieldFocused) return;

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

        private void SwitchPlaseholder()
        {
            if (inputText.text == "")
                placeholder.color = new Color(0.196f, 0.196f, 0.196f, 0.5f);
            else
                placeholder.color = new Color(0.196f, 0.196f, 0.196f, 0.0f);
        }

        public void Display()
        {
            inputFieldFocused = true;
            inputText.text = text;

            SwitchPlaseholder();
        }

        public void AddKey(string key)
        {
            KeyStrokeAudio();
            text += key;
            Display();
        }

#if !UNITY_EDITOR && UNITY_WEBGL
    [System.Runtime.InteropServices.DllImport("__Internal")]
    private static extern bool IsMobile();
#endif

        private bool CheckIfMobile()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
        isMobile = IsMobile();
#endif

#if UNITY_ANDROID
        isMobile = true;
#endif

            return isMobile;
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

        private void UpdateKeyboradInPC()
        {
            backSpaceKeyTime += Time.deltaTime;

            if (Input.anyKey == true)
            {
                string inputString = Input.inputString;

                if (Input.GetKeyDown(KeyCode.Return)) OnEnterPressed();
                else if (Input.GetKeyDown(KeyCode.Tab)) OnTabPressed();
                else if (Input.GetKeyDown(KeyCode.Space)) OnSpacePressed();
                else if (Input.GetKey(KeyCode.Backspace) && backSpaceKeyTime > 0.1f)
                {
                    OnBackSpacePressed();
                    backSpaceKeyTime = 0.0f;
                }
                else if (inputString != "" && inputString != "") AddKey(inputString);
                else if (Input.GetMouseButtonDown(1)) AddKey(GUIUtility.systemCopyBuffer);
            }
        }

        private void Start()
        {
            if (CheckIfMobile())
                InitalizeVirtualKeyborad();

            SwitchKeyborad(false);

            this.text = inputText.text;

            SwitchPlaseholder();
        }

        private void Update()
        {
            if (isMobile)
                UpdateKeyboradInMobile();
            else
                UpdateKeyboradInPC();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(TLabInputField))]
    [CanEditMultipleObjects]

    public class TLabInputFieldEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            TLabInputField inputField = target as TLabInputField;
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
