using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public class Interactable : MonoBehaviour
{
	[SerializeField] private RSE_PlaySoundOfType m_rsePlaySoundOfType; 

    [Title("Tweakable values")]
	[OnValueChanged("ResetSerializedTypes")]
    [SerializeField] private InteractType m_type;

	// TRAVEL
	[ShowIf("m_type", InteractType.TRAVEL)][SerializeField] private PanelType m_travelTo;
    [ShowIf("m_type", InteractType.TRAVEL)][SerializeField] private Transform m_travelPosition;


	// SPAWN
    [ShowIf("m_type", InteractType.SPAWN)][SerializeField] private Interactable m_pfItemSpawned;
    [ShowIf("m_type", InteractType.SPAWN)][SerializeField] private Transform m_spawnPosition;

	[ShowIf("@m_pfItemSpawned != null && m_pfItemSpawned.PickupType == ItemType.DIVING_SUIT")]
	[SerializeField] private Interactable m_interactableFishermanBoot;


	// PICKUP
	[ShowIf("m_type", InteractType.PICKUP)][SerializeField] public ItemType PickupType;

	[ShowIf("PickupType", ItemType.BOOT)][SerializeField] private Transform m_marketTravelPosition;

	[ShowIf("PickupType", ItemType.DIVING_SUIT)][SerializeField] private Vector2 m_abyssTravelPosition;

	[ShowIf("PickupType", ItemType.LADDER)][SerializeField] private GameObject m_pfLadderLong;

	[ShowIf("PickupType", ItemType.PLANT)][SerializeField] private GameObject m_pfPlantLong;

	[ShowIf("PickupType", ItemType.GRANNY)][SerializeField] private Interactable m_interactableGrandpaGranny;


	// PLACE
	[ShowIf("m_type", InteractType.PLACE)][SerializeField] private Transform m_placePosition;
	[ShowIf("m_type", InteractType.PLACE)][SerializeField] private ItemType m_itemRequiered;

	[ShowIf("m_itemRequiered", ItemType.FISH)][SerializeField] private Interactable m_rainTravel;

	[ShowIf("m_itemRequiered", ItemType.BOOT)][SerializeField] private GameObject m_pfFish;
	[ShowIf("m_itemRequiered", ItemType.BOOT)][SerializeField] private Transform m_fishSpawnPosition;

	[ShowIf("m_itemRequiered", ItemType.KEY)][SerializeField] private Interactable m_TravelToHouse;
	[ShowIf("m_itemRequiered", ItemType.KEY)][SerializeField] private Transform m_waypointToHouse;

	[ShowIf("m_itemRequiered", ItemType.LADDER)][SerializeField] private Interactable m_balconyTravel;

	[ShowIf("m_itemRequiered", ItemType.PLANT)][SerializeField] private Interactable m_lightTopTravel;

	[ShowIf("m_itemRequiered", ItemType.GRANNY)][SerializeField] private Camera m_camera;
	[ShowIf("m_itemRequiered", ItemType.GRANNY)][SerializeField] private Transform m_cameraCinematicTarget;


	[FoldoutGroup("Internal references")][SerializeField] private BoxCollider2D m_boxCollider2D;

	[FoldoutGroup("Scriptable")][SerializeField] private SSO_Camera m_ssoCamera;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentItem m_rsoCurrentItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentPanel m_rsoCurrentPanel;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_PickupItem m_rsePickupItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_PlaceItem m_rsePlaceItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SetCharacterPosition m_rseSetCharacterPosition;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_ToggleDivingSuit m_rsoToggleDivingSuit;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_ToggleMitigedGravity m_rsoToggleMitigedGravity;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SetBubble m_rseSetBubble;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_DisplayEnd m_rseDisplayEnd;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_LockInputs m_rsoLockInputs;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SetCameraLerp m_rseSetCameraLerp;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_LockCursor m_rsoLockCursor;

	[SerializeField] public bool IsValid = true;
	[SerializeField] public bool IsInteractive = true; 

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

	public bool CanInteract()
	{
		// Assertion
		if (!IsInteractive) return false;

		bool isInteractive = false;

		switch (m_type)
		{
			case InteractType.TRAVEL:
				isInteractive = true;
				break;

			case InteractType.SPAWN:
				// Hide contextual hint
				break;

			case InteractType.PLACE:
				isInteractive = m_rsoCurrentItem.Value == m_itemRequiered;
				break;

			case InteractType.PICKUP:
				isInteractive = m_rsoCurrentItem.Value == ItemType.NONE;
				break;
		}

		return isInteractive;
	}

	/// <summary>
	/// Called when the interactable is interacted with.
	/// </summary>
	public virtual void Interact()
	{
		HandleInteraction();
	}

	/// <summary>
	/// Called when a collider enters the interactable's collider. 
	/// If it's the character, add itseft to the character's interactable list.
	/// </summary>
	public virtual void OnTriggerEnter2D(Collider2D collider)
	{
		if (!collider.TryGetComponent<CharacterInteract>(out var character)) return;

		character.Add(this);
	}

	/// <summary>
	/// Called when a collider enters the interactable's collider. 
	/// If it's the character, remove itseft from the character's interactable list.
	/// </summary>
	public virtual void OnTriggerExit2D(Collider2D collider)
	{
		if (!collider.TryGetComponent<CharacterInteract>(out var character)) return;

		character.Remove(this);
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

				Interactable spawnInteractable = Instantiate(m_pfItemSpawned, transform.position, Quaternion.identity);
				float InteractableScale = spawnInteractable.transform.localScale.x;
				spawnInteractable.transform.localScale = Vector3.zero;
				spawnInteractable.transform.DOScale(InteractableScale, 1f).SetEase(Ease.OutBounce).SetLink(this.gameObject);
				spawnInteractable.transform.DOJump(m_spawnPosition.position, 1f, 1, 0.5f).SetEase(Ease.Linear).SetLink(this.gameObject).OnComplete(() => { m_rsePlaySoundOfType.Call(m_type, m_pfItemSpawned.PickupType);});

                if (m_pfItemSpawned.PickupType == ItemType.DIVING_SUIT)
				{
					// Enable fisherman interactable place
					m_interactableFishermanBoot.IsValid = true;
				}
				break;

			case InteractType.PLACE:
				if (m_rsoCurrentItem.Value == m_itemRequiered) 
				{
					if (m_itemRequiered == ItemType.KEY)
					{
						// TODO Animate door
						m_TravelToHouse.IsValid = true;
						m_rsoCurrentPanel.Value = PanelType.HOUSE;
						m_rseSetCharacterPosition.Call(m_waypointToHouse.position);
					}
					else if (m_itemRequiered == ItemType.BOOT)
					{
						m_rseSetBubble.Call(CharacterType.FISHERMAN, 1);

						GameObject fish = Instantiate(m_pfFish, transform.position, Quaternion.identity);
						float fishScale = fish.transform.localScale.x;
						fish.transform.localScale = Vector3.zero;
						fish.transform.DOScale(fishScale, 1f).SetEase(Ease.OutBounce).SetLink(gameObject);
						fish.transform.DOJump(m_fishSpawnPosition.position, 1f, 1, 0.5f).SetEase(Ease.Linear).SetLink(gameObject).OnComplete(() => { m_rsePlaySoundOfType.Call(m_type, ItemType.FISH); });
					}
					else if (m_itemRequiered == ItemType.FISH)
					{
						// TODO Animate door
						m_rainTravel.IsValid = true;
						m_rseSetBubble.Call(CharacterType.FISHMONGER_SHORTKING, 1);
						m_rseSetBubble.Call(CharacterType.FISHMONGER_TALL, 1);
					}
					else if (m_itemRequiered == ItemType.LADDER)
					{
						m_balconyTravel.IsValid = true;
					}
					else if (m_itemRequiered == ItemType.PLANT)
					{
						m_lightTopTravel.IsValid = true;
					}
					else if (m_itemRequiered == ItemType.GRANNY)
					{
						PlayEndCinematic();
					}

					IsValid = false;
					m_rsePlaceItem.Call(m_placePosition.position);
				}
				break;

			case InteractType.PICKUP:
				
                m_rsePlaySoundOfType.Call(InteractType.PICKUP, ItemType.NONE);

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

		transform.DOJump(destination, 1f, 1, 0.5f).SetEase(Ease.Linear).SetLink(gameObject);
		m_rsePlaySoundOfType.Call(InteractType.PLACE, PickupType);
		transform.parent = null;

		switch (PickupType)
		{
			case ItemType.BOOT:
				Destroy(gameObject);
				break;

			case ItemType.KEY:
				Destroy(gameObject);
				break;

			case ItemType.FISH:
				Destroy(gameObject);
				break;

			case ItemType.LADDER:
				Instantiate(m_pfLadderLong, transform.position, Quaternion.identity);
				Destroy(gameObject);
				break;

			case ItemType.PLANT:
				Instantiate(m_pfPlantLong, transform.position, Quaternion.identity);
				Destroy(gameObject);
				break;

			case ItemType.GRANNY:
				m_rsoToggleMitigedGravity.Value = false;				
				break;
		}
	}

	private void PlayEndCinematic()
	{
		m_rsoLockInputs.Value = true;
		m_rseSetCameraLerp.Call(false);
		m_camera.transform.DOMove(m_cameraCinematicTarget.position, m_ssoCamera.EndTranslationDuration);
		m_camera.DOOrthoSize(m_ssoCamera.EndOrthoSize, m_ssoCamera.EndTranslationDuration).OnComplete(() => 
		{ 
			m_rseDisplayEnd.Call(true); 
			m_rsoLockCursor.Value = false;
		});
	}

#if UNITY_EDITOR

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawWireCube(transform.position, transform.localScale);
	}

#endif

}