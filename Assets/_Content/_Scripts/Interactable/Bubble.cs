using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using DG.Tweening;

public class Bubble : MonoBehaviour
{
    [FoldoutGroup("Scriptable")][SerializeField] private RSE_ToggleForceActive m_rseToggleForceActive;

    [Title("Tweakable values")]
	[SerializeField] public CharacterType Type;
    [SerializeField] private List<Sprite> m_sprites = new List<Sprite>();
	[SerializeField] private float m_idMono;
	[SerializeField] private Sprite m_spMono;
    [SerializeField] private bool m_forceActive = false;

	[FoldoutGroup("Internal references")][SerializeField] private GameObject m_graphicsParent;
	[FoldoutGroup("Internal references")][SerializeField] private SpriteRenderer m_spriteRenderer;

	[ReadOnly] public int Index = -1;

	private bool m_entered;
	private bool m_exited;

    private void OnEnable()
    {
		m_rseToggleForceActive.Action += ToggleForceActive;
    }

    private void OnDisable()
    {
        m_rseToggleForceActive.Action -= ToggleForceActive;
    }

    private void Start()
	{
		m_graphicsParent.SetActive(m_forceActive);
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
		// Assertion
		if (m_forceActive) return;

		if (m_entered)
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

	public void SetMono()
	{
		// Assertion
		if (!m_spMono) return;
		if (m_idMono != Index) return;
		
		m_spriteRenderer.sprite = m_spMono;
	}

	public void AnimateCollision()
	{
		m_graphicsParent.transform.DOShakeScale(0.6f, 0.5f);
	}

	public void ToggleForceActive(bool state)
	{
		if (Type != CharacterType.GRANDPA) return;

		m_forceActive = state;

		if (!m_forceActive)
		{
			m_entered = false;
			m_exited = false;
            m_graphicsParent.SetActive(false);
        }

		if (m_forceActive && !m_graphicsParent.activeInHierarchy) 
		{
			Show();
		}
	}
}