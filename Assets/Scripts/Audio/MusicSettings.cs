using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicSettings : MonoBehaviour
{
    public Text txt;
    public AudioMixer Mixer;
    private void Awake()
    {
        if (Storage.IsMusic)
        {
            txt.text = "Music ON";
            Mixer.SetFloat("VolumeBG", 0);
        }
        else
        {
            Mixer.SetFloat("VolumeBG", -80);
            txt.text = "Music OFF";
        }
    }

    //called from Unity button
    public void Flip()
    {
        Storage.IsMusic = !Storage.IsMusic;
        Awake();
    }
}
