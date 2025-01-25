using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
	[FoldoutGroup("External references")][SerializeField] private Camera m_camera;
	[FoldoutGroup("External references")][SerializeField] private List<CameraAnchor> m_cameraAnchors = new List<CameraAnchor>();

	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentPanel m_rsoCurrentPanel;

	private void OnEnable()
	{
		m_rsoCurrentPanel.OnChanged += UpdateCamera;
	}

	private void OnDisable()
	{
		m_rsoCurrentPanel.OnChanged -= UpdateCamera;
	}

	private void UpdateCamera(PanelType newValue)
	{
		var matchingCamera = m_cameraAnchors.FirstOrDefault(ca => ca.Type == newValue);
	}
}