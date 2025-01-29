using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
	[SerializeField] private GameObject m_graphicsParent;
	[SerializeField] private GameObject m_creditsParent;
	[SerializeField] private GameObject m_endScreenParent;

	[FoldoutGroup("Scriptable")][SerializeField] private RSO_LockCursor m_rsoLockCursor;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_DisplayEnd m_rseDisplayEnd;

	private void Start()
	{
		Show();
		m_creditsParent.SetActive(false);
		m_endScreenParent.SetActive(false);
	}

	private void OnEnable()
	{
		m_rseDisplayEnd.Action += ToggleEndScreen;
	}

	private void OnDisable()
	{
		m_rseDisplayEnd.Action -= ToggleEndScreen;
	}

	private void ToggleEndScreen(bool isEnabled)
	{
		m_endScreenParent.SetActive(isEnabled);
	}

	public void OnPressedLeave()
	{
		SceneManager.LoadScene("Game");
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