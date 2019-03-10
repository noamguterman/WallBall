using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PopupOnEnable : MonoBehaviour
{
    private Sequence seq;
    void OnEnable()
    {
        transform.localScale = Vector3.zero;
        seq = DOTween.Sequence();
        seq.Append(transform.DOScale(Vector3.one* 1.2f, 0.4f).SetUpdate(UpdateType.Manual));
        seq.Append(transform.DOScale(Vector3.one, 0.2f).SetUpdate(UpdateType.Manual));
        seq.SetUpdate(UpdateType.Normal, true);
    }

}
