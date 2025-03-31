using System.Collections;
using UnityEngine;

public class SeagullSFX : MonoBehaviour
{
    [SerializeField] private RSE_PlaySoundOfType m_rsePlaySoundOfType;
    [SerializeField] private float m_delayBetweenSound = 1;

    private Coroutine m_coroutine;
    
    public void StartSeagullSound()
    {
        m_coroutine = StartCoroutine(SeagullSoundCoroutine());
    }

    IEnumerator SeagullSoundCoroutine()
    {
        while (true)
        {
            m_rsePlaySoundOfType.Call(InteractType.PICKUP, ItemType.SEAGULL);
            yield return new WaitForSeconds(m_delayBetweenSound);
        }
    }

    public void StopSeagullSound()
    {
        StopCoroutine(m_coroutine);
    }
}
