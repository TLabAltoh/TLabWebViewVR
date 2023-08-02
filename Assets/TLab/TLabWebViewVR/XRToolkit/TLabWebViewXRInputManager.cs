using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TLabWebViewXRInputManager : MonoBehaviour
{
    // https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/api/UnityEngine.XR.Interaction.Toolkit.InputHelpers.html

    [Header("Target WebView")]
    [SerializeField] private TLabWebView m_tlabWebView;

    [Header("Raycast Setting")]
    [SerializeField] private Transform m_anchor;
    [SerializeField] private float m_rayMaxLength = 10.0f;
    [SerializeField] private LayerMask m_webViewLayer;

    [Header("XR Input Settings")]
    [SerializeField] private XRNode m_controllerNode = XRNode.RightHand;
    [SerializeField] private InputHelpers.Button m_touchButton = InputHelpers.Button.TriggerButton;

    private InputDevice m_controller;

    private RaycastHit m_raycastHit;
    private int m_lastXPos;
    private int m_lastYPos;
    private bool m_onTheWeb = false;

    private bool m_lastPressed = false;

    private const int TOUCH_DOWN = 0;
    private const int TOUCH_UP = 1;
    private const int TOUCH_MOVE = 2;

    private int GetButtonEvent()
    {
        int eventNum = (int)TouchPhase.Stationary;

        bool pressed;
        m_controller.TryGetFeatureValue(CommonUsages.triggerButton, out pressed);

        Debug.Log(pressed);

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

    void Start()
    {
        m_controller = InputDevices.GetDeviceAtXRNode(m_controllerNode);
    }

    void Update()
    {
#if false
        GetButtonEvent();
#else
        Ray ray = new Ray(m_anchor.position, -m_anchor.forward);
        if (Physics.Raycast(ray, out m_raycastHit, m_rayMaxLength, m_webViewLayer))
        {
            m_onTheWeb = true;

            m_lastXPos = (int)((1.0f - m_raycastHit.textureCoord.x) * m_tlabWebView.webWidth);
            m_lastYPos = (int)(m_raycastHit.textureCoord.y * m_tlabWebView.webHeight);

            int eventNum = GetButtonEvent();
            Debug.Log(eventNum);

            m_tlabWebView.TouchEvent(m_lastXPos, m_lastYPos, eventNum);
        }
        else
        {
            if (m_onTheWeb)
            {
                m_tlabWebView.TouchEvent(m_lastXPos, m_lastYPos, TOUCH_UP);
            }
        }
#endif
    }
}
