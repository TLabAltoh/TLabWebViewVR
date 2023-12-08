using UnityEngine;
using TLab.Android.WebView;

namespace TLab.XR.Oculus
{
    public class TLabWebViewVRTouchEventListener : MonoBehaviour
    {
        [Header("Target WebView")]
        [SerializeField] private TLabWebView m_tlabWebView;

        [Header("Input Settings")]
        [SerializeField] private Transform m_pointerPos;
        [SerializeField] private RectTransform m_webViewRect;

        [SerializeField] private OVRInput.Controller m_controller;
        [SerializeField] private OVRInput.Button m_touchButton;

        private int m_lastXPos;
        private int m_lastYPos;
        private bool m_onTheWeb = false;

        private const int TOUCH_DOWN = 0;
        private const int TOUCH_UP = 1;
        private const int TOUCH_MOVE = 2;

        private const float m_rectZThreshold = 0.05f;

        public OVRInput.Controller controller { get => m_controller; set => m_controller = value; }

        public OVRInput.Button touchButton { get => m_touchButton; set => m_touchButton = value; }

        void TouchRelease()
        {
            if (m_onTheWeb)
            {
                m_tlabWebView.TouchEvent(m_lastXPos, m_lastYPos, TOUCH_UP);
            }

            m_onTheWeb = false;
        }

        void Update()
        {
            Vector3 invertPositoin = m_webViewRect.transform.InverseTransformPoint(m_pointerPos.position);

            // https://docs.unity3d.com/jp/2018.4/ScriptReference/Transform.InverseTransformPoint.html
            invertPositoin.z *= m_webViewRect.transform.lossyScale.z;

            float uvX = invertPositoin.x / m_webViewRect.rect.width + m_webViewRect.pivot.x;
            float uvY = 1.0f - (invertPositoin.y / m_webViewRect.rect.height + m_webViewRect.pivot.x);

            if (Mathf.Abs(invertPositoin.z) < m_rectZThreshold &&
                uvX >= 0.0f && uvX <= 1.0f && uvY >= 0.0f && uvY <= 1.0f)
            {
                m_onTheWeb = true;

                m_lastXPos = (int)(uvX * m_tlabWebView.WebWidth);
                m_lastYPos = (int)(uvY * m_tlabWebView.WebHeight);

                int eventNum = (int)TouchPhase.Stationary;
                if (OVRInput.GetUp(m_touchButton, m_controller))
                {
                    eventNum = TOUCH_UP;
                }
                else if (OVRInput.GetDown(m_touchButton, m_controller))
                {
                    eventNum = TOUCH_DOWN;
                }
                else if (OVRInput.Get(m_touchButton, m_controller))
                {
                    eventNum = TOUCH_MOVE;
                }

                m_tlabWebView.TouchEvent(m_lastXPos, m_lastYPos, eventNum);
            }
            else
            {
                TouchRelease();
            }
        }
    }
}
