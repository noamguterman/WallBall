using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public int AmountPlayed;

    public static bool IsSfx
    {
        get { return PlayerPrefs.GetInt("sfx", 0) == 0; }
        set
        {
            if (value)
            {
                PlayerPrefs.SetInt("sfx", 0);
            }
            else
            {
                PlayerPrefs.SetInt("sfx", 1);
            }
        }
    }
    
    public static bool IsMusic
    {
        get { return PlayerPrefs.GetInt("music", 0) == 0; }
        set
        {
            if (value)
            {
                PlayerPrefs.SetInt("music", 0);
            }
            else
            {
                PlayerPrefs.SetInt("music", 1);
            }
        }
    }

    public const string Level = "Level";
    private void Awake()
    {
#if UNITY_EDITOR
        return;
#endif
        AmountPlayed = PlayerPrefs.GetInt(Level);
    }

    public void Increase()
    {
        AmountPlayed++;
        PlayerPrefs.SetInt(Level, AmountPlayed);
    }
}
