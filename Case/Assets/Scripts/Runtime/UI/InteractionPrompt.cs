using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InteractionSystem.Runtime.UI
{
/// <summary>
/// Simple UI component to show an interaction prompt.
/// Hook this up to your InteractionDetector by subscribing to focus/defocus events
/// (or call it from IInteractable.OnFocus/OnDefocus implementations).
/// </summary>
    public class InteractionPrompt : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject m_root;
        [SerializeField] private  TextMeshProUGUI m_label;
        

        #endregion

        #region API

        public void Show(string text)
        {
            if (m_root != null) m_root.SetActive(true);
            if (m_label != null) m_label.text = text;
        }

        public void Hide()
        {
            if (m_root != null) m_root.SetActive(false);
        }

        #endregion
    }
}