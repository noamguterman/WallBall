using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public Transform Camera;
    public float ParalaxPower = 0.4f;
    private Vector3 StartPos;

    private bool _update;
    private IEnumerator Start()
    {
        yield return null;
        StartPos = Camera.position;
        _update = true;
    }

    private void Update()
    {
        if (_update == false)
            return;
        
        var currentPos = Camera.transform.position;

        transform.position = Vector3.Lerp(StartPos, currentPos, ParalaxPower);
        transform.position = new Vector3(transform.position.x, transform.position.y, 20f);
    }
}
