using UnityEngine;

public class InteractionHighlight : MonoBehaviour
{
    [SerializeField] private RSO_CurrentItem m_rsoCurrentItem;
    [SerializeField] private ItemType m_itemType;
    [SerializeField] private SpriteRenderer m_spriteRenderer;


    private void OnEnable()
    {
        m_rsoCurrentItem.OnChanged += ActivateHighLight;
    }

    private void OnDisable()
    {
        m_rsoCurrentItem.OnChanged -= ActivateHighLight;
    }

    private void ActivateHighLight(ItemType itemType)
    {
        if (m_itemType == itemType)
        {
            m_spriteRenderer.enabled = true;
        }
        else
        {
            m_spriteRenderer.enabled = false;
        }
    }
}
