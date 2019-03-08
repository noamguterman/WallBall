using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PompAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(Vector3.one * 1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

}
