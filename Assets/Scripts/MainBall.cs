using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBall : MonoBehaviour
{
    private Rigidbody2D _rig;
    public float PowerForMoveUp = 1f;
    public Transform Img;
    
    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsInvisable == true)
            return;
        
        if (IsTouch())
        {
            _rig.velocity = Vector2.zero;
            _rig.AddForce(Vector2.up * PowerForMoveUp, ForceMode2D.Impulse);
            Animation();
        }
    }

    private bool IsInvisable;

    public void MoveDown(float Distance)
    {
        if (IsInvisable)
            return;
        
        transform.DOKill(true);
        IsInvisable = true;
        _rig.velocity = Vector3.zero;
        transform.DOMoveY(transform.position.y - Distance, 1f).OnComplete(() =>
        {
            IsInvisable = false;
            _rig.velocity = Vector3.zero;
            transform.localScale = Vector3.one;
        });
    }

    public void TouchObstacle()
    {
        if (IsInvisable == false)
            SceneManager.LoadScene(0);
    }

    private void Animation()
    {
        Img.DOKill(true);
        Img.localScale = Vector3.one;
        var seq = DOTween.Sequence();
        seq.Append(Img.DOScale(new Vector3(0.5f, 2f, 1f), 0.1f).SetLoops(2, LoopType.Yoyo));
        seq.Append(Img.DOScale(new Vector3(2f, 0.5f, 1f), 0.1f).SetLoops(2, LoopType.Yoyo));
        seq.OnComplete(() => { Img.localScale = Vector3.one; });
    }

    private bool IsTouch()
    {
        return Input.GetMouseButtonUp(0)
#if UNITY_EDITOR
               || Input.GetKeyUp(KeyCode.Space)
#endif
            ;
    }

    private void OnDisable()
    {
        Img.DOKill(false);
    }
}
