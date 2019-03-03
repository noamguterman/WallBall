using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GuiHandler : MonoBehaviour
{
    public Text DistanceText;

    public Text CurrentLevel;
    public Text NextLevel;

    public Image Progress;

    public GenerateLevel GenerateLevel;
    public Storage Storage;
    
    private void Start()
    {
        UpdateCurrentLevel(Storage.AmountPlayed);
    }

    public void UpdateDistance(float distance)
    {
        var start = GenerateLevel.GetStartPos();
        var end = GenerateLevel.GetEndPos();

        var totalDistance = Vector3.Distance(start, end);
        
        Progress.fillAmount = distance / totalDistance;
        DistanceText.text = distance + "";
    }

    public void UpdateCurrentLevel(int current)
    {
        CurrentLevel.text = current.ToString();
        NextLevel.text = (current + 1).ToString();
    }

    public void LoadSelectLevel()
    {
        SceneManager.LoadScene(1);
    }
}
