using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public GameObject VideoTry;
    
    public MainBall MainBall;

    public Text ProgressTxt;

    public GameObject TapTxt;
    
    private void Awake()
    {
        Progress.fillAmount = 0f;
        ProgressTxt.text = "0%";
        TotalGames++;
        Time.timeScale = 1;
        TapTxt.SetActive(Storage.AmountPlayed < 4);
    }

    private void Start()
    {
        UpdateCurrentLevel(Storage.AmountPlayed);
    }

    public static int TotalGames
    {
        get => PlayerPrefs.GetInt("TotalGames", -1);
        set => PlayerPrefs.SetInt("TotalGames", value);
    }
    

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        
        if (Input.GetMouseButtonUp(0))
        {
            Buttom.gameObject.SetActive(false);
        }
    }

    public void WatchVideo()
    {
        VideoTry.SetActive(false);
        MainBall.Continue();
    }

    public void ShowRestart()
    {
        Time.timeScale = 0;
        if (TotalGames == 5)
        {
            RateApp.SetActive(true);
        }
        else
        {
            if (IsBallFar())
            {
                VideoTry.SetActive(true);
            }
            else
            {
                RestartLevel();
            }
        }
    }

    private bool IsBallFar()
    {
        var pos = MainBall.transform.position;
        return Vector3.Distance(pos, GenerateLevel.GetStartPos()) > 20f;
    }

    public void RestartLevel()
    {
        PointCalculation.TotalPoints = 0f;
        //here should be best score
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
            ProgressTxt.text = "Completed: " + Mathf.Floor(Progress.fillAmount * 100) + "%";
        }

    }

    public void UpdatePoints(float points)
    {
        DistanceText.text = Mathf.Floor(points) + "";
    }

    public void UpdateCurrentLevel(int current)
    {
        CurrentLevel.text = current.ToString();
        NextLevel.text = (current + 1).ToString();
    }

    public void LoadShop()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadSelectLevel()
    {
        SceneManager.LoadScene(1);
    }
}
