using UnityEngine;
using UnityEngine.UI;
using InteractionSystem.Runtime.Player;

using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private PlayerInventory m_inventory;
    [SerializeField] private Transform m_root;
    [SerializeField] private TextMeshProUGUI m_textPrefab;

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        foreach (Transform c in m_root)
            Destroy(c.gameObject);

        foreach (var item in m_inventory.Items)
        {
            var t = Instantiate(m_textPrefab, m_root);
            t.text = item.displayName;
        }
    }
}
