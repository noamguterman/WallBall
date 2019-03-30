using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayHandler : MonoBehaviour
{
    public GenerateLevel GenerateLevel;
    public GuiHandler GuiHandler;
    public static bool canRestart = true;
    public MainBall MainBall;
    
    private void Start()
    {
        GenerateLevel.Generate();
        canRestart = true;
    }

    public void Hit()
    {
        if (canRestart)
        {
            GuiHandler.ShowRestart();
            canRestart = false;
        }
        else
        {
            canRestart = true;
            GuiHandler.RestartLevel();
        }
    }

    private void OnHit()
    {
        SceneManager.LoadScene(0);
    }

    public void BoostStart()
    {
        var end = GenerateLevel.GetStartPos();
        var start = GenerateLevel.GetEndPos();
        var place = Vector3.Lerp(start, end, 0.5f);
        MainBall._active = false;
        MainBall._trail.gameObject.SetActive(true);
        MainBall.transform.DOMove(place, 2f).OnComplete(() =>
        {
            MainBall._trail.time = 0f;
            MainBall._active = true;
            MainBall._trail.gameObject.SetActive(false);
        });
    }

    public void StartSmaller()
    {
        MainBall.StartSmaller();
    }

}
