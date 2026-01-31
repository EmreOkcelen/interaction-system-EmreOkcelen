using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InteractionSystem.Runtime.UI{
public class HoldProgressUI : MonoBehaviour
{
    [SerializeField] private Image m_fill;

    public void SetProgress(float value)
    {
        m_fill.fillAmount = value;
    }

    public void Hide()
    {
        m_fill.fillAmount = 0f;
    }
}
}