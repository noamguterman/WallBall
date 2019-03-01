using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBall : MonoBehaviour
{
    private Rigidbody2D _rig;
    public float PowerForMoveUp = 1f;
    
    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsTouch())
        {
            _rig.AddForce(Vector2.up * PowerForMoveUp, ForceMode2D.Impulse);
        }
    }

    private bool IsTouch()
    {
        return Input.GetMouseButtonUp(0)
#if UNITY_EDITOR
               || Input.GetKeyUp(KeyCode.Space)
#endif
            ;
    }
}
