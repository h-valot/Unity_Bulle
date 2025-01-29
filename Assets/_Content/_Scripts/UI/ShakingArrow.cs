using DG.Tweening;
using UnityEngine;

public class ShakingArrow : MonoBehaviour
{
    private void OnEnable()
    {
        Sequence _shakeSequence = DOTween.Sequence().Pause();
        _shakeSequence.AppendInterval(Random.value * 5f);
        _shakeSequence.SetId(gameObject.GetInstanceID());
        _shakeSequence.Play().OnComplete(() => { Shake(); });
    }

    private void OnDisable()
    {
        DOTween.Kill(this);
    }

    private void Shake()
    {
        transform.DOPunchRotation(new Vector3(0, 0, 10f), 5f, 10, 1f).SetId(this).OnComplete(() => { Shake(); }); ;
    }
}
