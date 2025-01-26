using DG.Tweening;
using UnityEngine;

public class PanelsManager : MonoBehaviour
{
    [Header("Scriptable")]
    [SerializeField] private RSO_CurrentPanel m_rsoCurrentPanel;
    [SerializeField] private RSE_SetCharacterScale m_rseSetCharacterScale;
    [SerializeField] private SSO_Character m_ssoCharacter;

    [Header("Panel GameObjects")]
    [SerializeField] private GameObject[] m_panels;
    [SerializeField] private PanelType[] m_panelsType;
    [SerializeField] private bool[] m_arePanelsDiscovered;

	private void Start()
    {
        //Check what are the panel discovered at start
        for (int i = 0; i < m_panels.Length; i++)
        {
			m_panels[i].transform.localScale = new Vector3(1, 0, 1);
            if (m_arePanelsDiscovered[i])
            {
                RevealPanel(i);
			}
		}    
    }

    private void OnEnable()
    {
        m_rsoCurrentPanel.OnChanged += CheckPanelDiscovered;
        m_rsoCurrentPanel.OnChanged += TogglePanelSpecialAction;
    }

    private void OnDisable()
    {
        m_rsoCurrentPanel.OnChanged -= CheckPanelDiscovered;
        m_rsoCurrentPanel.OnChanged -= TogglePanelSpecialAction;
    }

    private void CheckPanelDiscovered(PanelType panelType)
    {
        int panelIndex = 0;
        
        for (int i = 0; i < m_panels.Length; i++)
        {
            if (m_panelsType[i] == panelType)
            {
                panelIndex = i;
                break;
            }
        }

        if (!m_arePanelsDiscovered[panelIndex])
        {
            RevealPanel(panelIndex);
            m_arePanelsDiscovered[panelIndex] = true;
        }
    }

    private void RevealPanel(int panelIndex)
    {
        m_panels[panelIndex].transform.DOScaleY(1f, 0.3f);
    }

    private void TogglePanelSpecialAction(PanelType panelType)
    {
        //Scale Up if in House, or reset scale
        if (panelType == PanelType.HOUSE) 
        {
            m_rseSetCharacterScale.Call(m_ssoCharacter.GraphicsScaleInHouse);
        }
        else
        {
            m_rseSetCharacterScale.Call(m_ssoCharacter.GraphicsScaleBase);
        }
    }
}
