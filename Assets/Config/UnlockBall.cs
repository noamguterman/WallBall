using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockBall : MonoBehaviour
{
    public int index = 0;

    public SpriteBallsSO data;
    public Image renderer;

    public bool IsCareer;

    private void OnEnable()
    {
        bool isOpen = UnlockBallsData.Instance.openedVideo[index];
        if (isOpen)
        {
            renderer.sprite = data._list[index];
            renderer.color = Color.white;
            transform.Find("Image").gameObject.SetActive(false);
        }
    }

    public void Click()
    {
        if (IsCareer == true)
        {
            bool isOpen = UnlockBallsData.Instance.openedVideo[index];
            if (isOpen)
            {
                renderer.sprite = data._list[index];
                renderer.color = Color.white;
                UnlockBallsData.Instance.currentBall = index;
                transform.Find("Image").gameObject.SetActive(false);
            }
        }
        else
        {
            //show ads
            bool isOpen = UnlockBallsData.Instance.openedVideo[index];
            {
                renderer.sprite = data._list[index];
                renderer.color = Color.white;
                UnlockBallsData.Instance.currentBall = index;
                transform.Find("Image").gameObject.SetActive(false);
            }
        }
    }
}
