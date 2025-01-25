using Sirenix.OdinInspector;
using UnityEngine;

public class PanelSetter : MonoBehaviour
{
	[SerializeField] private PanelType m_newPanel;

	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentPanel m_rsoCurrentPanel;

	[Button] public void SetPanel()
	{
		m_rsoCurrentPanel.Value = m_newPanel;
	}
}