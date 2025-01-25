using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
	[FoldoutGroup("Internal references")][SerializeField] private Rigidbody2D m_rigidbody2D;
	[FoldoutGroup("Internal references")] public GameObject ItemAnchor;

	[FoldoutGroup("Scriptable")][SerializeField] private SSO_Character m_ssoCharacter;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_Move m_rseMove;

	// PRIVATE VARIABLES
	// Interact


	private Vector2 m_moveInput;

	private void OnEnable()
	{
		m_rseMove.Action += UpdateMoveInput;
	}

	private void OnDisable()
	{
		m_rseMove.Action += UpdateMoveInput;
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void UpdateMoveInput(Vector2 moveInput)
	{
		m_moveInput = moveInput;
	}

	private void Move()
	{
		if (m_moveInput == Vector2.zero) return;

		m_rigidbody2D.MovePosition(m_rigidbody2D.position + m_moveInput * m_ssoCharacter.Speed * Time.fixedDeltaTime);
	}
}