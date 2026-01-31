using UnityEngine;


namespace InteractionSystem.Runtime.Core
{
    
/// <summary>
/// Interface for objects that can be interacted with by the player.
/// Implement this on any GameObject that should be interactable.
/// </summary>    
public interface IInteractable
{

/// <summary>
/// The point to be used as the interaction origin on this object.
/// If null, the object's transform will be used.
/// </summary>
Transform InteractionPoint { get; }
/// <summary>
/// Called when the player triggers interaction.
/// </summary>
/// <param name="interactor">Reference to the interactor (usually player gameObject).</param>
void Interact(GameObject interactor);
/// <summary>
/// Called when the object becomes the currently focused interactable (e.g. for UI prompt).
/// </summary>
void OnFocus();
/// <summary>
/// Called when the object is no longer focused.
/// </summary>
void OnDefocus();
}
}