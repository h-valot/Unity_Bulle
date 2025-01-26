using UnityEngine;
using Sirenix.OdinInspector;
using System;
using Unity.VisualScripting;

public class Interactable : MonoBehaviour
{
    [Title("Tweakable values")]
	[OnValueChanged("ResetSerializedTypes")]
    [SerializeField] private InteractType m_type;

	// TRAVEL
    [ShowIf("m_type", InteractType.TRAVEL)][SerializeField] private PanelType m_travelTo;
    [ShowIf("m_type", InteractType.TRAVEL)][SerializeField] private Transform m_travelPosition;


	// SPAWN
    [ShowIf("m_type", InteractType.SPAWN)][SerializeField] private Interactable m_pfItemSpawned;
    [ShowIf("m_type", InteractType.SPAWN)][SerializeField] private Transform m_spawnPosition;
	[ShowIf("@m_pfItemSpawned.PickupType == ItemType.DIVING_SUIT")][SerializeField] private Interactable m_interactableFishermanBoot;


	// PICKUP
	[ShowIf("m_type", InteractType.PICKUP)][SerializeField] public ItemType PickupType;

	[ShowIf("PickupType", ItemType.BOOT)][SerializeField] private Transform m_marketTravelPosition;
	[ShowIf("PickupType", ItemType.BOOT)][SerializeField] private Interactable m_interactableFishermanFish;

	[ShowIf("PickupType", ItemType.DIVING_SUIT)][SerializeField] private Vector2 m_abyssTravelPosition;

	[ShowIf("PickupType", ItemType.LADDER)][SerializeField] private GameObject m_pfLadderLong;

	[ShowIf("PickupType", ItemType.PLANT)][SerializeField] private GameObject m_pfPlantLong;

	[ShowIf("PickupType", ItemType.GRANNY)][SerializeField] private Interactable m_interactableGrandpaGranny;


	// PLACE
	[ShowIf("m_type", InteractType.PLACE)][SerializeField] private Transform m_placePosition;
	[ShowIf("m_type", InteractType.PLACE)][SerializeField] private ItemType m_itemRequiered;

	[ShowIf("m_itemRequiered", ItemType.FISH)][SerializeField] private Interactable m_rainTravel;
	[ShowIf("m_itemRequiered", ItemType.FISH)][SerializeField] private GameObject m_rainDoor;

	[ShowIf("m_itemRequiered", ItemType.KEY)][SerializeField] private Interactable m_TravelToHouse;
	[ShowIf("m_itemRequiered", ItemType.KEY)][SerializeField] private Transform m_waypointToHouse;

	[ShowIf("m_itemRequiered", ItemType.LADDER)][SerializeField] private Interactable m_balconyTravel;

	[ShowIf("m_itemRequiered", ItemType.PLANT)][SerializeField] private Interactable m_lightTopTravel;


    [FoldoutGroup("Internal references")][SerializeField] private BoxCollider2D m_boxCollider2D;

	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentItem m_rsoCurrentItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentPanel m_rsoCurrentPanel;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_PickupItem m_rsePickupItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_PlaceItem m_rsePlaceItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SetCharacterPosition m_rseSetCharacterPosition;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_ToggleDivingSuit m_rsoToggleDivingSuit;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_ToggleMitigedGravity m_rsoToggleMitigedGravity;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SetBubble m_rseSetBubble;

	[SerializeField] public bool IsValid = true;
	[SerializeField] private bool m_displayGizmos;

	public Action OnInteracted;
	public Action<CharacterInteract> OnInteractedWithRef;

	private CharacterInteract m_characterInteract;

	private void OnEnable()
	{
		if (m_type == InteractType.PICKUP) m_rsePlaceItem.Action += PlaceItem;
	}

	private void OnDisable()
	{
		if (m_type == InteractType.PICKUP) m_rsePlaceItem.Action -= PlaceItem;
	}

