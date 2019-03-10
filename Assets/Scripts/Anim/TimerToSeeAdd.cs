using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerToSeeAdd : MonoBehaviour
{

    public GuiHandler GuiHandler;

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

        _txt.text = "" + (5f - Mathf.Floor((float) t.TotalSeconds));
        if (t.TotalSeconds > 5.3f)
        {
            GuiHandler.RestartLevel();
        }
    }
}


