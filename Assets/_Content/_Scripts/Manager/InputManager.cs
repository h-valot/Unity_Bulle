using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	[FoldoutGroup("Internal references")][SerializeField] private PlayerInput m_playerInput;

	[FoldoutGroup("Scriptable")][SerializeField] private RSE_Move m_rseMove;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_Interact m_rseInteract;

	private Vector2 m_move;

	private void OnApplicationFocus(bool hasFocus)
	{
		Cursor.lockState = hasFocus ? CursorLockMode.Locked : CursorLockMode.None;
	}

	public void OnMove(InputValue value)
	{
		m_move = value.Get<Vector2>();
		m_rseMove.Call(m_move);
	}

	public void OnInteract(InputValue value)
	{
		m_rseInteract.Call();
	}
}