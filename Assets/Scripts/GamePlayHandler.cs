using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlayHandler : MonoBehaviour
{
    public GenerateLevel GenerateLevel;

    private void Start()
    {
        GenerateLevel.Generate();
    }

    private void OnHit()
    {
        SceneManager.LoadScene(0);
    }

}
