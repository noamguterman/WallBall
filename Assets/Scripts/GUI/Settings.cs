using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{

    public UnityEngine.UI.Image Level;
    public UnityEngine.UI.Image Endless;

    public SoundManager soundManager;
    
    //0 is Level
    //1 is Endless
    public static int GameType
    {
        get => PlayerPrefs.GetInt("GameType", 0);
        set => PlayerPrefs.SetInt("GameType", value);
    }


    private void Update()
    {
        Level.gameObject.SetActive(GameType == 0);
        Endless.gameObject.SetActive(GameType == 1);
    }

    public void SetLevelType()
    {
        GameType = 0;
        soundManager.buttonSFX.Play();
        SceneManager.LoadScene(0);
    }

    public void SetEndless()
    {
        GameType = 1;
        soundManager.buttonSFX.Play();
        SceneManager.LoadScene(0);
    }

    public void Back()
    {
        soundManager.buttonSFX.Play();
        SceneManager.LoadScene(0);
    }
    
    public void LoadLevel(int level)
    {
        GuiHandler.TotalGames = 0;
        PlayerPrefs.SetInt(Storage.Level, level);
       // SceneManager.LoadScene(0);
    }
}
