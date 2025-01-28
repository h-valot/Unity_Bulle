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
                PlayMusic(m_portMusic);
                break;
            
            case PanelType.MARKET:
                PlayMusic(m_marketMusic);
                break;
            
            case PanelType.GARDEN:
                PlayMusic(m_gardenMusic);
                break;
            
            case PanelType.ABYSS:
                PlayMusic(m_abyssMusic);
                break;
            
            case PanelType.WHALE:
                PlayMusic(m_whaleMusic);
                break;
            
            case PanelType.RAIN:
                PlayMusic(m_rainMusic);
                break;

            case PanelType.LIGHTHOUSE_BOTTOM:
                PlayMusic(m_lighthouseBottomMusic);
                break;

            case PanelType.LIGHTHOUSE_TOP:
                PlayMusic(m_lighthouseTopMusic);
                break;

            case PanelType.HOUSE:
                PlayMusic(m_houseMusic);
                break;
        }
    }

    private void PlayMusic(AudioClip audioClip)
    {
        m_audioSource.Stop();
        m_audioSource.PlayOneShot(audioClip);
    }
}