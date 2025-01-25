using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[FoldoutGroup("Scriptable")][SerializeField] private SSO_Character m_ssoCharacter;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentItem m_rsoCurrentItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentPanel m_rsoCurrentPanel;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_ToggleMitigedGravity m_rsoToggleMitigedGravity;

	private void Start()
	{
		m_rsoCurrentItem.Value = ItemType.NONE;
		m_rsoCurrentPanel.Value = PanelType.HARBOR;
		m_rsoToggleMitigedGravity.Value = false;
	}
}