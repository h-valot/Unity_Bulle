using UnityEngine;
using Sirenix.OdinInspector;
using System;

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


	// PICKUP
    [ShowIf("m_type", InteractType.PICKUP)][SerializeField] private ItemType m_pickupType;
	[ShowIf("m_pickupType", ItemType.BOOT)][SerializeField] private Transform m_marketTravelPosition;
	[ShowIf("m_pickupType", ItemType.DIVING_SUIT)][SerializeField] private Vector2 m_abyssTravelPosition;


	// PLACE
	[ShowIf("m_type", InteractType.PLACE)][SerializeField] private Transform m_placePosition;
	[ShowIf("m_type", InteractType.PLACE)][SerializeField] private ItemType m_itemRequiered;

	[ShowIf("m_itemRequiered", ItemType.BOOT)][SerializeField] private Interactable m_fishSpawner;
	[ShowIf("m_itemRequiered", ItemType.BOOT)][SerializeField] private Sprite m_fishermanSprite;
	[ShowIf("m_itemRequiered", ItemType.BOOT)][SerializeField] private SpriteRenderer m_fishermanRenderer;
	[ShowIf("m_itemRequiered", ItemType.BOOT)][SerializeField] private Sprite m_fishermanFishBubble;
	[ShowIf("m_itemRequiered", ItemType.BOOT)][SerializeField] private SpriteRenderer m_fishBubbleRenderer;

	[ShowIf("m_itemRequiered", ItemType.FISH)][SerializeField] private Interactable m_rainTravel;
	[ShowIf("m_itemRequiered", ItemType.FISH)][SerializeField] private SpriteRenderer m_doorRenderer;
	[ShowIf("m_itemRequiered", ItemType.FISH)][SerializeField] private Sprite m_doorSprite;

	[ShowIf("m_itemRequiered", ItemType.KEY)][SerializeField] private Interactable m_houseTravel;
	[ShowIf("m_itemRequiered", ItemType.KEY)][SerializeField] private SpriteRenderer m_houseDoorRenderer;
	[ShowIf("m_itemRequiered", ItemType.KEY)][SerializeField] private Sprite m_houseDoorSprite;

	[ShowIf("m_itemRequiered", ItemType.LADDER)][SerializeField] private Interactable m_balconyTravel;
	[ShowIf("m_itemRequiered", ItemType.LADDER)][SerializeField] private GameObject m_pfLadderLong;

	[ShowIf("m_itemRequiered", ItemType.PLANT)][SerializeField] private Interactable m_lightTopTravel;
	[ShowIf("m_itemRequiered", ItemType.PLANT)][SerializeField] private GameObject m_pfPlantLong;

	[ShowIf("m_itemRequiered", ItemType.GRANNY)][SerializeField] private Sprite m_grannySprite;
	[ShowIf("m_itemRequiered", ItemType.GRANNY)][SerializeField] private SpriteRenderer m_grannyRenderer;
	[ShowIf("m_itemRequiered", ItemType.GRANNY)][SerializeField] private Sprite m_grandpaBubble;
	[ShowIf("m_itemRequiered", ItemType.GRANNY)][SerializeField] private SpriteRenderer m_grandpaBubbleRenderer;


    [FoldoutGroup("Internal references")][SerializeField] private BoxCollider2D m_boxCollider2D;

	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentItem m_rsoCurrentItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentPanel m_rsoCurrentPanel;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_PickupItem m_rsePickupItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_PlaceItem m_rsePlaceItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SetCharacterPosition m_rseSetCharacterPosition;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_ToggleDivingSuit m_rsoToggleDivingSuit;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_ToggleMitigedGravity m_rsoToggleMitigedGravity;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SetBubble m_rseSetBubble;

	[SerializeField] private bool m_displayGizmos;

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

	private void ResetSerializedTypes()
	{
		m_pickupType = ItemType.NONE;
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
				break;

			case InteractType.PLACE:
				if (m_rsoCurrentItem.Value == m_itemRequiered) 
				{
					IsValid = false;
					m_rsePlaceItem.Call(m_placePosition.position);
				}
				break;

			case InteractType.PICKUP:
				if (m_rsoCurrentItem.Value == ItemType.NONE && m_pickupType == ItemType.DIVING_SUIT)
				{
					m_rsoToggleDivingSuit.Value = true;
					m_rsoCurrentPanel.Value = PanelType.ABYSS;
					m_rseSetCharacterPosition.Call(m_abyssTravelPosition);
				}
				else if (m_rsoCurrentItem.Value == ItemType.NONE && m_pickupType == ItemType.BOOT)
				{
					m_rsoToggleDivingSuit.Value = false;
					m_rsoCurrentPanel.Value = PanelType.MARKET;
					m_rseSetCharacterPosition.Call(m_marketTravelPosition.position);
				}
				else if (m_rsoCurrentItem.Value == ItemType.NONE && m_pickupType == ItemType.GRANNY)
				{
					m_rseSetBubble.Call(CharacterType.LOVER, 1);
					m_rsoToggleMitigedGravity.Value = true;
				}
				else if (m_rsoCurrentItem.Value == ItemType.NONE)
				{
					IsValid = false;
					m_rsoCurrentItem.Value = m_pickupType;
					m_rsePickupItem.Call(transform);
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
				m_fishBubbleRenderer.sprite = m_fishermanSprite;
				m_fishBubbleRenderer.sprite = m_fishermanFishBubble;
				m_fishSpawner.gameObject.SetActive(true);
				Destroy(gameObject);
				break;

			case ItemType.FISH:
				m_doorRenderer.sprite = m_doorSprite;
				m_rainTravel.gameObject.SetActive(true);
				break;

			case ItemType.KEY:
				m_houseDoorRenderer.sprite = m_houseDoorSprite;
				m_houseTravel.gameObject.SetActive(true);
				Destroy(gameObject);
				break;

			case ItemType.LADDER:
				Instantiate(m_pfLadderLong, transform.position, Quaternion.identity);
				// TODO Temp ? Maybe only activate on complete of ladder prefab animation
				m_balconyTravel.gameObject.SetActive(true);
				Destroy(gameObject);
				break;

			case ItemType.PLANT:
				Instantiate(m_pfPlantLong, transform.position, Quaternion.identity);
				// TODO Temp ? Maybe only activate on complete of plant prefab animation
				m_lightTopTravel.gameObject.SetActive(true);
				Destroy(gameObject);
				break;

			case ItemType.GRANNY:
				m_rsoToggleMitigedGravity.Value = false;
				m_grannyRenderer.sprite = m_grannySprite;
				m_grandpaBubbleRenderer.sprite = m_balconyTravel.m_grandpaBubble;
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