using UnityEngine;
using UnityEngine.Events;

namespace InteractionSystem.Runtime.Interactables
{
    public class SwitchInteractable : ToggleInteractable
    {
        [Header("Events")]
        [SerializeField] private UnityEvent<bool> m_onSwitch;

        protected override void ApplyState(bool on)
        {
            base.ApplyState(on);
            m_onSwitch?.Invoke(on);
        }
    }
}
