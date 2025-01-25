using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnScaleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Tweakable values")]
	[SerializeField] private float _scaleDuration = 0.1f;
	[SerializeField] private float _scaleDownMultiplier = 0.8f;
	[SerializeField] private float _scaleUpMultiplier = 1.2f;

	[Header("Internal references")]
	[Tooltip("It will be scaled down on pointer down and reset to normal on pointer up")]
	[SerializeField] private GameObject _graphicsParent;

	[Space(10)]
	public UnityEvent OnClick;

	public void OnPointerDown(PointerEventData data)
	{
		_graphicsParent.transform.DOScale(_scaleDownMultiplier, _scaleDuration).SetEase(Ease.OutBack);
	}

	public void OnPointerUp(PointerEventData data)
	{
		_graphicsParent.transform.DOScale(Vector3.one, _scaleDuration).SetEase(Ease.OutBack);
		OnClick.Invoke();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		_graphicsParent.transform.DOScale(_scaleUpMultiplier, _scaleDuration).SetEase(Ease.OutBack);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_graphicsParent.transform.DOScale(Vector3.one, _scaleDuration).SetEase(Ease.OutBack);
	}
}