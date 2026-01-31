using UnityEngine;
using InteractionSystem.Runtime.Core;

namespace InteractionSystem.Runtime.Interactables
{
    /// <summary>
    /// Hold-type interactable. Requires player to hold the interaction button for m_holdDuration seconds.
    /// Example: chests that open while holding, forced actions.
    /// </summary>
    [DisallowMultipleComponent]
    public class HoldInteractable : InteractableBase, IHoldable
    {
        #region Fields

        [SerializeField, Tooltip("Seconds required to hold interaction to complete.")]
        private float m_holdDuration = 1.5f;

        [SerializeField, Tooltip("Optional animator for open animation.")]
        private Animator m_animator;

        [SerializeField] private string m_holdPrompt = "Hold E to Interact";

        #endregion

        #region UI
        
        public override string PromptText => m_holdPrompt;

        #endregion
        
        #region IHoldable

        /// <inheritdoc/>
        public float HoldDuration => m_holdDuration;

        /// <inheritdoc/>
        public void OnHoldStart(GameObject interactor)
        {
            Debug.Log($"[HoldInteractable] Hold started on {name} by {interactor.name}");
            // e.g. play start sound / set animator param
            if (m_animator != null) m_animator.SetBool("HoldActive", true);
        }

        /// <inheritdoc/>
        public void OnHoldProgress(float progress)
        {
            // Optional: visual feedback (handled by InteractionProgressBar typically).
            // Debug.Log($"Hold progress: {progress:P0}");
        }

        /// <inheritdoc/>
        public void OnHoldComplete(GameObject interactor)
        {
            Debug.Log($"[HoldInteractable] Hold complete on {name} by {interactor.name}");
            CompleteInteraction(interactor);
        }

        /// <inheritdoc/>
        public void OnHoldCancelled()
        {
            Debug.Log($"[HoldInteractable] Hold cancelled on {name}");
            if (m_animator != null) m_animator.SetBool("HoldActive", false);
        }

        #endregion

        #region Protected

        /// <summary>
        /// Called when hold completes — override to apply custom effects (loot spawn / open chest).
        /// </summary>
        /// <param name="interactor">Interactor (player).</param>
        protected virtual void CompleteInteraction(GameObject interactor)
        {
            // Default: disable the object (simulate pick-up or opened chest)
            gameObject.SetActive(false);
        }

        public override void Interact(GameObject interactor)
        {
    // Hold interactables DO NOT use instant interact
        }

        #endregion
    }
}
