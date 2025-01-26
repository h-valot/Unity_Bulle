using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class NPCGraphics : MonoBehaviour
{
	[Title("Tweakable values")]
    [SerializeField] private float m_oscillationDuration;
    [SerializeField] private List<Sprite> m_sprites = new List<Sprite>();
    
	[FoldoutGroup("Internal references")][SerializeField] private SpriteRenderer m_spriteRenderer;

    private int m_currentIndex;
    private float m_oscillationTimer;

	private void Start()
	{
		m_oscillationTimer = m_oscillationDuration;
	}

    private void Update()
	{
		UpdateSprites(); 
		HandleOscillation();
	}

	private  void UpdateSprites()
	{
		if (m_sprites.Count <= 0) return;

		m_spriteRenderer.sprite = m_sprites[m_currentIndex];
	}

	private void HandleOscillation()
	{
		m_oscillationTimer -= Time.deltaTime;
		if (m_oscillationTimer <= 0f)
		{
			m_currentIndex++;
			m_oscillationTimer = m_oscillationDuration;

			if (m_currentIndex > m_sprites.Count - 1)
			{
				m_currentIndex = 0;
			}
		}
	}
}