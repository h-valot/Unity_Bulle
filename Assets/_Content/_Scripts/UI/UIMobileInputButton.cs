using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIMobileInputButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[Header("Tweakable values")]
	[SerializeField] private float m_scaleDuration = 0.1f;
	[SerializeField] private float m_scaleDownMultiplier = 0.8f;
	[SerializeField] private float m_scaleUpMultiplier = 1.2f;

	[Space(10)]
	public UnityEvent OnUp;
	public UnityEvent OnDown;

	public void OnPointerDown(PointerEventData data)
	{
		transform.DOScale(m_scaleDownMultiplier, m_scaleDuration).SetEase(Ease.OutBack);
		OnDown.Invoke();
	}

	public void OnPointerUp(PointerEventData data)
	{
		transform.DOScale(Vector3.one, m_scaleDuration).SetEase(Ease.OutBack);
		OnUp.Invoke();
	}
}