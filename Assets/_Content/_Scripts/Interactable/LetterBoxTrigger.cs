using Sirenix.OdinInspector;
using UnityEngine;

public class LetterBoxTrigger : MonoBehaviour
{
	[Title("References")]
	[Required][SerializeField] private GameObject m_letterBubble;
	[Required][SerializeField] private GameObject m_fishermanDivingSuitBubble;
	[Required][SerializeField] private Interactable m_fishermanDivingSuitInteractable;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		// Assertion
		if (!collider.TryGetComponent<CharacterMotor>(out var character)) return;

		m_letterBubble.SetActive(true);
		m_fishermanDivingSuitBubble.SetActive(true);
		m_fishermanDivingSuitInteractable.gameObject.SetActive(true);
	}
}