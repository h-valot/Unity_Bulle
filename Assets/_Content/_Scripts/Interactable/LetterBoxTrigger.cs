using Sirenix.OdinInspector;
using UnityEngine;

public class LetterBoxTrigger : MonoBehaviour
{
	[Title("References")]
	[Required][SerializeField] private Interactable m_interactableFishermanDiving;

	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SetBubble m_rseSetBubble;

	private bool m_activated;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		// Assertions
		if (!collider.TryGetComponent<CharacterMotor>(out var character)) return;
		if (m_activated) return;

		m_activated = true;
		m_interactableFishermanDiving.IsValid = true;
		m_rseSetBubble.Call(CharacterType.FISHERMAN, 0);
		m_rseSetBubble.Call(CharacterType.LETTER, 0);
		m_rseSetBubble.Call(CharacterType.SINGER, 0);
		m_rseSetBubble.Call(CharacterType.GRANDPA, 2);
	}
}