using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayHandler : MonoBehaviour
{
    public GenerateLevel GenerateLevel;
    public GuiHandler GuiHandler;
    
    private void Start()
    {
        GenerateLevel.Generate();
    }

    public void Hit()
    {
        GuiHandler.ShowRestart();
    }

    private void OnHit()
    {
        SceneManager.LoadScene(0);
    }

}
