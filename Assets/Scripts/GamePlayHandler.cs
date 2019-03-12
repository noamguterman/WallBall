using System.Collections;
using System.Collections.Generic;
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
        var place = Vector3.Lerp(start, end, 0.7f);
        MainBall.transform.position = place;
    }

    public void StartSmaller()
    {
        MainBall.StartSmaller();
    }

}
