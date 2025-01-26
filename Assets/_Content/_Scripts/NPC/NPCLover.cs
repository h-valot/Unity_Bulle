using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class NPCLover : NPCGraphics
{
	[FoldoutGroup("Tweakable values")][SerializeField] protected List<Sprite> m_spriteChocked = new List<Sprite>();

	[FoldoutGroup("Scriptable")][SerializeField] protected RSO_CurrentItem m_rsoCurrentItem;

	protected override void UpdateSprites()
	{
		m_currentSprites = m_rsoCurrentItem.Value != ItemType.GRANNY
			? m_defaultSprites
			: m_spriteChocked;

		base.UpdateSprites();
	}

	protected override void HandleOscillation()
	{
		base.HandleOscillation();

		if (m_rsoCurrentItem.Value == ItemType.GRANNY)
		{
			m_currentIndex = 0;
		}
	}
}