using UnityEngine;
using Sirenix.OdinInspector;
using System;
using System.Linq;

public class Interactable : MonoBehaviour
{
    [Title("Tweakable values")]
    [SerializeField] private InteractType m_type;
	// TRAVEL
    [ShowIf("m_type", InteractType.TRAVEL)][SerializeField] private PanelType m_travelTo;
    [ShowIf("m_type", InteractType.TRAVEL)][SerializeField] private Transform m_travelPosition;
	// SPAWN
    [ShowIf("m_type", InteractType.SPAWN)][SerializeField] private Interactable m_pfItemSpawned;
    [ShowIf("m_type", InteractType.SPAWN)][SerializeField] private Transform m_spawnPosition;
	// PLACE
	[ShowIf("m_type", InteractType.PLACE)][SerializeField] private ItemType m_itemRequiered;
    [ShowIf("m_type", InteractType.PLACE)][SerializeField] private Transform m_placePosition;
	// PICKUP
	[ShowIf("m_type", InteractType.PICKUP)][SerializeField] private ItemType m_pickupType;
	[ShowIf("m_type", InteractType.PICKUP), ShowIf("m_pickupType", ItemType.BOOT)][SerializeField] private Transform m_marketTravelPosition;


	[SerializeField] private bool m_displayGizmos;

    [FoldoutGroup("Internal references")][SerializeField] private BoxCollider2D m_boxCollider2D;

	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentItem m_rsoCurrentItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentPanel m_rsoCurrentPanel;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_PickupItem m_rsePickupItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_PlaceItem m_rsePlaceItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SetCharacterPosition m_rseSetCharacterPosition;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_ToggleDivingSuit m_rsoToggleDivingSuit;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_ToggleMitigedGravity m_rsoToggleMitigedGravity;

	public Action OnInteracted;
	public Action<CharacterInteract> OnInteractedWithRef;
	[HideInInspector] public bool IsValid = true;

	private CharacterInteract m_characterInteract;

	private void OnEnable()
	{
		if (m_type == InteractType.PICKUP) m_rsePlaceItem.Action += PlaceItem;
	}

	private void OnDisable()
	{
		if (m_type == InteractType.PICKUP) m_rsePlaceItem.Action -= PlaceItem;
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
			character.Remove(this);
			m_characterInteract = null;
		}
	}

	private void HandleInteraction()
	{
		switch(m_type)
		{
			case InteractType.TRAVEL:
				m_rsoCurrentPanel.Value = m_travelTo;
				m_rseSetCharacterPosition.Call(m_travelPosition.position);
				break;

			case InteractType.SPAWN:
				IsValid = false;
				Instantiate(m_pfItemSpawned, m_spawnPosition.position, Quaternion.identity);
				break;

			case InteractType.PLACE:
				if (m_rsoCurrentItem.Value == m_itemRequiered) 
				{
					IsValid = false;
					m_rsePlaceItem.Call(m_placePosition.position);
				}
				break;

			case InteractType.PICKUP:
				if (m_rsoCurrentItem.Value == ItemType.NONE) 
				{
					IsValid = false;
					m_rsoCurrentItem.Value = m_pickupType;
					m_rsePickupItem.Call(transform);
				}
				else if (m_rsoCurrentItem.Value == ItemType.DIVING_SUIT)
				{
					m_rsoToggleDivingSuit.Value = true;
				}
				else if (m_rsoCurrentItem.Value == ItemType.BOOT)
				{
					m_rsoToggleDivingSuit.Value = false;
					m_rsoCurrentPanel.Value = PanelType.MARKET;
					m_rseSetCharacterPosition.Call(m_marketTravelPosition.position);
				}
				else if (m_rsoCurrentItem.Value == ItemType.GRANNY)
				{
					// TODO Update Granny's amant dialogue
					m_rsoToggleMitigedGravity.Value = true;
				}
				break;
		}
	}

	private void PlaceItem(Vector3 destination)
	{
		// Assertion
		if (m_rsoCurrentItem.Value != m_pickupType) return;

		// TODO DOJump towards destination
		transform.position = destination;
		transform.parent = null;

		switch (m_pickupType)
		{
			case ItemType.BOOT:
				// TODO Update fisherman sprite (with boot in feet)
				// TODO Update fisherman dialogue 
				// TODO Enable interactable to spawn fish
				// TODO Destroy this game object
				break;

			case ItemType.FISH:
				// TODO Update door sprite
				// TODO Enable interactable travel to Rain
				break;

			case ItemType.KEY:
				// TODO Enable interactable travel to House
				break;

			case ItemType.LADDER:
				// TODO Instantiate ladder prefab
				// TODO Enable interactable travel to Rain-Balcony
				// TODO Destroy this game object
				break;

			case ItemType.PLANT:
				// TODO Instantiate plant prefab
				// TODO Enable interactable travel to Lighthouse Top
				// TODO Destroy this game object
				break;

			case ItemType.GRANNY:
				m_rsoToggleMitigedGravity.Value = false;
				// TODO Update Granny sprite
				// TODO Update Grandpa dialogue 
				// TODO Starts end game cinematic
				break;
		}

		m_rsoCurrentItem.Value = ItemType.NONE;
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