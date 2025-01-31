using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PanelsManager : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float m_revealSpeed = 0.5f;
	[SerializeField] private Camera m_camera;
	[SerializeField] private Transform m_character;
	[SerializeField] private Transform m_grandpa;

	[Header("Scriptable")]
    [SerializeField] private RSE_DisplayIntro m_rseDisplayIntro;
    [SerializeField] private RSO_CurrentPanel m_rsoCurrentPanel;
    [SerializeField] private RSE_SetCharacterScale m_rseSetCharacterScale;
    [SerializeField] private SSO_Character m_ssoCharacter;
	[SerializeField] private RSO_LockInputs m_rsoLockInputs;
	[SerializeField] private RSE_SetCameraLerp m_rseSetCameraLerp;
	[SerializeField] private SSO_Camera m_ssoCamera;
	[SerializeField] private RSE_SetBubble m_rseSetBubble;
    [SerializeField] private RSO_SpecialTransition m_rsoSpecialTransition;
	[SerializeField] private RSE_SetMobileInputs m_rseSetMobileInputs;
    [SerializeField] private RSE_SetCameraSmoothTime m_rseSetCameraSmoothTime;
    [SerializeField] private RSE_ToggleForceActive m_rseToggleForceActive;
    [SerializeField] private RSE_PlaySpecialSFX m_rsoPlaySpecialSFX;

    [Header("Panel GameObjects")]
    [SerializeField] private SpriteMask[] m_spriteMasks;
    [SerializeField] private PanelType[] m_panelsType;
    [SerializeField] private bool[] m_arePanelsDiscovered;

    private List<SpriteMask> m_spriteMasksToReveal = new List<SpriteMask>();

	private void Start()
    {
        //Check what are the panel discovered at start
        for (int i = 0; i < m_spriteMasks.Length; i++)
        {
			m_spriteMasks[i].alphaCutoff = 1;
            if (m_arePanelsDiscovered[i])
            {
                m_spriteMasksToReveal.AddUnique(m_spriteMasks[i]);
			}
		}    
    }

    private void OnEnable()
    {
        m_rseDisplayIntro.Action += RevealStart;
		m_rseDisplayIntro.Action += OnDisplayIntro;
		m_rsoCurrentPanel.OnChanged += CheckPanelDiscovered;
        m_rsoCurrentPanel.OnChanged += TogglePanelSpecialAction;
    }

    private void OnDisable()
    {
        m_rseDisplayIntro.Action -= RevealStart;
		m_rseDisplayIntro.Action -= OnDisplayIntro;
        m_rsoCurrentPanel.OnChanged -= CheckPanelDiscovered;
        m_rsoCurrentPanel.OnChanged -= TogglePanelSpecialAction;
    }

    private void CheckPanelDiscovered(PanelType panelType)
    {
        int panelIndex = 0;
        
        for (int i = 0; i < m_spriteMasks.Length; i++)
        {
            if (m_panelsType[i] == panelType)
            {
                panelIndex = i;
                break;
            }
        }

        if (!m_arePanelsDiscovered[panelIndex])
        {
            RevealPanel(panelIndex);
            m_arePanelsDiscovered[panelIndex] = true;
        }
        else
        {
            m_rsoPlaySpecialSFX.Call(SpecialSFX.WOOSH);
        }
    }

    private void RevealPanel(int panelIndex)
    {
		// Disable camera and character inputs 
		m_rsoLockInputs.Value = true;
	    m_rseSetCameraLerp.Call(false);

        m_rsoPlaySpecialSFX.Call(SpecialSFX.DRAWING);
        // Zoom out at the panel position, then unmask panel
        m_camera.transform.DOMove(m_ssoCamera.GetOffset(m_spriteMasks[panelIndex].transform.position), m_ssoCamera.DiscoveryInTranslationDuration);
		m_camera.DOOrthoSize(m_ssoCamera.DiscoveryOrthoSize, m_ssoCamera.DiscoveryInTranslationDuration);
		DOTween.To(() => m_spriteMasks[panelIndex].alphaCutoff, x => m_spriteMasks[panelIndex].alphaCutoff = x, 0, m_revealSpeed)
			.SetEase(Ease.Linear)
			.SetLink(gameObject)
			.OnComplete(() =>
			{
                m_rseSetCameraSmoothTime.Call(m_ssoCamera.SmoothTimeNewPanel);
                m_rseSetCameraLerp.Call(true);
                m_camera.DOOrthoSize(m_ssoCamera.DefaultOrthoSize, m_ssoCamera.SmoothTimeNewPanel).OnComplete(() => { 
                    m_rseSetCameraSmoothTime.Call(m_ssoCamera.SmoothTime);
                    m_rsoLockInputs.Value = false;
                });
            });
    }

    private void TogglePanelSpecialAction(PanelType panelType)
    {
        //Scale Up if in House, or reset scale
        if (panelType == PanelType.HOUSE) 
        {
            m_rseSetCharacterScale.Call(m_ssoCharacter.GraphicsScaleInHouse);
        }
        else
        {
            m_rseSetCharacterScale.Call(m_ssoCharacter.GraphicsScaleBase);
        }
    }

    private void RevealStart()
    {
        SpriteMask panelToReveal = m_spriteMasksToReveal[0];
        m_spriteMasksToReveal.RemoveAt(0);
        m_rsoPlaySpecialSFX.Call(SpecialSFX.DRAWING);

        DOTween.To(() => panelToReveal.alphaCutoff, x => panelToReveal.alphaCutoff = x, 0, m_revealSpeed)
			.SetEase(Ease.Linear)
			.SetLink(gameObject)
			.OnComplete(() => { RevealStart(); });
    }

	private void OnDisplayIntro()
	{	
		StartCoroutine(AnimateIntro());
	}

	private IEnumerator AnimateIntro()
	{
		m_rseSetBubble.Call(CharacterType.GRANDPA, 0);
		m_camera.transform.DOMove(m_ssoCamera.GetOffset(m_grandpa.position), m_ssoCamera.IntroGrandpaTranslationDuration);
		m_camera.DOOrthoSize(m_ssoCamera.DefaultOrthoSize, m_ssoCamera.IntroGrandpaTranslationDuration);

        yield return new WaitForSeconds(m_ssoCamera.IntroGrandpaTimingHey);

        m_rsoPlaySpecialSFX.Call(SpecialSFX.HEY);
        m_rseToggleForceActive.Call(true);

        yield return new WaitForSeconds(m_ssoCamera.IntroGrandpaTranslationDuration - m_ssoCamera.IntroGrandpaTimingHey);

		m_camera.DOOrthoSize(m_ssoCamera.DefaultOrthoSize, m_ssoCamera.IntroKidTranslationDuration);
        m_rseSetCameraSmoothTime.Call(m_ssoCamera.SmoothTimeIntro);
        m_rseSetCameraLerp.Call(true);

        yield return new WaitForSeconds(m_ssoCamera.IntroKidTranslationDuration);

		m_rseSetBubble.Call(CharacterType.GRANDPA, 1);
		m_rsoLockInputs.Value = false;
        m_rseSetCameraSmoothTime.Call(m_ssoCamera.SmoothTime);
		m_rseSetMobileInputs.Call(true);
        m_rseToggleForceActive.Call(false);

    }
}
