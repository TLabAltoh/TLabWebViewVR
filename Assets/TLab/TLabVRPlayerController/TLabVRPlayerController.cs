using UnityEngine;

namespace TLab.XR.Oculus
{
    public class TLabVRPlayerController : BasePlayerController
    {
        [Header("Input config")]
        [SerializeField] private OVRInput.Controller m_moveController;
        [SerializeField] private OVRInput.Button m_jumpButton = OVRInput.Button.One;
        [SerializeField] private OVRInput.Button m_runButton = OVRInput.Button.Two;

        protected override void Start()
        {
            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            if (OVRInput.GetDown(m_jumpButton))
            {
                Jump();
            }

            Vector2 input = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, m_moveController);

            Vector3 targetForward = new Vector3(m_directionAnchor.forward.x, ZERO, m_directionAnchor.forward.z).normalized;
            Vector3 targetRight = new Vector3(m_directionAnchor.right.x, ZERO, m_directionAnchor.right.z).normalized;
            Vector3 targetMove = targetForward * input.y + targetRight * input.x;
            Vector3 targetJump = new Vector3(ZERO, m_currentJumpVelocity, ZERO);

            float final = OVRInput.Get(m_runButton) ? m_runSpeed : m_moveSpeed;

            m_controller.Move((targetMove * final + targetJump) * Time.deltaTime);
            // m_rb.MovePosition(this.transform.position + (targetMove * final + targetJump) * Time.deltaTime);
        }
    }
}
