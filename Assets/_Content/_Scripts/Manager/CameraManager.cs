using Sirenix.OdinInspector;
using System.Linq.Expressions;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	[FoldoutGroup("Internal references")][SerializeField] private Camera m_camera;

	[FoldoutGroup("Scriptable")][SerializeField] private SSO_Camera m_ssoCamera;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentPosition m_rsoCurrentPosition;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SetCameraLerp m_rseSetCameraLerp;
    [FoldoutGroup("Scriptable")][SerializeField] private RSE_SetCameraSmoothTime m_rseSetCameraSmoothTime;

    private Vector3 m_currentVelocity = new Vector3();
	private bool m_isLerpEnabled = true;
	private float m_smoothTime;


    private void OnEnable()
	{
		m_rseSetCameraLerp.Action += SetLerp;
        m_rseSetCameraSmoothTime.Action += SetSmoothTime;
    }

	private void OnDisable()
	{
		m_rseSetCameraLerp.Action -= SetLerp;
        m_rseSetCameraSmoothTime.Action -= SetSmoothTime;
    }

	private void SetLerp(bool isEnabled)
	{
		m_isLerpEnabled = isEnabled;
    }

	private void SetSmoothTime(float smoothTime)
	{
		m_smoothTime = smoothTime;
        m_currentVelocity = new Vector3();
    }

	private void LateUpdate()
	{
		if (!m_isLerpEnabled) return;

		m_camera.transform.position = Vector3.SmoothDamp(
			m_camera.transform.position,
			new Vector3(m_rsoCurrentPosition.Value.x, m_rsoCurrentPosition.Value.y + 2, -10),
			ref m_currentVelocity,
            m_smoothTime
        );
    }
}