using System.Collections.Generic;
using UnityEngine;

namespace InteractionSystem.Runtime.Player
{
    /// <summary>
    /// Very small inventory used for testing (key collection).
    /// Not a full featured inventory — just enough for the case.
    /// </summary>
    public class PlayerInventory : MonoBehaviour
    {
        #region Fields

        [SerializeField] private List<ItemData> m_items = new();

        #endregion

        #region API
       public void AddItem(ItemData item)
        {
            if (!m_items.Contains(item))
            m_items.Add(item);
        }

        public bool HasItem(ItemData item) => m_items.Contains(item);

        public IReadOnlyList<ItemData> Items => m_items;
        #endregion
    }
}