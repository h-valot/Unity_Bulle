using Sirenix.OdinInspector;
using UnityEngine;

public class WaterBoxTrigger : MonoBehaviour
{
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_ToggleMitigedGravity m_rsoToggleMitigedGravity;
	
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (!collider.TryGetComponent<CharacterMotor>(out var character)) return;

		m_rsoToggleMitigedGravity.Value = true;
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if (!collider.TryGetComponent<CharacterMotor>(out var character)) return;

		m_rsoToggleMitigedGravity.Value = false;
	}
}