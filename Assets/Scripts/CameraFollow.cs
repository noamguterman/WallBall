using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _follow;
    public float Speed = 2f;
    private float startZ;

    private void OnEnable()
    {
        startZ = transform.position.z;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _follow.position, Time.deltaTime * Speed);
        transform.position = new Vector3(transform.position.x, transform.position.y, startZ);
    }
}
