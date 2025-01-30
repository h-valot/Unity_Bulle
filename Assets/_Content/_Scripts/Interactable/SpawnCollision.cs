using Sirenix.OdinInspector;
using UnityEngine;

public class SpawnCollision : MonoBehaviour
{
	[FoldoutGroup("Tweakable values")][SerializeField] private ParticleSystem m_pfVfxPopBubble;

	[FoldoutGroup("External references")][SerializeField] private Bubble m_bubble;

	[FoldoutGroup("Internal references")][SerializeField] private Interactable m_interactable;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Assertion
		if (!collision.gameObject.TryGetComponent<CharacterMotor>(out var character)) return;
		if (!m_interactable.IsValid) return;

		m_interactable.Interact();
		m_bubble.AnimateCollision();
		Instantiate(m_pfVfxPopBubble, transform.position, Quaternion.identity);
	}
}