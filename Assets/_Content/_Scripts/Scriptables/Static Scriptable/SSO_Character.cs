using System.Collections.Generic;
using Sirenix.OdinInspector;
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

	[Title("Graphics")]
	public float GraphicsScaleBase = 1;
	public float OscillationDuration;
	public float CarryIdleDelay;
	public List<Sprite> CarrySprites = new List<Sprite>();
	public List<Sprite> CarryWalkSprites = new List<Sprite>();
	public List<Sprite> WalkSprites = new List<Sprite>();

	[Title("Panel Special")]
	public float GraphicsScaleInHouse = 2;
}