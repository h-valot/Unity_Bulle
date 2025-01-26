using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	[FoldoutGroup("Internal references")][SerializeField] private PlayerInput m_playerInput;

	[FoldoutGroup("Scriptable")][SerializeField] private RSE_Move m_rseMove;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_Interact m_rseInteract;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_Jump m_rseJump;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_LockCursor m_rsoLockCursor;

	private Vector2 m_move;
	private bool m_jump;
	private bool m_interact;
	private bool m_isFocused;

	private void Start()
	{
		m_jump = false;
		m_rseJump.Call(false);

		m_interact = false;
		m_rseInteract.Call(false);
	}

	private void OnEnable()
	{
		m_rsoLockCursor.OnChanged += UpdateCursorMode;
	}

	private void OnDisable()
	{
		m_rsoLockCursor.OnChanged -= UpdateCursorMode;
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		m_isFocused = hasFocus;
		UpdateCursorMode(hasFocus);
	}

	private void UpdateCursorMode(bool isEnable)
	{
		Cursor.lockState = m_rsoLockCursor.Value && m_isFocused
			? CursorLockMode.Locked
			: CursorLockMode.None;
	}

	public void OnMove(InputValue value)
	{
		m_move = value.Get<Vector2>();
		m_rseMove.Call(m_move);
	}

	public void OnInteract(InputValue value)
	{
		m_interact = value.isPressed;
		m_rseInteract.Call(m_interact);
	}

	public void OnJump(InputValue value)
	{
		m_jump = value.isPressed;
		m_rseJump.Call(m_jump);
	}
}