using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TLab.Android.WebView;

namespace TLab.XR.Oculus
{
    public class TLabWebViewXRInputListener : MonoBehaviour
    {
        // https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/api/UnityEngine.XR.Interaction.Toolkit.InputHelpers.html

        [Header("Target WebView")]
        [SerializeField] private TLabWebView m_tlabWebView;

        [Header("XR Input Settings")]
        [SerializeField] private XRRayInteractor m_rayInteractor;
        [SerializeField] private RectTransform m_webViewRect;
        [SerializeField] private InputActionReference m_triggerPress;
        //[SerializeField] private XRNode m_controllerNode = XRNode.RightHand;
        //[SerializeField] private InputHelpers.Button m_touchButton = InputHelpers.Button.TriggerButton;

        [Header("XR Interact Line Visual")]
        [SerializeField] private XRInteractorLineVisual m_lineVisual;
        [SerializeField] private LineRenderer m_lineRenderer;

        //private InputDevice m_controller;

        private int m_lastXPos;
        private int m_lastYPos;
        private bool m_onTheWeb = false;

        private bool m_lastPressed = false;

        private const int TOUCH_DOWN = 0;
        private const int TOUCH_UP = 1;
        private const int TOUCH_MOVE = 2;

        private const float m_rectZThreshold = 0.05f;

        private void OnEnable()
        {
            Application.onBeforeRender += UpdateWebViewLineVisual;
            m_triggerPress.action.Enable();
        }

        private void OnDisable()
        {
            Application.onBeforeRender -= UpdateWebViewLineVisual;
            m_triggerPress.action.Disable();
        }

        private int GetButtonEvent()
        {
            int eventNum = (int)UnityEngine.TouchPhase.Stationary;

            bool pressed = m_triggerPress.action.IsPressed();
            //m_controller.TryGetFeatureValue(CommonUsages.triggerButton, out pressed);

            if (m_lastPressed == true)
            {
                if (pressed == false)
                    eventNum = TOUCH_UP;
                else
                    eventNum = TOUCH_MOVE;
            }
            else
            {
                if (pressed == true)
                    eventNum = TOUCH_DOWN;
            }

            m_lastPressed = pressed;

            return eventNum;
        }

        [BeforeRenderOrder(500)]
        private void UpdateWebViewLineVisual()
        {
            if (m_onTheWeb == true)
                m_lineRenderer.colorGradient = m_lineVisual.validColorGradient;
        }

        void TouchRelease()
        {
            if (m_onTheWeb)
                m_tlabWebView.TouchEvent(m_lastXPos, m_lastYPos, TOUCH_UP);

            m_onTheWeb = false;
        }

        void Update()
        {
            m_rayInteractor.TryGetHitInfo(out Vector3 position, out Vector3 normal, out int positionInLine, out bool isValidate);
            Vector3 invertPositoin = m_webViewRect.transform.InverseTransformPoint(position);
            invertPositoin.z *= m_webViewRect.transform.lossyScale.z;

            float uvX = invertPositoin.x / m_webViewRect.rect.width + m_webViewRect.pivot.x;
            float uvY = 1.0f - (invertPositoin.y / m_webViewRect.rect.height + m_webViewRect.pivot.x);

            if (Mathf.Abs(invertPositoin.z) < m_rectZThreshold &&
                uvX >= 0.0f && uvX <= 1.0f && uvY >= 0.0f && uvY <= 1.0f)
            {
                m_onTheWeb = true;

                m_lastXPos = (int)(uvX * m_tlabWebView.WebWidth);
                m_lastYPos = (int)(uvY * m_tlabWebView.WebHeight);

                m_tlabWebView.TouchEvent(m_lastXPos, m_lastYPos, GetButtonEvent());
            }
            else
            {
                TouchRelease();
            }
        }
    }
}
