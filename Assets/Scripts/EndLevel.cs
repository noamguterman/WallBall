using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public Storage Storage;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var ball = other.GetComponent<MainBall>();
        if (ball)
        {
            StartCoroutine(EndLevel_Routine(ball));
        }
    }

    private IEnumerator EndLevel_Routine(MainBall ball)
    {
        ball.Finish();
        Storage.Increase();
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(0);
    }
}
