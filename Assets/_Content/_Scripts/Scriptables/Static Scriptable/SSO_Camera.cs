using UnityEngine;

[CreateAssetMenu(fileName = "SSO_Camera", menuName = "Static Scriptable/Camera")]
public class SSO_Camera : ScriptableObject
{
	public float SmoothTime;
    public float SmoothTimeNewPanel;
    public float SmoothTimeIntro;

    public float EndTranslationDuration;
	public float EndOrthoSize;

	public float IntroGrandpaTranslationDuration;
    public float IntroGrandpaTimingHey;
    public float IntroKidTranslationDuration;
	public float DefaultOrthoSize;

	public float DiscoveryInTranslationDuration;
	public float DiscoveryOutTranslationDuration;
	public float DiscoveryOrthoSize;

	public Vector3 GetOffset(Vector3 position)
	{
		return new Vector3(position.x, position.y + 2, -10);
	}
}