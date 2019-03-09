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

    public Transform Buttom;

    public GameObject RateApp;

    private void Awake()
    {
        Progress.fillAmount = 0f;
    }

    private void Start()
    {
        UpdateCurrentLevel(Storage.AmountPlayed);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Buttom.gameObject.SetActive(false);
        }
    }

    public void ShowRestart()
    {
        RateApp.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateDistance(float distance)
    {
        var start = GenerateLevel.GetStartPos();
        var end = GenerateLevel.GetEndPos();

        var totalDistance = Vector3.Distance(start, end);
        if (Progress.fillAmount < distance / totalDistance)
        {
            Progress.fillAmount = distance / totalDistance;
        }

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
