using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class StopBallTrigger : MonoBehaviour
{

    private bool isEntered;
    private MainBall ball;
    private void Update()
    {
        if (isEntered == true)
        {
            if (Input.GetMouseButtonUp(0))
            {
                GuiHandler.Instance.Tap2.SetActive(false);
                Time.timeScale = 1f;
                ball.Jump();
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isEntered == false)
        {
            ball = other.GetComponent<MainBall>();
            if (ball != null)
            {
                GuiHandler.Instance.Tap2.SetActive(true);
                Time.timeScale = 0f;
                isEntered = true;
            }
        }
    } 
}
