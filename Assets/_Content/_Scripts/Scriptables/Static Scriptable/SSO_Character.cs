using UnityEngine;

[CreateAssetMenu(fileName = "SSO_Character", menuName = "Static Scriptable/Character")]
public class SSO_Character : ScriptableObject
{
	public float Speed;
	public float SpeedScalar;
	public float SpeedMitigedScalar;

	public float JumpForce;
	public float JumpDuration;

	public float GravityForce;
	public float GravityScalar;
	public float GravityMitigedScalar;

	public float GroundLength;

	public LayerMask GroundLayerToInclude;
}