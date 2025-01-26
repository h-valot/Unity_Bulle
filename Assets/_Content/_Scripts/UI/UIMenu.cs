using Sirenix.OdinInspector;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
	[SerializeField] private GameObject m_graphicsParent;
	[SerializeField] private GameObject m_creditsParent;

	[FoldoutGroup("Scriptable")][SerializeField] private RSO_LockCursor m_rsoLockCursor;

	private void Start()
	{
		Show();
		m_creditsParent.SetActive(false);
	}

	public void OnPressedPlay()
	{
		Hide();
	}

	public void OnPressedCredits()
	{
		ToggleCredits();
	}

	private void ToggleCredits()
	{
		m_creditsParent.SetActive(!m_creditsParent.activeInHierarchy);
	}

	private void Hide()
	{
		m_graphicsParent.SetActive(false);
		m_rsoLockCursor.Value = true;
	}

	private void Show()
	{
		m_graphicsParent.SetActive(true);
		m_rsoLockCursor.Value = false;
	}
}