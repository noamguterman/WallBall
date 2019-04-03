using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXSettings : MonoBehaviour
{
    public Text txt;
    public AudioMixer Mixer;

    private void Awake()
    {
        if (Storage.IsSfx)
        {
            txt.text = "SFX ON";
            Mixer.SetFloat("VolumeSFX", 0);
        }
        else
        {
            txt.text = "SFX OFF";
            Mixer.SetFloat("VolumeSFX", -80);
        }
    }
    
    //called from Unity button
    public void Flip()
    {
        Storage.IsSfx = !Storage.IsSfx;
        Awake();
    }
}
