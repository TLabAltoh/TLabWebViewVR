using UnityEngine;

namespace TLab.XR
{
    public class BasePlayerController : MonoBehaviour
    {
        [Header("Head Transform")]
        [SerializeField] protected Transform m_directionAnchor;

        [Header("Move")]
        [SerializeField] protected float m_runSpeed = 1.5f;
        [SerializeField] protected float m_moveSpeed = 1.0f;

        [Header("Jump")]
        [SerializeField] protected float m_groundThreshold = 0.2f;
        [SerializeField] protected float m_jumpHeight = 1.5f;
        [SerializeField] protected float m_jumpInertia = 0.5f;
        [SerializeField] protected float m_gravity = 1.0f;

        [Header("Charactor Controller")]
        [SerializeField] protected CharacterController m_controller;

        protected const float ZERO = 0.0f;
        protected const float RAY_OFFSET = 0.1f;

        protected bool m_onGround = false;
        protected float m_currentJumpInertia = ZERO;
        protected float m_currentJumpVelocity = ZERO;

        protected Ray m_ray;
        protected RaycastHit m_raycastHit;

        protected const float FALLING_STABILITY = -10.0f;

        protected virtual void Jump()
        {
            if (m_onGround)
            {
                m_currentJumpVelocity = m_jumpHeight;
                m_currentJumpInertia = m_jumpInertia;
            }
        }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            m_currentJumpInertia -= Time.deltaTime;
            m_currentJumpVelocity -= Time.deltaTime * m_gravity;

            m_currentJumpVelocity = m_currentJumpVelocity < FALLING_STABILITY ? FALLING_STABILITY : m_currentJumpVelocity;

            m_ray = new Ray(m_controller.transform.position + Vector3.up * RAY_OFFSET, Vector3.down);

            m_onGround = Physics.Raycast(m_ray, out m_raycastHit, m_groundThreshold);

            if (m_onGround)
            {
                m_currentJumpVelocity = m_currentJumpInertia < ZERO ? ZERO : m_currentJumpVelocity;
            }
        }
    }
}
