using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerToSeeAdd : MonoBehaviour
{

    public GuiHandler GuiHandler;

    public Image Img;
    private Text _txt;
    private System.DateTime _call;
    
    private void OnEnable()
    {
        _txt = GetComponent<Text>();
        _call = System.DateTime.Now;
    }

    private void Update()
    {
        var t = (System.DateTime.Now - _call);

        _txt.text = "" + (7f - Mathf.Floor((float) t.TotalSeconds));

        Img.fillAmount = (7f - (float) t.TotalSeconds) / 7f;
        Img.color = new Color(Img.color.r, Img.color.g, Img.color.b, Img.fillAmount);
        if (t.TotalSeconds > 7.3f)
        {
            GuiHandler.RestartLevel();
        }
    }
}


