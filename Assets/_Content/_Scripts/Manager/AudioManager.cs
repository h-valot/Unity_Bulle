using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private RSO_CurrentPanel _rsoCurrentPanel;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _portMusic;
    [SerializeField] private AudioClip _abyssMusic;
    [SerializeField] private AudioClip _marketMusic;
    [SerializeField] private AudioClip _gardenMusic;
    [SerializeField] private AudioClip _houseMusic;
    [SerializeField] private AudioClip _whaleMusic;
    [SerializeField] private AudioClip _rainMusic;
    [SerializeField] private AudioClip _lighthouseTopMusic;
    [SerializeField] private AudioClip _lighthouseBottomMusic;
    private PanelType _currentPanel;
    private PanelType _lastPanel;

    void Start()
    {
        _lastPanel = PanelType.ALL;
    }

    private void DeterminePanel()
    {
        switch (_currentPanel)
        {
            case PanelType.HARBOR:
                PlayMusic(_portMusic);
                break;
            
            case PanelType.MARKET:
                PlayMusic(_marketMusic);
                break;
            
            case PanelType.GARDEN:
                PlayMusic(_gardenMusic);
                break;
            
            case PanelType.ABYSS:
                PlayMusic(_abyssMusic);
                break;
            
            case PanelType.WHALE:
                PlayMusic(_whaleMusic);
                break;
            
            case PanelType.RAIN:
                PlayMusic(_rainMusic);
                break;

            case PanelType.LIGHTHOUSE_BOTTOM:
                PlayMusic(_lighthouseBottomMusic);
                break;

            case PanelType.LIGHTHOUSE_TOP:
                PlayMusic(_lighthouseTopMusic);
                break;

            case PanelType.HOUSE:
                PlayMusic(_houseMusic);
                break;

            case PanelType.ALL:
                break;
        }
    }
    private void PlayMusic(AudioClip audioClip)
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(audioClip);
        _lastPanel = _currentPanel;
    }

    void Update()
    {
        _currentPanel = _rsoCurrentPanel.Value;
        if(_currentPanel != _lastPanel)
        {
            DeterminePanel();
        }
    }
}
