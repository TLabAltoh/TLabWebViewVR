#define FUNCTIONAL_VERIFICATION
#undef FUNCTIONAL_VERIFICATION
using UnityEngine.XR.Interaction.Toolkit.Samples.SpatialKeyboard;
using UnityEngine;
using TMPro;

#if FUNCTIONAL_VERIFICATION
using UnityEngine.Events;
using System;
using System.Reflection;
using System.Collections.Generic;
#endif

namespace TLab.WebView.XRInteractionToolkit
{
    public class XRBrowserInputField : MonoBehaviour
    {
        [SerializeField] private BrowserContainer m_container;

        [SerializeField] private TMP_InputField m_inputField;

        [SerializeField] private XRKeyboard m_keyborad;

        private bool m_gainedFocus = false;
        private bool m_shifted = false;

        private void OnEnable()
        {
            foreach (var target in FindObjectsOfType<TMP_InputField>(includeInactive: true))
                target.onSelect.AddListener(
                    (target == m_inputField) ? OnInputFieldGainedFocus : OnInputFieldLostFocus);
        }

        private void OnDisable()
        {
            foreach (var target in FindObjectsOfType<TMP_InputField>(includeInactive: true))
                target.onSelect.RemoveListener(
                    (target == m_inputField) ? OnInputFieldGainedFocus : OnInputFieldLostFocus);

            m_gainedFocus = false;
        }

        public void AddKey(string key) => m_container.browser?.KeyEvent(key.ToCharArray()[0]);

        public void OnInputFieldGainedFocus(string text)
        {
            m_gainedFocus = true;
            m_shifted = m_keyborad.shifted;
        }

        public void OnInputFieldLostFocus(string text)
        {
            m_gainedFocus = false;
        }

        public void OnKeyPressed(KeyboardKeyEventArgs args)
        {
            Debug.Log($"[{nameof(OnKeyPressed)}] Start.");

            var key = args.key;
            if (key == null || !m_gainedFocus)
                return;

            var keyPress = m_shifted && !string.IsNullOrEmpty(key.shiftCharacter) ? key.shiftCharacter : key.character;
            switch (keyPress)
            {
                case "\\s":
                    // Shift
                    break;
                case "\\caps":
                    break;
                case "\\b":
                    // Backspace
                    m_container.browser?.KeyEvent(67);
                    break;
                case "\\c":
                    // cancel
                    break;
                case "\\r":
                    AddKey("\n");
                    break;
                case "\\cl":
                    // Clear
                    break;
                case "\\h":
                    // Hide
                    break;
                default:
                    Debug.Log($"[{nameof(OnKeyPressed)}] keyPress:{keyPress}");
                    AddKey(keyPress);
                    break;
            }

            m_shifted = m_keyborad.shifted;
        }

#if FUNCTIONAL_VERIFICATION
        public class ClassA
        {
            public void Function()
            {

            }
        }

        public static int GetRuntimeEventCount(UnityEventBase unityEvent)
        {
            Type unityEventType = typeof(UnityEventBase);

            Assembly libAssembly = Assembly.GetAssembly(unityEventType);

            Type invokableCallListType = libAssembly.GetType("UnityEngine.Events.InvokableCallList");
            Type baseInvokableCallType = libAssembly.GetType("UnityEngine.Events.BaseInvokableCall");
            Type listType = typeof(List<>);
            Type baseInvokableCallListType = listType.MakeGenericType(baseInvokableCallType);

            FieldInfo callsField = unityEventType.GetField("m_Calls", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo runtimeCallsField = invokableCallListType.GetField("m_RuntimeCalls", BindingFlags.Instance | BindingFlags.NonPublic);

            var calls = callsField.GetValue(unityEvent);
            var runtimeCalls = runtimeCallsField.GetValue(calls);
            PropertyInfo countProperty = baseInvokableCallListType.GetProperty("Count");

            return (int)countProperty.GetValue(runtimeCalls, null);
        }
#endif

        private void Start()
        {
#if FUNCTIONAL_VERIFICATION
            var instance0 = new ClassA();
            var instance1 = new ClassA();

            UnityAction action0 = instance0.Function;
            UnityAction action1 = instance1.Function;
            Debug.Log(action0 == action1); // False

            UnityEvent @event = new UnityEvent();
            @event.AddListener(action0);
            @event.AddListener(action1);
            Debug.Log($"GetRuntimeEventCount(@event) [Before]: {GetRuntimeEventCount(@event)}"); // 2

            @event.RemoveListener(action0);
            Debug.Log($"GetRuntimeEventCount(@event) [After]: {GetRuntimeEventCount(@event)}"); // 1
#endif
        }
    }
}