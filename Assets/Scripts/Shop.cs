using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    public SoundManager soundManager;

    public void Back()
    {
        soundManager.buttonSFX.Play();
        SceneManager.LoadScene(0);
    }
}
