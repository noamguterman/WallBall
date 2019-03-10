﻿using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBall : MonoBehaviour
{
    private Rigidbody2D _rig;
    public float PowerForMoveUp = 1f;
    public Transform Img;
    public Transform Particles;
    public GenerateLevel GenerateLevel;
    private TrailRenderer _trail;
    private float startGravity;
    public GamePlayHandler GamePlayHandler;
    public CameraFollow CameraFollow;

    
    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        startGravity = _rig.gravityScale;
        _rig.gravityScale = 0;
        Img.DOScale(new Vector3(0.7f, 1.5f, 1f), 0.6f).SetLoops(-1, LoopType.Yoyo);
        _trail = GetComponentInChildren<TrailRenderer>();
    }

    private IEnumerator Start()
    {
        _trail.gameObject.SetActive(false);
        yield return null;
        _trail.gameObject.SetActive(true);
    }

    private bool _active = true;

    void Update()
    {
        if (_active == false)
            return;
        
        _trail.gameObject.SetActive(_rig.velocity.y < 0f);

        if (IsInvisable == true)
            return;
        
        if (IsTouch())
        {
            _canLose = true;
            _rig.gravityScale = startGravity;
            _rig.velocity = Vector2.zero;
            _rig.AddForce(Vector2.up * PowerForMoveUp, ForceMode2D.Impulse);
            Animation();
            Increase();
        }
    }

    private void Increase()
    {
        transform.DOScale(transform.localScale + Vector3.one * 0.1f, 0.2f);
        _trail.startWidth += 0.05f;
    }

    private bool IsInvisable;

    public void MoveDown(float Distance)
    {
        if (Img.gameObject.activeSelf == false)
            return;
        
        if (IsInvisable)
            return;

        _canLose = false;
        
        transform.DOKill(true);
        IsInvisable = true;
        _rig.velocity = Vector3.zero;
        //GenerateLevel.DestroyWallsInRange( startMove,startMove - Distance, true);

        transform.DOMoveY(transform.position.y - Distance, 1f).OnComplete(() =>
        {
            IsInvisable = false;
            _rig.velocity = Vector3.zero;
            Img.transform.localScale = Vector3.one;
            _canLose = false;
            Invoke("InvisibleDisable", 2f);
        });
    }

    private void InvisibleDisable()
    {
        _canLose = true;
    }

    private bool _canLose = true;

    public void TouchObstacle()
    {
        if (_canLose == false)
            return;
        
        if (IsInvisable == false)
        {
            _rig.gravityScale = 0;
            _rig.velocity = Vector3.zero;
            _active = false;
            Hit();
            StopAllCoroutines();
            StartCoroutine(Hit_Delay());
        }
    }

    private IEnumerator Hit_Delay()
    {
        yield return new WaitForSeconds(1f);
        GamePlayHandler.Hit();
    }

    private void Hit()
    {
        Img.gameObject.SetActive(false);
        _trail.gameObject.SetActive(false);
        Particles.gameObject.SetActive(true);
        CameraFollow.Shake();
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
        if (Time.timeScale < 0.1f)
            return false;
        
        return Input.GetMouseButtonDown(0)
#if UNITY_EDITOR
               || Input.GetKeyDown(KeyCode.Space)
#endif
            ;
    }

    public void Finish()
    {
        _trail.gameObject.SetActive(false);
        _rig.gravityScale = 0;
        _rig.velocity = Vector3.zero;
        _active = false;
        Img.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _active = false;
        Img.DOKill(false);
        transform.DOKill();
    }

    public void Continue()
    {
        _active = true;
        Particles.gameObject.SetActive(false);
        Img.gameObject.SetActive(true);
        Img.DOKill(false);
        Img.gameObject.SetActive(true);
        _rig.gravityScale = 0;
        _canLose = false;
        Time.timeScale = 1;
    }
    
    
}
