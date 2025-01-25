using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterGraphics : MonoBehaviour
{
	[FoldoutGroup("Internal references")][SerializeField] private GameObject m_divingSuit;

	[FoldoutGroup("Scriptable")][SerializeField] private RSO_ToggleDivingSuit m_rsoToggleDivingSuit;

	private void Start()
	{
		m_rsoToggleDivingSuit.Value = false;
	}

	private void OnEnable()
	{
		m_rsoToggleDivingSuit.OnChanged += ToggleDivingSuit;
	}

	private void OnDisable()
	{
		m_rsoToggleDivingSuit.OnChanged -= ToggleDivingSuit;
	}

	private void ToggleDivingSuit(bool isEnabled)
	{
		m_divingSuit.SetActive(isEnabled);
	}
}
