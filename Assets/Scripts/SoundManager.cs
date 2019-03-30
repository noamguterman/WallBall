using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource jumpSFX;
    public AudioSource gameOverSFX;
    public AudioSource victorySFX;
    public AudioSource powerupSFX;
    public AudioSource buttonSFX;

    public void PlayPowerupSound()
    {
        powerupSFX.Play();
    }
}
