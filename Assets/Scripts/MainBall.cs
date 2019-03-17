using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
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
    private Color _startTrailColor;
    private Vector3 _lastPosition;
    
    
    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        startGravity = _rig.gravityScale;
        _rig.gravityScale = 0;
        Img.DOScale(new Vector3(0.7f, 1.5f, 1f), 0.6f).SetLoops(-1, LoopType.Yoyo);
        _trail = GetComponentInChildren<TrailRenderer>();
        _trail.gameObject.SetActive(false);
    }

    public void StartSmaller()
    {
        transform.localScale = Vector3.one * 0.7f;
        _trail.startWidth = transform.localScale.x * 0.6f;
    }

    private IEnumerator Start()
    {
        _startTrailColor = _trail.startColor;
        _trail.startWidth = transform.localScale.x * 0.6f;
        yield return null;
        _trail.gameObject.SetActive(true);
        _lastPosition = transform.position;

    }

    private bool _active = true;

    void Update()
    {
        if (_active == false)
            return;
        
        _trail.gameObject.SetActive(_rig.velocity.y < 0f);

        if (IsInvisable == true)
            return;
        
        UpdateColor();
        
        if (_lastPosition.y > transform.position.y)
        {
            _lastPosition = transform.position;
        }

        
        if (_lastPosition.y + 5 < transform.position.y)
        {
            return;
        }
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

    private void UpdateColor()
    {
        var c =  Color.Lerp(_startTrailColor, Color.red, _rig.velocity.y / -20);
        SetColor(c);
    }

    private void SetColor(Color c)
    {
        _trail.startColor = c;
        _trail.endColor = c;
        Img.GetComponent<SpriteRenderer>().color = c;
    }

    private void Increase()
    {
        if (transform.localScale.x < 5f)
        {
            transform.DOScale(transform.localScale + Vector3.one * 0.1f, 0.2f);
            _trail.startWidth += 0.05f;
        }
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

    private bool _canLose = false;

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

        if (EventSystem.current.IsPointerOverGameObject())
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
        if (transform.localScale.x > 1f)
        {
            transform.localScale = Vector3.one;
        }
            
        _active = true;
        Particles.gameObject.SetActive(false);
        Img.gameObject.SetActive(true);
        Img.DOKill(false);
        Img.gameObject.SetActive(true);
        
        _trail.startWidth = transform.localScale.x * 0.6f;

        _rig.gravityScale = 0;
        _canLose = false;
        Time.timeScale = 1;
    }
    
    
}
