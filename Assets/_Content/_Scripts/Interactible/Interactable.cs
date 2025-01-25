using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class Interactable : MonoBehaviour
{
    [Title("Tweakable values")]
    [SerializeField] private InteractType m_type;
    [ShowIf("m_type", InteractType.TRAVEL)]
    [SerializeField] private PanelType m_destination;
    [ShowIf("m_type", InteractType.SPAWN)]
    [SerializeField] private ItemType m_item;
    [ShowIf("m_type", InteractType.PLACE)]
    [SerializeField] private Transform m_placePosition;

	[SerializeField] private bool m_displayGizmos;

    [FoldoutGroup("Internal references")][SerializeField] private BoxCollider2D m_boxCollider2D;

	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentItem m_currentItem;

	public Action OnInteracted;
	public Action<CharacterInteract> OnInteractedWithRef;
	public bool IsValid { get; set; }

	private CharacterInteract m_characterInteract;

	/// <summary>
	/// Called when the interactable is getting interacted.
	/// </summary>
	public virtual void Interact()
	{
		OnInteracted?.Invoke();
		if (m_characterInteract) OnInteractedWithRef?.Invoke(m_characterInteract);
	}

	/// <summary>
	/// Called when a collider enters the interactable's collider. 
	/// If it's the character, add itseft to the character's interactable list.
	/// </summary>
	public virtual void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<CharacterInteract>(out var character))
		{
			m_characterInteract = character;
			character.Add(this);
		}
	}

	/// <summary>
	/// Called when a collider enters the interactable's collider. 
	/// If it's the character, remove itseft from the character's interactable list.
	/// </summary>
	public virtual void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.TryGetComponent<CharacterInteract>(out var character))
		{
			m_characterInteract = null;
			character.Remove(this);
		}
	}

#if UNITY_EDITOR

	private void OnDrawGizmos()
	{
		// Assertion
		if (!m_displayGizmos) return;

		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube(transform.position, m_boxCollider2D.size);
	}

#endif
}