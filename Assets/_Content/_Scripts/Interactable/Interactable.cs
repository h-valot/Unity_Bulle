using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class Interactable : MonoBehaviour
{
    [Title("Tweakable values")]
    [SerializeField] private InteractType m_type;
	// TRAVEL
    [ShowIf("m_type", InteractType.TRAVEL)][SerializeField] private PanelType m_travelTo;
    [ShowIf("m_type", InteractType.TRAVEL)][SerializeField] private Transform m_travelPosition;
	// SPAWN
    [ShowIf("m_type", InteractType.SPAWN)][SerializeField] private GameObject m_pfItemSpawned;
    [ShowIf("m_type", InteractType.SPAWN)][SerializeField] private Transform m_spawnPosition;
	// PLACE
    [ShowIf("m_type", InteractType.PLACE)][SerializeField] private ItemType m_itemRecievedType;
    [ShowIf("m_type", InteractType.PLACE)][SerializeField] private GameObject m_pfItemRecieved;
    [ShowIf("m_type", InteractType.PLACE)][SerializeField] private Transform m_placePosition;
	// PICKUP
    [ShowIf("m_type", InteractType.PICKUP)][SerializeField] private ItemType m_pickupType;


	[SerializeField] private bool m_displayGizmos;

    [FoldoutGroup("Internal references")][SerializeField] private BoxCollider2D m_boxCollider2D;

	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentItem m_rsoCurrentItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentPanel m_rsoCurrentPanel;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_PickupItem m_rsePickupItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_PlaceItem m_rsePlaceItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SetCharacterPosition m_rseSetCharacterPosition;

	public Action OnInteracted;
	public Action<CharacterInteract> OnInteractedWithRef;
	public bool IsValid = true;

	private CharacterInteract m_characterInteract;

	private void OnEnable()
	{
		m_rsePlaceItem.Action += PlacePickup;
	}

	private void OnDisable()
	{
		m_rsePlaceItem.Action -= PlacePickup;
	}

	/// <summary>
	/// Called when the interactable is interacted with.
	/// </summary>
	public virtual void Interact()
	{
		OnInteracted?.Invoke();
		if (m_characterInteract) OnInteractedWithRef?.Invoke(m_characterInteract);
		HandleInteraction();
	}

	/// <summary>
	/// Called when a collider enters the interactable's collider. 
	/// If it's the character, add itseft to the character's interactable list.
	/// </summary>
	public virtual void OnTriggerEnter2D(Collider2D collider)
	{
		if (!IsValid) return;

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
		if (!IsValid) return;

		if (collider.TryGetComponent<CharacterInteract>(out var character))
		{
			m_characterInteract = null;
			character.Remove(this);
		}
	}

	private void HandleInteraction()
	{
		switch(m_type)
		{
			case InteractType.TRAVEL:
				m_rseSetCharacterPosition.Call(m_travelPosition.position);
				m_rsoCurrentPanel.Value = m_travelTo;
				break;

			case InteractType.SPAWN:
				IsValid = false;
				Interactable spawnedItem = Instantiate(m_pfItemSpawned, m_spawnPosition.position, Quaternion.identity).GetComponent<Interactable>();
				spawnedItem.IsValid = true;
				m_characterInteract.Remove(this);
				m_characterInteract = null;
				break;

			case InteractType.PLACE:
				if (m_rsoCurrentItem.Value == m_itemRecievedType) 
				{
					IsValid = false;
					m_rsePlaceItem.Call(m_placePosition.position);
					m_characterInteract.Remove(this);
					m_characterInteract = null;
				}
				break;

			case InteractType.PICKUP:
				if(m_rsoCurrentItem.Value != ItemType.NONE) return;
				m_rsePickupItem.Call(gameObject);
				IsValid = false;
				m_characterInteract = null;
				break;
		}
	}

	private void PlacePickup(Vector3 destination) 
	{
		if(m_rsoCurrentItem.Value == m_pickupType)
		{
			transform.parent = null;
			transform.position = destination;
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