using UnityEngine;
using UnityEngine.EventSystems;

namespace TLab.XR.Oculus
{
    public class HandTrackingSample : MonoBehaviour
    {
        [SerializeField] private TLabOVRInputModule m_inputModule;

        [SerializeField] private OVRHand m_leftHand;
        [SerializeField] private OVRHand m_rightHand;

        void Start()
        {
            m_inputModule.joyPadClickButton = OVRInput.Button.One;  // pinch down

            m_inputModule.rayTransformRight = m_rightHand.PointerPose;
            m_inputModule.rayTransformLeft = m_rightHand.PointerPose;
        }
    }
}
