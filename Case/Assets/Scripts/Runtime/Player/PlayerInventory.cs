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

        [SerializeField] private List<string> m_collectedKeys = new List<string>();

        #endregion

        #region API

        public void AddKey(string keyId)
        {
            if (!m_collectedKeys.Contains(keyId))
                m_collectedKeys.Add(keyId);
        }

        public bool HasKey(string keyId)
        {
            return m_collectedKeys.Contains(keyId);
        }

        #endregion
    }
}