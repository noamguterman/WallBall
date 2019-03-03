using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public void LoadLevel(int level)
    {
        PlayerPrefs.SetInt(Storage.Level, level);
        SceneManager.LoadScene(0);
    }
}
