using UnityEngine;
using InteractionSystem.Runtime.Core;

namespace InteractionSystem.Runtime.Interactables
{
    public class InteractableKey : InteractableBase
    {
        [SerializeField] private string m_keyId = "GoldGate";
        public override void Interact(GameObject interactor)
        {
            var inv = interactor.GetComponent<Player.PlayerInventory>();
            if (inv != null)
            {
                inv.AddKey(m_keyId);
                Debug.Log($"Key {m_keyId} picked up by {interactor.name}");
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
