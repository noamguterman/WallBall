using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public Storage Storage;
    public ParticleSystem ParticleSystem;
    public MainBall MainBall;
    public GenerateLevel GenerateLevel;
    public CameraFollow CameraFollow;
    public GameObject Parent;
    
    private void OnEnable()
    {
        if (Settings.GameType == 1)
        {
            Parent.SetActive(false);
            transform.position += Vector3.up * 10f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Settings.GameType == 1)
        {
            CameraFollow.transform.position = GenerateLevel.GetStartPos();
            MainBall.transform.position = GenerateLevel.GetStartPos();
            return;
        }
        
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
