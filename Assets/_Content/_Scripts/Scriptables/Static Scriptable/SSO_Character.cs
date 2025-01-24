using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SSO_Character", menuName = "Static Scriptable/Character")]
public class SSO_Character : ScriptableObject
{
	[InfoBox("Scalar to the character movement.", InfoMessageType.None)]
	public float Speed;
}