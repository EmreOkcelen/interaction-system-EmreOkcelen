using UnityEngine;
using UnityEngine.Events;
using InteractionSystem.Runtime.Core;
using InteractionSystem.Runtime.Player;

namespace InteractionSystem.Runtime.Interactables
{
    /// <summary>
    /// Modular instant interactable that can act as:
    /// - Pickup (adds key to PlayerInventory or fires events)
    /// - Button (triggers animator / events)
    /// - Custom (only fires events)
    /// 
    /// Designed to be overrideable and extensible.
    /// </summary>
    [DisallowMultipleComponent]
    public class InstantInteractable : InteractableBase
    {
        #region Types

        public enum InstantMode
        {
            Pickup,
            Button,
            Custom
        }

        #endregion

        #region Fields

        [Header("General")]
        [SerializeField] private string m_label = "Interact";
        [SerializeField] private InstantMode m_mode = InstantMode.Pickup;

        [Header("Pickup (Pickup mode)")]
        [Tooltip("If non-empty and player has PlayerInventory, this keyId will be added on pickup.")]
        [SerializeField] private string m_keyId = "GoldGate";
        [SerializeField] private bool m_deactivateOnPickup = true;

        [Header("Button (Button mode)")]
        [Tooltip("Optional animator to trigger when used as a button.")]
        [SerializeField] private Animator m_animator;
        [SerializeField] private string m_animatorTrigger = "Press";

        [Header("Common VFX / SFX")]
        [SerializeField] private AudioClip m_sound;
        [SerializeField] private ParticleSystem m_vfx;
        [SerializeField] private bool m_playVfxOnInteract = true;
        [SerializeField] private bool m_playSfxOnInteract = true;

        [Header("Events")]
        [Tooltip("Generic event invoked without parameters.")]
        [SerializeField] private UnityEvent m_onInteract = new UnityEvent();

        [Tooltip("Event invoked with the interactor GameObject (player).")]
        [SerializeField] private UnityEvent<GameObject> m_onInteractWithActor = new UnityEvent<GameObject>();

        #endregion

        #region Unity Methods

        private void Reset()
        {
            m_label = "Interact";
            m_mode = InstantMode.Pickup;
        }

        #endregion

        #region IInteractable

        /// <inheritdoc/>
        public override void Interact(GameObject interactor)
        {
            Debug.Log($"[InstantInteractable] {name} interacted by {interactor.name}. Mode={m_mode}");

            // Common effects
            if (m_playVfxOnInteract && m_vfx != null)
                m_vfx.Play();

            if (m_playSfxOnInteract && m_sound != null)
                AudioSource.PlayClipAtPoint(m_sound, transform.position);

            // Mode-specific handling
            switch (m_mode)
            {
                case InstantMode.Pickup:
                    HandlePickup(interactor);
                    break;
                case InstantMode.Button:
                    HandleButton(interactor);
                    break;
                case InstantMode.Custom:
                default:
                    // Nothing special, just fire events
                    break;
            }

            // Fire events so designer can hook arbitrary behaviour
            m_onInteract?.Invoke();
            m_onInteractWithActor?.Invoke(interactor);
        }

        #endregion

        #region Protected / Virtual handlers (override to customize)

        /// <summary>
        /// Handle pickup behaviour. Override to provide custom pickup logic.
        /// Default: if interactor has PlayerInventory -> AddKey(m_keyId). Then deactivate or destroy depending on inspector.
        /// </summary>
        /// <param name="interactor">Interactor (player)</param>
        protected virtual void HandlePickup(GameObject interactor)
        {
            if (!string.IsNullOrEmpty(m_keyId))
            {
                var inv = interactor.GetComponent<PlayerInventory>();
                if (inv != null)
                {
                    inv.AddKey(m_keyId);
                    Debug.Log($"[InstantInteractable] Added key '{m_keyId}' to {interactor.name}");
                }
                else
                {
                    Debug.LogWarning($"[InstantInteractable] Interactor {interactor.name} has no PlayerInventory to receive key '{m_keyId}'.");
                }
            }

            if (m_deactivateOnPickup)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Handle button behaviour. Default: trigger animator (if assigned).
        /// Override to call remote systems programmatically.
        /// </summary>
        /// <param name="interactor">Interactor (player)</param>
        protected virtual void HandleButton(GameObject interactor)
        {
            if (m_animator != null && !string.IsNullOrEmpty(m_animatorTrigger))
            {
                m_animator.SetTrigger(m_animatorTrigger);
            }
        }

        #endregion
    }
}
