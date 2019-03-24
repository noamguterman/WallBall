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
    public GuiHandler GuiHandler;
    
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

        
        var ball = other.GetComponent<MainBall>();
        if (ball)
        {
            
            if (Settings.GameType == 1)
            {
                CameraFollow.transform.position = GenerateLevel.GetStartPos();
                MainBall.transform.position = GenerateLevel.GetStartPos();
                return;
            }
            
            ParticleSystem.gameObject.SetActive(true);
            ball.Finish();
            Storage.Increase();
            StartCoroutine(EndLevel_Routine(ball));
        }
    }

    private IEnumerator EndLevel_Routine(MainBall ball)
    {
        yield return new WaitForSeconds(2f);
        if (Storage.AmountPlayed % 5 == 0)
        {
            GuiHandler.ShowOpenNewBall();
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
