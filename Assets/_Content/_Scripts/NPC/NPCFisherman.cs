using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class NPCFisherman : NPCGraphics
{
	[FoldoutGroup("Tweakable values")][SerializeField] protected List<Sprite> m_spritesSatisfied = new List<Sprite>();

	[FoldoutGroup("Internal references")][SerializeField] protected Bubble m_bubble;

	protected override void Start()
	{
		base.Start();
		m_currentIndex = 0;
	}

	protected override void Update()
	{
		UpdateSprites();
	}

	protected override void UpdateSprites()
	{
		m_currentSprites = m_bubble.Index <= 0
			? m_defaultSprites
			: m_spritesSatisfied;

		base.UpdateSprites();
	}
}