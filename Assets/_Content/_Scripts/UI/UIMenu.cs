using Sirenix.OdinInspector;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
	[SerializeField] private GameObject m_graphicsParent;

	[FoldoutGroup("Scriptable")][SerializeField] private RSO_LockCursor m_rsoLockCursor;

	private void Start()
	{
		Show();
	}

	public void OnPressedPlay()
	{
		Hide();
	}

	public void OnPressedCredits()
	{
		// TODO Display credits
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