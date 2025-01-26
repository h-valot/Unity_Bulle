using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private RSE_PlaySoundOfType m_rsePlaySoundOfType;

    [Header("SFX")]
    [SerializeField] private AudioSource m_SFXSource;
    [SerializeField] private AudioClip m_pickUpSFX;
    [SerializeField] private AudioClip m_keySFX;
    [SerializeField] private AudioClip m_doorSFX;
    [SerializeField] private AudioClip m_fishSFX;
    [SerializeField] private AudioClip m_ladderSFX;
    [SerializeField] private AudioClip m_plantSFX;
    [SerializeField] private AudioClip m_divingSuitSFX;

    private void OnEnable()
    {
        m_rsePlaySoundOfType.Action += DetermineSFX;
    }

    private void OnDisable()
    {
        m_rsePlaySoundOfType.Action += DetermineSFX;
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
                sfxClip = m_pickUpSFX;
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
}

