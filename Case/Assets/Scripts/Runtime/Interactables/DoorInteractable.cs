using UnityEngine;
using InteractionSystem.Runtime.Player;
using System.Collections;

namespace InteractionSystem.Runtime.Interactables
{
    public class DoorInteractable : ToggleInteractable
    {
        #region Fields
        
        [Header("Lock")]
        [SerializeField] private bool m_isLocked = false;
        [SerializeField] private ItemData m_requiredKey;


        [Header("UI")]
        [SerializeField] private string m_lockedPrompt = "Locked - Key Required";

        [Header("Animation")]
        [SerializeField] private Animator m_animator;
        [SerializeField] private string m_openParam = "Open";

            [Header("Door Settings")]
    [SerializeField] private Transform m_doorPivot;
    [SerializeField] private float m_openAngle = 90f;
    [SerializeField] private float m_rotateSpeed = 6f;

    private Quaternion m_closedRot;
    private Quaternion m_openRot;

  #endregion

private void Awake()
{
    if (m_doorPivot == null)
        m_doorPivot = transform;

    m_closedRot = m_doorPivot.localRotation;
    m_openRot = Quaternion.Euler(0f, m_openAngle, 0f);
}

public void SetDoorState(bool open)
{
    ApplyState(open);
}


    private IEnumerator RotateDoor(Quaternion target)
    {
        while (Quaternion.Angle(m_doorPivot.localRotation, target) > 0.5f)
        {
            m_doorPivot.localRotation =
                Quaternion.Slerp(
                    m_doorPivot.localRotation,
                    target,
                    Time.deltaTime * m_rotateSpeed
                );

            yield return null;
        }

        m_doorPivot.localRotation = target;
    }
        public override string PromptText
        {
            get
            {
                if (m_isLocked)
                    return m_lockedPrompt;

                return base.PromptText;
            }
        }

        public override void Interact(GameObject interactor)
        {
        if (m_isLocked)
        {
            var inv = interactor.GetComponent<PlayerInventory>();
            if (inv != null && inv.HasItem(m_requiredKey))
            {
                m_isLocked = false;
            }
            else
            {
                return;
            }
        }

            base.Interact(interactor); // toggle state
        }

protected override void ApplyState(bool on)
{
    Debug.Log("DOOR APPLY STATE: " + on);

    if (m_animator != null)
    {
        m_animator.SetBool(m_openParam, on);
        return;
    }

    if (m_doorPivot == null)
        return;

    StopAllCoroutines();
    StartCoroutine(RotateDoor(on ? m_openRot : m_closedRot));
}
    }
}
