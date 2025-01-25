using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
	[FoldoutGroup("Internal references")][SerializeField] private Rigidbody2D m_rigidbody2D;

	[FoldoutGroup("Scriptable")][SerializeField] private SSO_Character m_ssoCharacter;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentPosition m_rsoCurrentPosition;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_Move m_rseMove;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_Jump m_rseJump;

	private Vector2 m_moveInput;
	private bool m_isGrounded;
	private float m_jumpTimer;

	private bool IsJumping => m_jumpTimer > 0f;

	private void OnEnable()
	{
		m_rseMove.Action += UpdateMoveInput;
		m_rseJump.Action += UpdateJumpInput;
	}

	private void OnDisable()
	{
		m_rseMove.Action -= UpdateMoveInput;
		m_rseJump.Action -= UpdateJumpInput;
	}

	private void FixedUpdate()
	{
		CheckGround();

		HandleJump();
		HandleMove();

		m_rsoCurrentPosition.Value = m_rigidbody2D.position;
	}

	private void UpdateMoveInput(Vector2 moveInput)
	{
		m_moveInput = new Vector2(moveInput.x, 0f);
	}

	private void UpdateJumpInput(bool isPressed)
	{
		if (!m_isGrounded) return;
		if (!isPressed) return;

		m_jumpTimer = m_ssoCharacter.JumpDuration;
	}

	private void CheckGround()
	{
		m_isGrounded = Physics2D.Raycast(
			transform.position, 
			Vector2.down,
			m_ssoCharacter.GroundLength,
			m_ssoCharacter.GroundLayerToInclude
		);
	}

	private void HandleJump()
	{
		if (!IsJumping) return;

		m_jumpTimer -= Time.fixedDeltaTime;
	}

	private void HandleMove()
	{
		Vector2 gravity = !m_isGrounded 
			? Vector2.down * m_ssoCharacter.GravityForce * m_ssoCharacter.GravityScalar * Time.fixedDeltaTime 
			: Vector2.zero;
		
		Vector2 input = m_moveInput != Vector2.zero 
			? m_moveInput * m_ssoCharacter.Speed * Time.fixedDeltaTime 
			: Vector2.zero;
		
		Vector2 jump = IsJumping
			? Vector2.up * m_ssoCharacter.JumpForce * (m_jumpTimer / m_ssoCharacter.JumpDuration) * Time.fixedDeltaTime 
			: Vector2.zero;

		m_rigidbody2D.MovePosition(m_rigidbody2D.position + gravity + input + jump);
	}
}