using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiHandler : MonoBehaviour
{
    public Text DistanceText;
    public void UpdateDistance(float distance)
    {
        DistanceText.text = distance + "";
    }
}
