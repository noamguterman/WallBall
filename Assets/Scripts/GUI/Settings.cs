using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    //0 is Level
    //1 is Endless
    public static int GameType
    {
        get => PlayerPrefs.GetInt("GameType", 0);
        set => PlayerPrefs.SetInt("GameType", value);
    }

    public void SetLevelType()
    {
        GameType = 0;
        SceneManager.LoadScene(0);
    }

    public void SetEndless()
    {
        GameType = 1;
        SceneManager.LoadScene(0);
    }
    
    public void LoadLevel(int level)
    {
        PlayerPrefs.SetInt(Storage.Level, level);
       // SceneManager.LoadScene(0);
    }
}
