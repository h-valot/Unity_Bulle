using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class NPCFisherman : NPCGraphics
{
	[FoldoutGroup("Tweakable values")][SerializeField] protected List<Sprite> m_spritesSatisfied = new List<Sprite>();

	[FoldoutGroup("Internal references")][SerializeField] protected Bubble m_bubble;

	protected override void Update()
	{
		UpdateSprites();
	}

	protected override void UpdateSprites()
	{
		m_currentSprites = m_bubble.Index <= 0
			? m_defaultSprites
			: m_spritesSatisfied;

		m_spriteRenderer.sprite = m_currentSprites[0];
	}
}