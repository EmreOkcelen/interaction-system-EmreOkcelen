using UnityEngine;
using InteractionSystem.Runtime.Player;

namespace InteractionSystem.Runtime.Interactables
{
    public class ChestInteractable : HoldInteractable
    {
        [Header("Chest")]
        [SerializeField] private bool m_opened = false;

        [Header("Reward")]
        [SerializeField] private string m_keyInside;
        [SerializeField] private ItemData m_itemInside;


        [Header("Animation")]
        [SerializeField] private Animator m_animator;
        [SerializeField] private string m_openTrigger = "Open";

        public override string PromptText =>
            m_opened ? string.Empty : base.PromptText;

        protected override void CompleteInteraction(GameObject interactor)
        {
            if (m_opened)
                return;

            m_opened = true;

            if (m_animator != null)
                m_animator.SetTrigger(m_openTrigger);

            // reward
        if (m_itemInside != null)
        {
            var inv = interactor.GetComponent<PlayerInventory>();
            if (inv != null)
                inv.AddItem(m_itemInside);
        }

            Debug.Log("[Chest] Opened and rewarded.");
        }
    }
}
