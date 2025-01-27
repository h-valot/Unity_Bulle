using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private RSO_CurrentPanel m_rsoCurrentPanel;

    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_portMusic;
    [SerializeField] private AudioClip m_abyssMusic;
    [SerializeField] private AudioClip m_marketMusic;
    [SerializeField] private AudioClip m_gardenMusic;
    [SerializeField] private AudioClip m_houseMusic;
    [SerializeField] private AudioClip m_whaleMusic;
    [SerializeField] private AudioClip m_rainMusic;
    [SerializeField] private AudioClip m_lighthouseTopMusic;
    [SerializeField] private AudioClip m_lighthouseBottomMusic;

    private void OnEnable()
    {
        m_rsoCurrentPanel.OnChanged += DeterminePanelMusic;
    }

    private void OnDisable()
    {
        m_rsoCurrentPanel.OnChanged -= DeterminePanelMusic;
    }

    private void DeterminePanelMusic(PanelType panelType)
    {
        switch (panelType)
        {
            case PanelType.HARBOR:
                Debug.Log(panelType.ToString());
                PlayMusic(m_portMusic);
                break;
            
            case PanelType.MARKET:
                Debug.Log(panelType.ToString());
                PlayMusic(m_marketMusic);
                break;
            
            case PanelType.GARDEN:
                Debug.Log(panelType.ToString());
                PlayMusic(m_gardenMusic);
                break;
            
            case PanelType.ABYSS:
                Debug.Log(panelType.ToString());
                PlayMusic(m_abyssMusic);
                break;
            
            case PanelType.WHALE:
                Debug.Log(panelType.ToString());
                PlayMusic(m_whaleMusic);
                break;
            
            case PanelType.RAIN:
                Debug.Log(panelType.ToString());
                PlayMusic(m_rainMusic);
                break;

            case PanelType.LIGHTHOUSE_BOTTOM:
                Debug.Log(panelType.ToString());
                PlayMusic(m_lighthouseBottomMusic);
                break;

            case PanelType.LIGHTHOUSE_TOP:
                Debug.Log(panelType.ToString());
                PlayMusic(m_lighthouseTopMusic);
                break;

            case PanelType.HOUSE:
                Debug.Log(panelType.ToString());
                PlayMusic(m_houseMusic);
                break;

            case PanelType.ALL:
                Debug.Log(panelType.ToString());
                break;
        }
    }
    private void PlayMusic(AudioClip audioClip)
    {
        m_audioSource.Stop();
        m_audioSource.PlayOneShot(audioClip);
    }
}
