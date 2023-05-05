using UnityEngine;

public class TLabWebViewVRTouchEventManager : MonoBehaviour
{
    [Header("Target WebView")]
    [SerializeField] private TLabWebView m_tlabWebView;
    [Header("Raycast Setting")]
    [SerializeField] private Transform m_anchor;
    [SerializeField] private float m_rayMaxLength = 10.0f;
    [SerializeField] private LayerMask m_webViewLayer;
    [SerializeField] private OVRInput.Controller m_controller;
    [SerializeField] private OVRInput.Button m_touchButton;

    private RaycastHit m_raycastHit;
    private int m_lastXPos;
    private int m_lastYPos;
    private bool m_onTheWeb = false;

    private const int TOUCH_DOWN = 0;
    private const int TOUCH_UP = 1;
    private const int TOUCH_MOVE = 2;

    void Update()
    {
        Ray ray = new Ray(m_anchor.position, m_anchor.forward);
        if(Physics.Raycast(ray, out m_raycastHit, m_rayMaxLength, m_webViewLayer))
        {
            m_onTheWeb = true;

            m_lastXPos = (int)((1.0f - m_raycastHit.textureCoord.x) * m_tlabWebView.webWidth);
            m_lastYPos = (int)(m_raycastHit.textureCoord.y * m_tlabWebView.webHeight);

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
            if (m_onTheWeb)
            {
                m_tlabWebView.TouchEvent(m_lastXPos, m_lastYPos, TOUCH_UP);
            }
        }
    }
}
