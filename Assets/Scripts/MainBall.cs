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
    public TrailRenderer _trail;
    private float startGravity;
    public GamePlayHandler GamePlayHandler;
    public CameraFollow CameraFollow;
    private Color _startTrailColor;
    private Vector3 _lastPosition;

    public SpriteRenderer MainBallRenderer;
    public SpriteBallsSO Sprites;

    public SoundManager soundManager;
    
    
    private void Awake()
    {
        MainBallRenderer.sprite = Sprites._list[UnlockBallsData.Instance.currentBall];
        _rig = GetComponent<Rigidbody2D>();
        startGravity = _rig.gravityScale;
        _rig.gravityScale = 0;
//        transform.localScale = Vector3.one * Random.Range(1f, 2f);
        transform.localScale = Vector3.one;
        //Img.DOScale(new Vector3(0.7f, 1.5f, 1f), 0.6f).SetLoops(-1, LoopType.Yoyo);
        _trail = GetComponentInChildren<TrailRenderer>();
        _trail.gameObject.SetActive(false);
    }

    public void StartSmaller()
    {
        transform.localScale = Vector3.one * 0.5f;
        _trail.startWidth = transform.localScale.x * 0.7f;
    }

    private IEnumerator Start()
    {
        _startTrailColor = _trail.startColor;
        _trail.startWidth = transform.localScale.x * 0.6f;
        yield return null;
        _trail.gameObject.SetActive(true);
        _lastPosition = transform.position;
    }

    public bool _active = true;

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
            Jump();
        }

    }

    public void Jump()
    {
        _canLose = true;
        _rig.gravityScale = startGravity;
        _rig.velocity = Vector2.zero;
        if (FirstTap == false)
        {
            FirstTap = true;
            return;
        }
        _rig.AddForce(Vector2.up * PowerForMoveUp, ForceMode2D.Impulse);
        Animation();
        Increase();
    }

    private void UpdateColor()
    {
        var c =  Color.Lerp(_startTrailColor, new Color(114f / 255f, 14f / 255f, 14f / 255f, 255f / 255f), _rig.velocity.y / -15);
        SetColor(c);
    }

    private void SetColor(Color c)
    {
        _trail.startColor = c;
        _trail.endColor = c;
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

    public void TouchObstacle(Color start, Color end)
    {
        if (_canLose == false)
            return;
        
        if (IsInvisable == false)
        {
            _rig.gravityScale = 0;
            _rig.velocity = Vector3.zero;
            _active = false;
            Hit(start, end);
            StopAllCoroutines();
            StartCoroutine(Hit_Delay());
        }

        if (_canLose == true)
        {
            soundManager.gameOverSFX.Play();
        }
    }

    private IEnumerator Hit_Delay()
    {
        yield return new WaitForSeconds(1f);
        GamePlayHandler.Hit();
    }

    private void Hit(Color start, Color end)
    {
        Img.gameObject.SetActive(false);
        _trail.gameObject.SetActive(false);
        Particles.gameObject.SetActive(true);
        var part = Particles.GetComponent<ParticleSystem>();

        var col = part.colorOverLifetime;
        col.enabled = true;

        Gradient grad = new Gradient();
        grad.SetKeys( new GradientColorKey[] { new GradientColorKey(start, 0.0f), new GradientColorKey(end, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) } );

        col.color = grad;
        
        CameraFollow.Shake();
    }

    private bool FirstTap;
    
    private void Animation()
    {        
        Img.DOKill(true);
        Img.localScale = Vector3.one;
        var seq = DOTween.Sequence();
        seq.Append(Img.DOScale(Sprites.BallSettings.FirstScale, Sprites.BallSettings.TransitionSpeed1).SetLoops(2, LoopType.Yoyo));
        seq.Append(Img.DOScale(Sprites.BallSettings.SecondScale, Sprites.BallSettings.TransitionSpeed2).SetLoops(2, LoopType.Yoyo));
        seq.OnComplete(() => { Img.localScale = Vector3.one; });
        soundManager.jumpSFX.Play();
    }

    private bool IsTouch()
    {
        if (Time.timeScale < 0.1f)
            return false;

        if (EventSystem.current.IsPointerOverGameObject(0))
            return false;
        
        if (FirstTap == false)
        {
            return Input.GetMouseButtonUp(0);
        }
        else
        {
            return Input.GetMouseButtonDown(0);
        }
    }

    public void Finish()
    {
        _trail.gameObject.SetActive(false);
        _rig.gravityScale = 0;
        _rig.velocity = Vector3.zero;
        _active = false;
        Img.gameObject.SetActive(false);
        soundManager.victorySFX.Play();
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
        Invoke("SetFirstTapFalse", 0.1f);
    }

    void SetFirstTapFalse()
    {
        FirstTap = false;
    }
}
