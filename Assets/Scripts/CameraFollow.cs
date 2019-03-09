using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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

    void Update()
    {
        if (_follow.position.y > LowestPos.position.y)
        {
            transform.position = Vector3.Lerp(transform.position, _follow.position, Time.deltaTime * Speed);
            transform.position = new Vector3(transform.position.x, transform.position.y, startZ);
        }
        else
        {
            var brain = GetComponent<CinemachineBrain>();
            brain.enabled = false;
        }
    }
}
