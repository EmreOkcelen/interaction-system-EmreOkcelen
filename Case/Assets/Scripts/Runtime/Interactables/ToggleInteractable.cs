using UnityEngine;

namespace InteractionSystem.Runtime.Interactables
{
    /// <summary>
    /// Toggle-type interactable. On each Interact() toggles between states.
    /// Example: light switches, open/close doors (simple toggle).
    /// </summary>
    [DisallowMultipleComponent]
    public class ToggleInteractable : InteractableBase
    {
        #region Fields

        [SerializeField] private bool m_isOn = false;
        [SerializeField] private string m_onLabel = "Turn Off";
        [SerializeField] private string m_offLabel = "Turn On";

        #endregion

        #region IInteractable

        /// <inheritdoc/>
        public override void Interact(GameObject interactor)
        {
            m_isOn = !m_isOn;
            ApplyState(m_isOn);
            Debug.Log($"[ToggleInteractable] {name} toggled to {(m_isOn ? "On" : "Off")} by {interactor.name}");
        }

        #endregion

        #region Protected

        /// <summary>
        /// Apply visual/audio/logic for the specified state.
        /// Override this to implement custom visuals (animator, light, etc).
        /// </summary>
        /// <param name="on">Target state.</param>
        protected virtual void ApplyState(bool on)
        {
            // Default: enable/disable root renderer(s) as a trivial visual.
            var renderers = GetComponentsInChildren<Renderer>();
            foreach (var r in renderers) r.enabled = on;
        }

        #endregion
    }
}
