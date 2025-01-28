using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[FoldoutGroup("Scriptable")][SerializeField] private SSO_Character m_ssoCharacter;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentItem m_rsoCurrentItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentPanel m_rsoCurrentPanel;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_ToggleMitigedGravity m_rsoToggleMitigedGravity;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SetBubble m_rseSetBubble; 
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_LockInputs m_rsoLockInputs;

	private void Start()
	{
		m_rseSetBubble.Call(CharacterType.FISHERMAN, -1);
		m_rseSetBubble.Call(CharacterType.FISHMONGER_SHORTKING, -1);
		m_rseSetBubble.Call(CharacterType.FISHMONGER_TALL, -1);
		m_rseSetBubble.Call(CharacterType.LETTER, -1);
		m_rseSetBubble.Call(CharacterType.SINGER, -1);
		m_rseSetBubble.Call(CharacterType.LOVER, 0);
		m_rseSetBubble.Call(CharacterType.COAST_GUARD, 0);
		m_rseSetBubble.Call(CharacterType.GRANDPA, 0);

		m_rsoCurrentItem.Value = ItemType.NONE;
		m_rsoCurrentPanel.Value = PanelType.HARBOR;
		
		m_rsoToggleMitigedGravity.Value = false;
		m_rsoLockInputs.Value = false;
	}
}