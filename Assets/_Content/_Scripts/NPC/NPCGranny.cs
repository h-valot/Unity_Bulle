using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class NPCGranny : NPCGraphics
{
	[FoldoutGroup("Tweakable values")][SerializeField] protected List<Sprite> m_spritesCarried = new List<Sprite>();
	[FoldoutGroup("Tweakable values")][SerializeField] protected List<Sprite> m_spritesFall = new List<Sprite>();

	[FoldoutGroup("Scriptable")][SerializeField] protected RSO_CurrentItem m_rsoCurrentItem;
	[FoldoutGroup("Scriptable")][SerializeField] protected RSO_CharacterVelocity m_rsoCharacterVelocity;

	protected override void UpdateSprites()
	{
		m_currentSprites = m_rsoCurrentItem.Value != ItemType.GRANNY
			? m_defaultSprites
			: m_rsoCharacterVelocity.Value.y >= 0f
				? m_spritesCarried
				: m_spritesFall;

		base.UpdateSprites();
	}

	protected override void HandleOscillation()
	{
		base.HandleOscillation();

		if (m_rsoCharacterVelocity.Value.y < 0f)
		{
			m_currentIndex = 0;
			// TODO Handle Granny fall speed up with m_spritesFall[1]
		}
	}
}