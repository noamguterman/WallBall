using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockBall : MonoBehaviour
{
    public int index = 0;

    public SpriteBallsSO data;
    public Image renderer;

    private void OnEnable()
    {
        bool isOpen = UnlockBallsData.Instance.openedVideo[index];
        if (isOpen)
        {
            renderer.sprite = data._list[index];
            renderer.color = Color.white;
        }
    }

    public void Click()
    {
        UnlockBallsData.Instance.Open(index);
        UnlockBallsData.Instance.currentBall = index;
        OnEnable();
    }
}
