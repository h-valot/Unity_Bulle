using Sirenix.OdinInspector;
using UnityEngine;

public class EnterGarden : MonoBehaviour
{
    [FoldoutGroup("Scriptable")][SerializeField] private RSE_SetBubble m_rseSetBubble;
    [FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentPanel m_rsoCurrentPanel;
    // Start is called before the first frame update

    private void OnEnable()
    {
        m_rsoCurrentPanel.OnChanged += ActivateDialogue;
    }

    private void OnDisable()
    {
        m_rsoCurrentPanel.OnChanged -= ActivateDialogue;
    }

    private void ActivateDialogue(PanelType panelType)
    {
        if(panelType == PanelType.GARDEN)
        {
            m_rseSetBubble.Call(CharacterType.FISHMONGER_SHORTKING, 0);
            m_rseSetBubble.Call(CharacterType.FISHMONGER_TALL, 0);
        }
    }
}
