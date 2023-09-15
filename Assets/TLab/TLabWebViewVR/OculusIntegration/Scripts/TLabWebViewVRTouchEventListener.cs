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

            float uvX = invertPositoin.x / m_webViewRect.rect.width + 0.5f;
            float uvY = 1.0f - (invertPositoin.y / m_webViewRect.rect.height + 0.5f);

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
