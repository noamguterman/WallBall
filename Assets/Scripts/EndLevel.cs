using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public Storage Storage;
    public ParticleSystem ParticleSystem;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var ball = other.GetComponent<MainBall>();
        if (ball)
        {
            ParticleSystem.gameObject.SetActive(true);
            ball.Finish();
            Storage.Increase();
            StartCoroutine(EndLevel_Routine(ball));
        }
    }

    private IEnumerator EndLevel_Routine(MainBall ball)
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
