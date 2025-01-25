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
                Debug.Log(_currentPanel.ToString());
                PlayMusic(_portMusic);
                break;
            
            case PanelType.MARKET:
                Debug.Log(_currentPanel.ToString());
                PlayMusic(_marketMusic);
                break;
            
            case PanelType.GARDEN:
                Debug.Log(_currentPanel.ToString());
                PlayMusic(_gardenMusic);
                break;
            
            case PanelType.ABYSS:
                Debug.Log(_currentPanel.ToString());
                PlayMusic(_abyssMusic);
                break;
            
            case PanelType.WHALE:
                Debug.Log(_currentPanel.ToString());
                PlayMusic(_whaleMusic);
                break;
            
            case PanelType.RAIN:
                Debug.Log(_currentPanel.ToString());
                PlayMusic(_rainMusic);
                break;

            case PanelType.LIGHTHOUSE_BOTTOM:
                Debug.Log(_currentPanel.ToString());
                PlayMusic(_lighthouseBottomMusic);
                break;

            case PanelType.LIGHTHOUSE_TOP:
                Debug.Log(_currentPanel.ToString());
                PlayMusic(_lighthouseTopMusic);
                break;

            case PanelType.HOUSE:
                Debug.Log(_currentPanel.ToString());
                PlayMusic(_houseMusic);
                break;

            case PanelType.ALL:
                Debug.Log(_currentPanel.ToString());
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
