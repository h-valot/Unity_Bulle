using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SetBubble m_rseSetBubble;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_SetMono m_rseSetMono;

	private List<Bubble> m_bubbles = new List<Bubble>();

	private void Awake()
	{
		m_bubbles = FindObjectsOfType<Bubble>().ToList();
	}

	private void OnEnable()
	{
		m_rseSetBubble.Action += SetBubble;
		m_rseSetMono.Action += SetMono;
	}

	private void OnDisable()
	{
		m_rseSetBubble.Action -= SetBubble;
		m_rseSetMono.Action -= SetMono;
	}

	private void SetBubble(CharacterType type, int index)
	{
		m_bubbles.FirstOrDefault(b => b.Type == type)?.SetIndex(index);
	}

	private void SetMono(CharacterType type)
	{
		m_bubbles.FirstOrDefault(b => b.Type == type)?.SetMono();
	}
}