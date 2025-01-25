using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentItem m_rsoCurrentItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentPanel m_rsoCurrentPanel;

	private void Awake()
	{
		m_rsoCurrentItem.Value = ItemType.NONE;
		m_rsoCurrentPanel.Value = PanelType.HARBOR;
	}
}