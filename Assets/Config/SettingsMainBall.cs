using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMainBall : MonoBehaviour
{
    public SpriteBallsSO SO;
    public Image img;
    
    private void Update()
    {
        var current = UnlockBallsData.Instance.currentBall;
        img.sprite = SO._list[current];
    }
}