	private void ResetSerializedTypes()
	{
		PickupType = ItemType.NONE;
		m_itemRequiered = ItemType.NONE;
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

				if (m_pfItemSpawned.PickupType == ItemType.DIVING_SUIT)
				{
					// Enable fisherman interactable place
					m_interactableFishermanBoot.IsValid = true;
				}
				break;

			case InteractType.PLACE:
				if (m_rsoCurrentItem.Value == m_itemRequiered) 
				{
					if (m_itemRequiered == ItemType.PLANT)
					{
						m_lightTopTravel.IsValid = true;
					}
					else if (m_itemRequiered == ItemType.LADDER)
					{
						m_balconyTravel.IsValid = true;
					}
					else if (m_itemRequiered == ItemType.FISH)
					{
						// TODO Animate door
						m_rainDoor.SetActive(true);
						m_rainTravel.IsValid = true;
					}
					else if (m_itemRequiered == ItemType.KEY)
					{
						// TODO Animate door
						m_TravelToHouse.IsValid = true;
						m_rsoCurrentPanel.Value = PanelType.HOUSE;
						m_rseSetCharacterPosition.Call(m_waypointToHouse.position);
					}

					IsValid = false;
					m_rsePlaceItem.Call(m_placePosition.position);
				}
				break;

			case InteractType.PICKUP:
				if (m_rsoCurrentItem.Value == ItemType.NONE 
				&& PickupType == ItemType.DIVING_SUIT)
				{
					m_rsoToggleDivingSuit.Value = true;
					m_rsoCurrentPanel.Value = PanelType.ABYSS;
					m_rseSetCharacterPosition.Call(m_abyssTravelPosition);

					Destroy(gameObject);
				}
				else if (m_rsoCurrentItem.Value == ItemType.NONE 
				&& PickupType == ItemType.BOOT)
				{
					m_rsoToggleDivingSuit.Value = false;
					m_rsoCurrentPanel.Value = PanelType.MARKET;
					m_rseSetCharacterPosition.Call(m_marketTravelPosition.position);

					IsValid = false;
					m_rsoCurrentItem.Value = PickupType;
					m_rsePickupItem.Call(transform);
				}
				else if (m_rsoCurrentItem.Value == ItemType.NONE
				&& PickupType == ItemType.GRANNY)
				{
					m_rsoToggleMitigedGravity.Value = true;
					m_rseSetBubble.Call(CharacterType.LOVER, 1);
					m_interactableGrandpaGranny.IsValid = true;

					IsValid = false;
					m_rsoCurrentItem.Value = PickupType;
					m_rsePickupItem.Call(transform);
				}
				else if (m_rsoCurrentItem.Value == ItemType.NONE)
				{
					IsValid = false;
					m_rsoCurrentItem.Value = PickupType;
					m_rsePickupItem.Call(transform);
				}
				break;
		}
	}

	private void PlaceItem(Vector3 destination)
	{
		// Assertion
		if (m_rsoCurrentItem.Value != PickupType) return;

		m_rsoCurrentItem.Value = ItemType.NONE;

		// TODO DOJump towards destination
		transform.position = destination;
		transform.parent = null;

		switch (PickupType)
		{
			case ItemType.BOOT:
				m_rseSetBubble.Call(CharacterType.FISHERMAN, 1);
				m_interactableFishermanFish.IsValid = true;
				Destroy(gameObject);
				break;

			case ItemType.KEY:
				Destroy(gameObject);
				break;

			case ItemType.LADDER:
				Instantiate(m_pfLadderLong, transform.position, Quaternion.identity);
				// TODO Temp ? Maybe only activate on complete of ladder prefab animation
				Destroy(gameObject);
				break;

			case ItemType.PLANT:
				Instantiate(m_pfPlantLong, transform.position, Quaternion.identity);
				// TODO Temp ? Maybe only activate on complete of plant prefab animation
				Destroy(gameObject);
				break;

			case ItemType.GRANNY:
				m_rsoToggleMitigedGravity.Value = false;
				// TODO Starts end game cinematic
				print("the end");
				break;
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