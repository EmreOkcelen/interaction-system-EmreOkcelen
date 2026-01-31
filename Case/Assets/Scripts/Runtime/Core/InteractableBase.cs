using UnityEngine;


namespace InteractionSystem.Runtime.Interactables
{
/// <summary>
/// Convenience base class providing default implementations for IInteractable.
/// Use this to reduce boilerplate on simple interactables.
/// </summary>
[DisallowMultipleComponent]
public abstract class InteractableBase : MonoBehaviour, InteractionSystem.Runtime.Core.IInteractable
{
#region Fields

[Header("Interaction")]
[SerializeField] private Transform m_interactionPoint;
[Header("UI")]
[SerializeField] protected string m_promptText = "Interact";

#endregion


#region IInteractable
public Transform InteractionPoint => m_interactionPoint != null ? m_interactionPoint : this.transform;
public virtual string PromptText => m_promptText;
public abstract void Interact(GameObject interactor);
public virtual void OnFocus() { }

public virtual void OnDefocus() { }


#endregion
}
}