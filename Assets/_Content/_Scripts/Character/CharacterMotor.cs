using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
	[FoldoutGroup("Internal references")][SerializeField] private Rigidbody2D m_rigidbody2D;
	[FoldoutGroup("Internal references")][SerializeField] private Transform m_itemAnchor;

	[FoldoutGroup("Scriptable")][SerializeField] private SSO_Character m_ssoCharacter;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_Move m_rseMove;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_Jump m_rseJump;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SetCharacterPosition m_rseSetCharacterPosition;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_PickupItem m_rsePickupItem;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentPosition m_rsoCurrentPosition;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_ToggleMitigedGravity m_rsoToggleMitigedGravity;

	private Vector2 m_moveInput;
	private bool m_isGrounded;
	private float m_jumpTimer;
	private float m_gravityScalar;
	private float m_speedScalar;
	private bool m_isMitiged;

	private bool IsJumping => m_jumpTimer > 0f;

	private void OnEnable()
	{
		m_rseSetCharacterPosition.Action += SetCharacterPosition;
		m_rsePickupItem.Action += PickUpItem;
		m_rsoToggleMitigedGravity.OnChanged += ToggleMitigedGravity;

		m_rseMove.Action += UpdateMoveInput;
		m_rseJump.Action += UpdateJumpInput;
	}

	private void OnDisable()
	{
		m_rseSetCharacterPosition.Action -= SetCharacterPosition;
		m_rsePickupItem.Action -= PickUpItem;
		m_rsoToggleMitigedGravity.OnChanged -= ToggleMitigedGravity;

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

	private void ToggleMitigedGravity(bool isEnabled)
	{
		m_isMitiged = isEnabled;
	}

	private void SetCharacterPosition(Vector2 position)
	{
		m_rigidbody2D.velocity = Vector2.zero;
		m_rigidbody2D.position = position;
		m_rsoCurrentPosition.Value = transform.position;
	}

	private void PickUpItem(Transform pickupTransform)
	{
		pickupTransform.SetParent(m_itemAnchor);
		pickupTransform.position = m_itemAnchor.position;
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
		m_gravityScalar = m_isMitiged && !m_isGrounded ? m_ssoCharacter.GravityMitigedScalar : m_ssoCharacter.GravityScalar;
		Vector2 gravity = !m_isGrounded 
			? Vector2.down * m_ssoCharacter.GravityForce * m_gravityScalar * Time.fixedDeltaTime 
			: Vector2.zero;
		
		m_speedScalar = m_isMitiged && !m_isGrounded ? m_ssoCharacter.SpeedMitigedScalar : m_ssoCharacter.SpeedScalar;
		Vector2 input = m_moveInput != Vector2.zero 
			? m_moveInput * m_ssoCharacter.Speed * m_speedScalar * Time.fixedDeltaTime 
			: Vector2.zero;
		
		Vector2 jump = IsJumping
			? Vector2.up * m_ssoCharacter.JumpForce * (m_jumpTimer / m_ssoCharacter.JumpDuration) * Time.fixedDeltaTime 
			: Vector2.zero;

		m_rigidbody2D.MovePosition(m_rigidbody2D.position + gravity + input + jump);
	}
}