using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class DeadTrigger : MonoBehaviour
{
    private Vector3 _startScale;
    private SpriteRenderer _renderer;

    public Color endColor;
    private IEnumerator Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        //Need a frame because of spawnWallBalls
        yield return null;
        _startScale = transform.localScale;
        transform.DOScale(_startScale * 1.2f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        var start = _renderer.color;
        var rand = Color.Lerp(start, endColor, Random.Range(0, 0.5f));
        _renderer.DOColor(rand, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        transform.DOKill();
        _renderer.DOKill();
    }

    private void OnDisable()
    {
        transform.DOKill();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var ball = other.GetComponent<MainBall>();
        if (ball != null)
        {
            ball.TouchObstacle();
        }
    } 
}
