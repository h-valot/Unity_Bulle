using DG.Tweening;
using UnityEngine;

public class BubbleBouncing : MonoBehaviour
{
    [SerializeField] float m_bounceForce = 0.2f;
    [SerializeField] float m_bounceTime = 2f;
    [SerializeField] int m_bounceVibrato = 5;
    [SerializeField] float m_bounceDelay = 3f;

    private bool ShouldBounce = false;

    private void OnEnable()
    {
        if (ShouldBounce)
        {
            Bounce();
        }
    }

    private void OnDisable()
    {
        DOTween.Kill(this);
        transform.localScale = Vector3.one;
    }

    public void StartBounce()
    {
        ShouldBounce = true;
        Bounce();
    }

    public void StopBounce()
    {
        ShouldBounce = false;
        DOTween.Kill(this);
        transform.localScale = Vector3.one;
    }

    private void Bounce()
    {
        Sequence bounceSequence = DOTween.Sequence().Pause();
        bounceSequence.AppendInterval(m_bounceDelay / 2f);
        bounceSequence.Append(transform.DOPunchScale(new Vector3(m_bounceForce, m_bounceForce, 0), m_bounceTime, m_bounceVibrato));
        bounceSequence.AppendInterval(m_bounceDelay / 2f);
        bounceSequence.SetId(this);
        bounceSequence.Play().OnComplete(() => { Bounce(); });
    }
}
