using Sirenix.OdinInspector;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	[FoldoutGroup("Internal references")][SerializeField] private Camera m_camera;

	[FoldoutGroup("Scriptable")][SerializeField] private SSO_Camera m_ssoCamera;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentPosition m_rsoCurrentPosition;

	private Vector3 m_currentVelocity = new Vector3();

	private void LateUpdate()
	{
		m_camera.transform.position = Vector3.SmoothDamp(
			m_camera.transform.position,
			new Vector3(m_rsoCurrentPosition.Value.x, m_rsoCurrentPosition.Value.y, -10),
			ref m_currentVelocity,
			m_ssoCamera.SmoothTime
		);
	}
}