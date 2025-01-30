using DG.Tweening;
using UnityEngine;

public class ShakingArrow : MonoBehaviour
{
    [SerializeField] GameObject m_arrowGraphics;
    [SerializeField] RSO_CurrentPanel m_rsoCurrentPanel;
    [SerializeField] RSE_DisplayEnd m_rsoDisplayEnd;
    [SerializeField] PanelType m_panelType;

    private void OnEnable()
    {
        m_rsoCurrentPanel.OnChanged += DisplayArrow;
        m_rsoDisplayEnd.Action += Hide;
        DisplayArrow(m_rsoCurrentPanel.Value);

        Sequence _shakeSequence = DOTween.Sequence().Pause();
        _shakeSequence.AppendInterval(Random.value * 5f);
        _shakeSequence.SetId(gameObject.GetInstanceID());
        _shakeSequence.Play().OnComplete(() => { Shake(); });
    }

    private void OnDisable()
    {
        DOTween.Kill(this);
        m_rsoCurrentPanel.OnChanged -= DisplayArrow;
        m_rsoDisplayEnd.Action -= Hide;
    }

    private void Shake()
    {
        m_arrowGraphics.transform.DOPunchRotation(new Vector3(0, 0, 5f), 5f, 10, 1f).SetId(this).OnComplete(() => { Shake(); }); ;
    }

    private void DisplayArrow(PanelType panelType)
    {
        if (m_panelType == panelType)
        {
            m_arrowGraphics.SetActive(true);
        }
        else
        {
            m_arrowGraphics.SetActive(false);
        }
    }

    private void Hide(bool shouldHide)
    {
        if (shouldHide)
        {
            m_arrowGraphics.SetActive(false);
        }
    }
}
