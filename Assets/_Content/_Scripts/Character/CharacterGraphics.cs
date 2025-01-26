using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterGraphics : MonoBehaviour
{
	[FoldoutGroup("Internal references")][SerializeField] private GameObject m_divingSuit;
	[FoldoutGroup("Internal references")][SerializeField] private SpriteRenderer m_spriteRenderer;

	[FoldoutGroup("Scriptable")][SerializeField] private SSO_Character m_ssoCharacter;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_ToggleDivingSuit m_rsoToggleDivingSuit;
	[FoldoutGroup("Scriptable")][SerializeField] private RSE_Move m_rseMove;
	[FoldoutGroup("Scriptable")][SerializeField] private RSO_CurrentItem m_rsoCurrentItem;
    [FoldoutGroup("Scriptable")][SerializeField] private RSE_SetCharacterScale m_rseSetCharacterScale;

    private Vector2 m_moveInput;
	private float m_oscillationTimer;
	private List<Sprite> m_currentSprites = new List<Sprite>();
	private int m_currentIndex;

	private void Start()
	{
		m_rsoToggleDivingSuit.Value = false;
		ScaleCharacterGraphics(m_ssoCharacter.GraphicsScaleBase);
	}

	private void OnEnable()
	{
		m_rsoToggleDivingSuit.OnChanged += ToggleDivingSuit;
		m_rseMove.Action += UpdateMoveInput;
		m_rseSetCharacterScale.Action += ScaleCharacterGraphics;
    }

	private void OnDisable()
	{
		m_rsoToggleDivingSuit.OnChanged -= ToggleDivingSuit;
		m_rseMove.Action -= UpdateMoveInput;
        m_rseSetCharacterScale.Action -= ScaleCharacterGraphics;
    }

	private void Update()
	{
		UpdateSprites();
		HandleOscillation();
	}

	private void UpdateMoveInput(Vector2 input)
	{
		m_moveInput = input;
	}

	private void ToggleDivingSuit(bool isEnabled)
	{
		m_divingSuit.SetActive(isEnabled);
	}

	public void UpdateSprites()
	{
		if (m_moveInput.magnitude > 0f) m_spriteRenderer.flipX = m_moveInput.x > 0;

		m_currentSprites = m_rsoCurrentItem.Value == ItemType.NONE
			? m_ssoCharacter.WalkSprites
			: m_moveInput.magnitude > 0f 
				? m_ssoCharacter.CarryWalkSprites 
				: m_ssoCharacter.CarrySprites;

		m_spriteRenderer.sprite = m_currentSprites[m_currentIndex];
	}

	private void HandleOscillation()
	{
		m_oscillationTimer -= Time.deltaTime;
		if (m_oscillationTimer <= 0f)
		{
			m_currentIndex++;
			m_oscillationTimer = m_ssoCharacter.OscillationDuration;
			if (m_currentIndex > m_currentSprites.Count - 1)
			{
				m_currentIndex = 0;
			}
		}
	}

	private void ScaleCharacterGraphics(float newScale)
	{
		transform.localScale = Vector3.one * newScale;
	}
}
