using UnityEngine;

namespace InteractionSystem.Runtime.Player
{
    /// <summary>
    /// Simple first-person player controller for testing the interaction system.
    /// Supports WASD movement and mouse look. Uses CharacterController.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        #region Fields

        [Header("Movement")]
        [SerializeField] private float m_moveSpeed = 4f;
        [SerializeField] private float m_sprintMultiplier = 1.6f;

        [Header("Look")]
        [SerializeField] private Transform m_camera; // assign main camera (child)
        [SerializeField] private float m_lookSensitivity = 2.0f;
        [SerializeField] private float m_maxLookAngle = 85f;

        [Header("Physics")]
        [SerializeField] private float m_gravity = -9.81f;

        private CharacterController m_characterController;
        private float m_verticalVelocity = 0f;
        private float m_pitch = 0f;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_characterController = GetComponent<CharacterController>();
            if (m_camera == null && Camera.main != null)
                m_camera = Camera.main.transform;

            // lock cursor for testing
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            HandleLook();
            HandleMove();
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        #endregion

        #region Movement & Look

        private void HandleLook()
        {
            if (m_camera == null) return;

            float mouseX = Input.GetAxis("Mouse X") * m_lookSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * m_lookSensitivity;

            // yaw on player
            transform.Rotate(Vector3.up * mouseX);

            // pitch on camera
            m_pitch -= mouseY;
            m_pitch = Mathf.Clamp(m_pitch, -m_maxLookAngle, m_maxLookAngle);
            m_camera.localEulerAngles = new Vector3(m_pitch, 0f, 0f);
        }

        private void HandleMove()
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputZ = Input.GetAxisRaw("Vertical");

            Vector3 move = transform.right * inputX + transform.forward * inputZ;
            move = move.normalized;

            bool sprint = Input.GetKey(KeyCode.LeftShift);
            float speed = m_moveSpeed * (sprint ? m_sprintMultiplier : 1f);

            // gravity
            if (m_characterController.isGrounded && m_verticalVelocity < 0f)
                m_verticalVelocity = -1f; // small grounded offset

            m_verticalVelocity += m_gravity * Time.deltaTime;
            move.y = m_verticalVelocity;

            m_characterController.Move(move * speed * Time.deltaTime);
        }

        #endregion
    }
}