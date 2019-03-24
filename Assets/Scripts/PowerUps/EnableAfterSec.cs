using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnableAfterSec : MonoBehaviour
{
    public GameObject Enable;

    private void OnEnable()
    {
        Enable.transform.DOKill(false);
        Enable.SetActive(false);
        Enable.transform.DOScale(Vector3.one, 1f).SetUpdate(UpdateType.Normal, true).OnComplete(() =>
        {
            Enable.SetActive(true);
        });
    }
}
