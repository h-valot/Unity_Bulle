using UnityEngine;

[CreateAssetMenu(fileName = "SSO_Camera", menuName = "Static Scriptable/Camera")]
public class SSO_Camera : ScriptableObject
{
	public float SmoothTime;
	
	public float EndTranslationDuration;
	public float EndOrthoSize;

	public float IntroGrandpaTranslationDuration;
	public float IntroKidTranslationDuration;
	public float DefaultOrthoSize;

	public Vector3 GetOffset(Vector3 position)
	{
		return new Vector3(position.x, position.y, -10);
	}
}