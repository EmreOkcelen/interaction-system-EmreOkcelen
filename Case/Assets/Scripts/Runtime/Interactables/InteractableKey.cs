using UnityEngine;
using InteractionSystem.Runtime.Core;

namespace InteractionSystem.Runtime.Interactables
{
    public class InteractableKey : InteractableBase
    {
        [SerializeField] private ItemData m_keyItem;

        public override void Interact(GameObject interactor)
        {
            var inv = interactor.GetComponent<Player.PlayerInventory>();
            if (inv != null)
            {
                inv.AddItem(m_keyItem);
                Debug.Log($"Key {m_keyItem} picked up by {interactor.name}");
                gameObject.SetActive(false);
            }
        }

        public override void OnFocus()
        {
            // Optional: show prompt
            Debug.Log("Focus Key");
        }

        public override void OnDefocus()
        {
            Debug.Log("Defocus Key");
        }
    }
}
