using System.Linq;
using UnityEngine;

namespace InteractionSystem.Runtime.Player
{
/// <summary>
/// Detects IInteractable objects and forwards interaction input.
/// Supports two detection modes: Raycast (center-screen) and OverlapSphere (proximity).
/// Picks the nearest interactable when multiple are in-range.
/// </summary>
    public class InteractionDetector : MonoBehaviour
    {
        #region Nested Types

        public enum DetectionMode
        {
            RaycastCenter,
            OverlapSphere
        }

        #endregion

        #region Fields

        [Header("References")]
        [SerializeField] private Transform m_interactionOrigin; // usually player camera or player head

        [Header("Detection")]
        [SerializeField] private DetectionMode m_detectionMode = DetectionMode.RaycastCenter;
        [SerializeField] private float m_interactionRange = 2.5f;
        [SerializeField] private LayerMask m_interactableLayer = ~0; // default: everything

        [Header("Input")]
        [Tooltip("The input name used with Input.GetButtonDown / GetButton. Can be set in Inspector.")]
        [SerializeField] private string m_interactionInput = "Interact"; // configurable in Inspector

        [Header("Raycast")]
        [SerializeField] private float m_raycastRadius = 0.1f; // spherecast radius for more forgiving aim

        // current focused interactable
        private InteractionSystem.Runtime.Core.IInteractable m_current;

        #endregion

        #region Properties

        /// <summary>
        /// Interaction range in meters.
        /// </summary>
        public float InteractionRange => m_interactionRange;

        #endregion

        #region Unity Methods

        private void Reset()
        {
            // sensible default: main camera as origin if available
            if (m_interactionOrigin == null && Camera.main != null)
                m_interactionOrigin = Camera.main.transform;
        }

        private void Update()
        {
            UpdateFocus();
            HandleInput();
        }

        private void OnDrawGizmosSelected()
        {
            if (m_interactionOrigin == null) return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(m_interactionOrigin.position, m_interactionRange);

            // draw a forward ray/sphere for raycast mode
            if (m_detectionMode == DetectionMode.RaycastCenter)
            {
                Gizmos.DrawRay(m_interactionOrigin.position, m_interactionOrigin.forward * m_interactionRange);
            }
        }

        #endregion

        #region Detection & Input

        private void UpdateFocus()
        {
            var found = DetectInteractables();

            var nearest = found
                .OrderBy(x => Vector3.Distance(GetOriginPosition(), GetInteractablePosition(x)))
                .FirstOrDefault();

            if (nearest != null && !ReferenceEquals(nearest, m_current))
            {
                m_current?.OnDefocus();
                m_current = nearest;
                m_current.OnFocus();
            }
            else if (nearest == null && m_current != null)
            {
                m_current.OnDefocus();
                m_current = null;
            }
        }

        private System.Collections.Generic.List<InteractionSystem.Runtime.Core.IInteractable> DetectInteractables()
        {
            var list = new System.Collections.Generic.List<InteractionSystem.Runtime.Core.IInteractable>();

            if (m_interactionOrigin == null)
                return list;

            if (m_detectionMode == DetectionMode.OverlapSphere)
            {
                var cols = Physics.OverlapSphere(GetOriginPosition(), m_interactionRange, m_interactableLayer);
                foreach (var c in cols)
                {
                    var interactable = c.GetComponentInParent<InteractionSystem.Runtime.Core.IInteractable>();
                    if (interactable != null)
                        list.Add(interactable);
                }
            }
            else // RaycastCenter
            {
                RaycastHit[] hits = Physics.SphereCastAll(GetOriginPosition(), m_raycastRadius, m_interactionOrigin.forward, m_interactionRange, m_interactableLayer);
                foreach (var h in hits)
                {
                    var interactable = h.collider.GetComponentInParent<InteractionSystem.Runtime.Core.IInteractable>();
                    if (interactable != null)
                        list.Add(interactable);
                }
            }

            return list;
        }

        private Vector3 GetOriginPosition()
        {
            return m_interactionOrigin != null ? m_interactionOrigin.position : transform.position;
        }

        private Vector3 GetInteractablePosition(InteractionSystem.Runtime.Core.IInteractable interactable)
        {
            return (interactable.InteractionPoint != null) ? interactable.InteractionPoint.position : (interactable as MonoBehaviour).transform.position;
        }

        private void HandleInput()
        {
            if (string.IsNullOrEmpty(m_interactionInput))
                return;

            if (Input.GetButtonDown(m_interactionInput) && m_current != null)
            {
                m_current.Interact(gameObject);
            }
        }

        #endregion
    }
}