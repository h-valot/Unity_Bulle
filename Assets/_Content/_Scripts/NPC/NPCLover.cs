using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class NPCLover : NPCGraphics
{
	[FoldoutGroup("Tweakable values")][SerializeField] protected List<Sprite> m_spriteChocked = new List<Sprite>();

	[FoldoutGroup("Scriptable")][SerializeField] protected RSO_CurrentItem m_rsoCurrentItem;

	protected override void Update()
	{
		UpdateSprites();
	}

	protected override void UpdateSprites()
	{
		m_currentSprites =  m_defaultSprites;

		if (m_rsoCurrentItem.Value == ItemType.GRANNY) 
		{
			m_currentSprites = m_spriteChocked;
			m_currentIndex = 0;
		}

		m_spriteRenderer.sprite = m_currentSprites[m_currentIndex];
	}
}