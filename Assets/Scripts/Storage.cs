using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    public int AmountPlayed;

    public const string Level = "Level";
    private void Awake()
    {
#if UNITY_EDITOR
        //return;
#endif
        AmountPlayed = PlayerPrefs.GetInt(Level);
    }

    public void Increase()
    {
        AmountPlayed++;
        PlayerPrefs.SetInt(Level, AmountPlayed);
    }
}
