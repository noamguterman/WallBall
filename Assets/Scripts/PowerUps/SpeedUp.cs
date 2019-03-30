using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    public float MoveDown = 5f;
    public SoundManager soundManager;

    private void Start()
    {
        transform.DOMoveX(transform.position.x + 10f, 4f).SetLoops(-1, LoopType.Yoyo);
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var ball = other.GetComponent<MainBall>();
        if (ball != null)
        {
            ball.MoveDown(MoveDown);
            soundManager.PlayPowerupSound();
            gameObject.SetActive(false);
        }
    }
}