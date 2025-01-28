using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterInteract : MonoBehaviour
{
	[FoldoutGroup("Internal references")][SerializeField] private GameObject m_contextual;

	[FoldoutGroup("Scriptable")][SerializeField] private SSO_Character m_characterConfig;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_Interact m_rseInteract;

	public List<Interactable> m_interactables = new List<Interactable>();

	private void OnEnable()
	{
		m_rseInteract.Action += Interact;
	}

	private void OnDisable()
	{
		m_rseInteract.Action -= Interact;
	}

	private void Update()
	{
		m_contextual.SetActive(m_interactables.Any(c => c.CanInteract()));
	}

	private void Interact(bool isEnabled)
	{
		// Assertion
		if (!isEnabled) return;
		if (m_interactables.Count <= 0) return;

		m_interactables.FirstOrDefault(i => i.IsValid)?.Interact();
	}

	/// <summary>
	/// Add the given interactable into the interactable list.
	/// </summary>
	public void Add(Interactable interactable)
	{
		m_interactables.Add(interactable);
	}

	/// <summary>
	/// Remove the given interactable from the interactable list.
	/// The character will no longer be able to interact with it.
	/// </summary>
	public void Remove(Interactable interactable, bool doRecycle = false)
	{
		m_interactables.Remove(interactable);
	}
}