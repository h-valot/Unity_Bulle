using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class Bubble : MonoBehaviour
{
	[Title("Tweakable values")]
	[SerializeField] public CharacterType Type;
    [SerializeField] private List<Sprite> m_sprites = new List<Sprite>();

    [FoldoutGroup("Internal references")][SerializeField] private GameObject m_graphicsParent;
	[FoldoutGroup("Internal references")][SerializeField] private SpriteRenderer m_spriteRenderer;

	private int m_currentIndex = -1;

	private void Start()
	{
		m_graphicsParent.SetActive(false);
	}

    private void OnTriggerStay2D(Collider2D collider)
	{
		// Assertion
		if (!collider.TryGetComponent<CharacterMotor>(out var character)) return;

		m_graphicsParent.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collider)
	{
		// Assertion
		if (!collider.TryGetComponent<CharacterMotor>(out var character)) return;

        m_graphicsParent.SetActive(false);
    }

	public void SetIndex(int index)
	{
		m_currentIndex = index;
		m_currentIndex = Mathf.Clamp(m_currentIndex, -1, m_sprites.Count - 1);
		
		if (m_currentIndex == -1
		|| m_sprites.Count <= 0)
		{
			m_graphicsParent.SetActive(false);
			return;
		}

		m_spriteRenderer.sprite = m_sprites[m_currentIndex];
	}
}