using UnityEngine;

[CreateAssetMenu(fileName = "SSO_Camera", menuName = "Static Scriptable/Camera")]
public class SSO_Camera : ScriptableObject
{
	public float SmoothTime;
	
	public float EndTranslationDuration;
	public float EndOrthoSize;
}