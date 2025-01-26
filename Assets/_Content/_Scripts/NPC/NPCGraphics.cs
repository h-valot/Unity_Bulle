using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class NPCGraphics : MonoBehaviour
{
	[FoldoutGroup("Tweakable values")][SerializeField] protected float m_oscillationDuration;
	[FoldoutGroup("Tweakable values")][SerializeField] protected List<Sprite> m_defaultSprites = new List<Sprite>();
    
	[FoldoutGroup("Internal references")][SerializeField] protected SpriteRenderer m_spriteRenderer;

	protected List<Sprite> m_currentSprites = new List<Sprite>();
	protected int m_currentIndex;
	protected float m_oscillationTimer;

	protected virtual void Start()
	{
		m_oscillationTimer = m_oscillationDuration;
		m_currentSprites = m_defaultSprites;
	}

	protected virtual void Update()
	{
		UpdateSprites(); 
		HandleOscillation();
	}

	protected virtual void UpdateSprites()
	{
		if (m_currentSprites.Count <= 0) return;

		m_spriteRenderer.sprite = m_currentSprites[m_currentIndex];
	}

	protected virtual void HandleOscillation()
	{
		m_oscillationTimer -= Time.deltaTime;
		if (m_oscillationTimer <= 0f)
		{
			m_currentIndex++;
			m_oscillationTimer = m_oscillationDuration;

			if (m_currentIndex > m_currentSprites.Count - 1)
			{
				m_currentIndex = 0;
			}
		}
	}
}