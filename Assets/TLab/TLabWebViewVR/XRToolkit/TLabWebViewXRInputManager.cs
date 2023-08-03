using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

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
    [SerializeField] private InputActionReference m_triggerPress;
    //[SerializeField] private XRNode m_controllerNode = XRNode.RightHand;
    //[SerializeField] private InputHelpers.Button m_touchButton = InputHelpers.Button.TriggerButton;

    [Header("XR Interact Line Visual")]
    [SerializeField] private XRInteractorLineVisual m_lineVisual;
    [SerializeField] private LineRenderer m_lineRenderer;

    //private InputDevice m_controller;

    private RaycastHit m_raycastHit;
    private int m_lastXPos;
    private int m_lastYPos;
    private bool m_onTheWeb = false;

    private bool m_lastPressed = false;

    private const int TOUCH_DOWN = 0;
    private const int TOUCH_UP = 1;
    private const int TOUCH_MOVE = 2;

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

    void Update()
    {
        Ray ray = new Ray(m_anchor.position, m_anchor.forward);
        if (Physics.Raycast(ray, out m_raycastHit, m_rayMaxLength, m_webViewLayer))
        {
            m_onTheWeb = true;

            m_lastXPos = (int)((1.0f - m_raycastHit.textureCoord.x) * m_tlabWebView.webWidth);
            m_lastYPos = (int)(m_raycastHit.textureCoord.y * m_tlabWebView.webHeight);

            m_tlabWebView.TouchEvent(m_lastXPos, m_lastYPos, GetButtonEvent());
        }
        else
        {
            if (m_onTheWeb)
            {
                m_tlabWebView.TouchEvent(m_lastXPos, m_lastYPos, TOUCH_UP);
            }

            m_onTheWeb = false;
        }
    }
}
