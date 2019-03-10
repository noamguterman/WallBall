using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _follow;
    public float Speed = 2f;
    private float startZ;
    public Transform LowestPos;

    private void OnEnable()
    {
        startZ = transform.position.z;
    }

    private bool _ignore;

    void Update()
    {
        if (_ignore == true)
            return;
        
        if (_follow.position.y > LowestPos.position.y)
        {
            transform.position = Vector3.Lerp(transform.position, _follow.position, Time.deltaTime * Speed);
            transform.position = new Vector3(transform.position.x, transform.position.y, startZ);
            var brain = GetComponent<CinemachineBrain>();
            brain.enabled = true;
        }
        else
        {
            var brain = GetComponent<CinemachineBrain>();
            brain.enabled = false;
        }
    }

    public void Shake()
    {
        _ignore = true;
        var brain = GetComponent<CinemachineBrain>();
        brain.enabled = false;
        var startpos = transform.position;
        transform.DOShakePosition(0.3f, 1f).OnComplete(() =>
        {
            transform.position = startpos;
            brain.enabled = true;
            _ignore = false;
        });

    }
}
