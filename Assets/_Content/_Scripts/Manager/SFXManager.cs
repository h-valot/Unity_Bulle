using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private RSE_PlaySoundOfType m_rsePlaySoundOfType;
    [SerializeField] private RSE_PlaySpecialSFX m_rsePlaySpecialSFX;

    [Header("SFX")]
    [SerializeField] private AudioSource m_SFXSource;
    [SerializeField] private AudioClip m_pickUpSFX;
    [SerializeField] private AudioClip m_keySFX;
    [SerializeField] private AudioClip m_doorSFX;
    [SerializeField] private AudioClip m_fishSFX;
    [SerializeField] private AudioClip m_ladderSFX;
    [SerializeField] private AudioClip m_plantSFX;
    [SerializeField] private AudioClip m_divingSuitSFX;
    [SerializeField] private AudioClip m_diveSFX;
    [SerializeField] private AudioClip m_wooshSFX;
    [SerializeField] private AudioClip m_whaleSFX;
    [SerializeField] private AudioClip m_heySFX;
    [SerializeField] private AudioClip m_drawingSFX;

    private void OnEnable()
    {
        m_rsePlaySoundOfType.Action += DetermineSFX;
        m_rsePlaySpecialSFX.Action += DetermineSpecialSFX;
    }

    private void OnDisable()
    {
        m_rsePlaySoundOfType.Action -= DetermineSFX;
        m_rsePlaySpecialSFX.Action -= DetermineSpecialSFX;
    }
    
    private void DetermineSFX(InteractType interactType, ItemType itemType)
    {
		AudioClip sfxClip = null;

        switch (interactType)
        {
            case InteractType.SPAWN:

                switch (itemType)
                {
                    case ItemType.KEY:
                        sfxClip = m_keySFX;
                        break;

                    case ItemType.DIVING_SUIT:
                        sfxClip = m_divingSuitSFX;
                        break;

                    case ItemType.FISH:
                        sfxClip = m_fishSFX;
                        break;

                    case ItemType.LADDER:
                        sfxClip = m_ladderSFX;
                        break;

                    case ItemType.PLANT:
                        sfxClip = m_plantSFX;
                        break;
                }
                break;

            case InteractType.PLACE:
                switch (itemType)
                {
                    case ItemType.KEY:
                        sfxClip = m_doorSFX;
                        break;

                    case ItemType.LADDER:
                        sfxClip = m_ladderSFX;
                        break;

                    case ItemType.FISH:
                        sfxClip = m_fishSFX;
                        break;

                    case ItemType.PLANT:
                        sfxClip = m_plantSFX;
                        break;
                }
                break;

            case InteractType.PICKUP:
                if(itemType == ItemType.BOOT)
                {
                    sfxClip = m_whaleSFX;
                }
                else
                {
                    sfxClip = m_pickUpSFX;
                }
                break;
        }

        if (sfxClip != null)
        {
            PlaySFX(sfxClip);
        }
    }

    private void DetermineSpecialSFX(SpecialSFX sfx)
    {
        AudioClip sfxClip = null;

        switch (sfx)
        {
            case SpecialSFX.DIVE:
                sfxClip = m_diveSFX;
                break;
            case SpecialSFX.HEY:
                sfxClip = m_heySFX;
                break;
            case SpecialSFX.WOOSH:
                sfxClip = m_wooshSFX;
                break;
            case SpecialSFX.DRAWING:
                sfxClip = m_drawingSFX;
                break;

        }

        if (sfxClip != null)
        {
            PlaySFX(sfxClip);
        }
    }

    private void PlaySFX(AudioClip sfxClip)
	{
		m_SFXSource.PlayOneShot(sfxClip);
    }

	private float m_timer;
	private void Update()
	{
		m_timer -= Time.deltaTime;
	}
}

