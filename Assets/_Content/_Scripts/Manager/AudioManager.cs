using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[SerializeField] private float m_defaultVolume;
	[SerializeField] private float m_crossfadeDuration;

	[SerializeField] private RSO_CurrentPanel m_rsoCurrentPanel;

	[SerializeField] private AudioSource m_audioSourceA;
	[SerializeField] private AudioSource m_audioSourceB;
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

	private Coroutine m_crossfadeCoroutine = null;
    private void PlayMusic(AudioClip audioClip)
    {
		var current = m_audioSourceA.isPlaying ? m_audioSourceA : m_audioSourceB;
		var target = m_audioSourceA.isPlaying ? m_audioSourceB : m_audioSourceA;

		target.clip = audioClip;
		if (m_crossfadeCoroutine != null) StopCoroutine(m_crossfadeCoroutine) ;
		m_crossfadeCoroutine = StartCoroutine(Crossfade(current, target));
    }

	private IEnumerator Crossfade(AudioSource current, AudioSource target)
	{
		if (target.isPlaying == false) target.Play();
		target.UnPause();

		float percentage = 0;
		while (current.volume > 0)
		{
			current.volume = Mathf.Lerp(m_defaultVolume, 0, percentage);
			target.volume = Mathf.Lerp(0, m_defaultVolume, percentage);
			percentage += Time.deltaTime / m_crossfadeDuration;
			yield return null;
		}

		current.Pause();
		m_crossfadeCoroutine = null;
	}
}