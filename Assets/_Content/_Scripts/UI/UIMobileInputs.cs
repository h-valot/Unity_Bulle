using Sirenix.OdinInspector;
using UnityEngine;

public class UIMobileInputs : MonoBehaviour
{
	[FoldoutGroup("Internal references")][SerializeField] private GameObject m_graphicsParent;

	[FoldoutGroup("Scriptable")][SerializeField] private RSE_Move m_rseMove;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_Interact m_rseInteract;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_Jump m_rseJump;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SetMobileInputs m_rseSetMobileInputs;

	private void Start()
	{
		m_graphicsParent.SetActive(false);
	}

	private void OnEnable()
	{
		m_rseSetMobileInputs.Action += Setup;
	}

	private void OnDisable()
	{
		m_rseSetMobileInputs.Action -= Setup;
	}

	private void Setup(bool isEnabled)
	{
		if (!PlatformManagement.s_isMobile)
		{
			m_graphicsParent.SetActive(false);
			return;
		}

		m_graphicsParent.SetActive(isEnabled);
	}

	public void OnJumpDown() => m_rseJump.Call(true);
	public void OnJumpUp() => m_rseJump.Call(false);

	public void OnInteractDown() => m_rseInteract.Call(true);
	public void OnInteractUp() => m_rseInteract.Call(false);

	public void OnRightDown() => m_rseMove.Call(new Vector2(1f, 0));
	public void OnRightUp() => m_rseMove.Call(Vector2.zero);

	public void OnLeftDown() => m_rseMove.Call(new Vector2(-1f, 0));
	public void OnLeftUp() => m_rseMove.Call(Vector2.zero);
}