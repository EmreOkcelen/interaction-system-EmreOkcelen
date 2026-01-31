using UnityEngine;

namespace InteractionSystem.Runtime.Core
{
    /// <summary>
    /// Optional interface for interactables that require the player to hold the interaction input.
    /// InteractionDetector will handle the hold lifecycle (start/progress/complete/cancel).
    /// </summary>
    public interface IHoldable
    {
        /// <summary>
        /// How long (seconds) the player must hold the interaction button to complete.
        /// </summary>
        float HoldDuration { get; }

        /// <summary>
        /// Called once when the hold starts (Input.GetButtonDown).
        /// </summary>
        /// <param name="interactor">Interactor (usually the player gameObject).</param>
        void OnHoldStart(GameObject interactor);

        /// <summary>
        /// Called every frame during hold with normalized progress [0..1].
        /// </summary>
        /// <param name="progress">Normalized progress (0..1).</param>
        void OnHoldProgress(float progress);

        /// <summary>
        /// Called when the hold completes (player held long enough).
        /// </summary>
        /// <param name="interactor">Interactor (usually the player gameObject).</param>
        void OnHoldComplete(GameObject interactor);

        /// <summary>
        /// Called when hold is cancelled early (button released or moved out of range).
        /// </summary>
        void OnHoldCancelled();
    }
}
