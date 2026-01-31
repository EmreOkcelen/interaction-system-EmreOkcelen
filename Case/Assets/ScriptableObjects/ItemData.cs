using UnityEngine;

[CreateAssetMenu(menuName = "Interaction/Item")]
public class ItemData : ScriptableObject
{
    public string itemId;
    public string displayName;
    public Sprite icon;
}
