using Sirenix.OdinInspector;
using UnityEngine;

public class LetterBoxTrigger : MonoBehaviour
{
	[Title("References")]
	[Required][SerializeField] private Interactable m_interactableFishermanDiving;

	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SetBubble m_rseSetBubble;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		// Assertion
		if (!collider.TryGetComponent<CharacterMotor>(out var character)) return;

		m_rseSetBubble.Call(CharacterType.FISHERMAN, 0);
		m_rseSetBubble.Call(CharacterType.FISHMONGER_SHORTKING, 0);
		m_rseSetBubble.Call(CharacterType.FISHMONGER_TALL, 0);
		m_rseSetBubble.Call(CharacterType.LETTER, 0);
		m_rseSetBubble.Call(CharacterType.SINGER, 0);
		m_rseSetBubble.Call(CharacterType.GRANDPA, 1);

		m_interactableFishermanDiving.IsValid = true;
	}
}