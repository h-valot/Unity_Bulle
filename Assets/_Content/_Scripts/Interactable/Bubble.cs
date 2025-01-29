using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using DG.Tweening;

public class Bubble : MonoBehaviour
{
	[Title("Tweakable values")]
	[SerializeField] public CharacterType Type;
    [SerializeField] private List<Sprite> m_sprites = new List<Sprite>();

    [FoldoutGroup("Internal references")][SerializeField] private GameObject m_graphicsParent;
	[FoldoutGroup("Internal references")][SerializeField] private SpriteRenderer m_spriteRenderer;

	[ReadOnly] public int Index = -1;

	private bool m_entered;
	private bool m_exited;

	private void Start()
	{
		m_graphicsParent.SetActive(false);
	}

    private void OnTriggerStay2D(Collider2D collider)
	{
		// Assertion
		if (!collider.TryGetComponent<CharacterMotor>(out var character)) return;

		if (Index == -1)
		{
			m_graphicsParent.SetActive(false);
			return;
		}

		m_entered = true;
	}

    private void OnTriggerExit2D(Collider2D collider)
	{
		// Assertion
		if (!collider.TryGetComponent<CharacterMotor>(out var character)) return;

		m_exited = true;
	}

	private void Update()
	{
		if (m_entered && !m_exited)
		{
			m_entered = false;
			Show();
		}

		else if (!m_entered && m_exited)
		{
			m_exited = false;
			Hide();
		}
	}

	private void Show()
	{
		m_graphicsParent.SetActive(true);
		m_graphicsParent.transform.DOScale(1f, 0.1f).SetEase(Ease.OutBounce);
	}

	private void Hide()
	{
		m_graphicsParent.transform.DOScale(0f, 0.2f).SetEase(Ease.OutBounce)
			.OnComplete(() => { m_graphicsParent.SetActive(false); });
	}

	public void SetIndex(int index)
	{
		Index = index;
		Index = Mathf.Clamp(Index, -1, m_sprites.Count - 1);
		
		if (m_sprites.Count > 0
		&& Index >= 0)
		{
			m_spriteRenderer.sprite = m_sprites[Index];
		}
	}

	public void AnimateCollision()
	{
		m_graphicsParent.transform.DOShakeScale(0.6f, 0.5f);
	}
}