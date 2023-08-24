using UnityEngine;

namespace TLab.XR.Oculus
{
    public class TLabVRPlayerController : MonoBehaviour
    {
        [Header("Input config")]
        [SerializeField] private OVRInput.Controller m_moveController;
        [SerializeField] private OVRInput.Button m_jumpButton = OVRInput.Button.One;
        [SerializeField] private OVRInput.Button m_runButton = OVRInput.Button.Two;

        [Header("Head Transform")]
        [SerializeField] private Transform m_centerEyeAnchor;

        [Header("Move")]
        [SerializeField] private float m_runSpeed = 1.5f;
        [SerializeField] private float m_moveSpeed = 1.0f;

        [Header("Jump")]
        [SerializeField] private float m_jumpHeight = 1.5f;
        [SerializeField] private float m_jumpInertia = 0.5f;
        [SerializeField] private float m_gravity = 1.0f;

        [Header("Charactor Controller")]
        [SerializeField] private CharacterController m_controller;

        private float m_currentJumpInertia;
        private float m_currentJumpVelocity = 0.0f;

        void Start()
        {

        }

        void Update()
        {
            m_currentJumpInertia -= Time.deltaTime;
            m_currentJumpVelocity -= Time.deltaTime * m_gravity;

            if (m_currentJumpVelocity < -10.0f)
            {
                m_currentJumpVelocity = -10.0f;
            }

            Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_moveController);

            Ray ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 0.2f) == true)
            {
                if (m_currentJumpInertia < 0.0f)
                {
                    m_currentJumpVelocity = 0.0f;

                    if (OVRInput.GetDown(m_jumpButton) == true)
                    {
                        m_currentJumpVelocity = m_jumpHeight;

                        m_currentJumpInertia = m_jumpInertia;
                    }
                }
            }

            // Vector3 target = m_centerEyeAnchor.InverseTransformDirection(new Vector3(input.x, 0.0f, input.y));
            Vector3 targetForward = new Vector3(m_centerEyeAnchor.forward.x, 0.0f, m_centerEyeAnchor.forward.z).normalized;
            Vector3 targetRight = new Vector3(m_centerEyeAnchor.right.x, 0.0f, m_centerEyeAnchor.right.z).normalized;
            Vector3 targetMove = targetForward * input.y + targetRight * input.x;
            Vector3 targetJump = new Vector3(0.0f, m_currentJumpVelocity, 0.0f);

            float final = OVRInput.Get(m_runButton) ? m_runSpeed : m_moveSpeed;

            m_controller.Move((targetMove * final + targetJump) * Time.deltaTime);
            // m_rb.MovePosition(this.transform.position + (targetMove * final + targetJump) * Time.deltaTime);
        }
    }
}
