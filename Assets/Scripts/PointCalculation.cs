using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCalculation : MonoBehaviour
{
    public GuiHandler GuiHandler;
    public MainBall Ball;
    public GenerateLevel GenerateLevel;

    private Vector3 _lastPosition;
    private Vector3 _startPosition;

    public static float TotalPoints = 0f;
    public static float TotalPointsEndless = 0f;

    private void Start()
    {
        _startPosition = GenerateLevel.GetStartPos();
        _lastPosition = GenerateLevel.GetStartPos();
        GuiHandler.UpdateDistance(Vector3.Distance(_startPosition, _lastPosition));


        if (Settings.GameType == 1)
        {
            if (TotalPointsEndless < 1f)
            {
                var total = PlayerPrefs.GetFloat("TotalPointsEndless", TotalPointsEndless);
                GuiHandler.UpdatePoints(total);
            }
            else
            {
                GuiHandler.UpdatePoints(TotalPointsEndless);
            }
        }
        else
        {
            if (TotalPoints < 1f)
            {
                var total = PlayerPrefs.GetFloat("TotalPoints", TotalPoints);
                GuiHandler.UpdatePoints(total);
            }
            else
            {
                GuiHandler.UpdatePoints(TotalPoints);
            }
        }
    }

    void Update()
    {
        if (_lastPosition.y > Ball.transform.position.y)
        {
            IncreaseByDistance(_lastPosition.y - Ball.transform.position.y);
            _lastPosition = Ball.transform.position;
            GuiHandler.UpdateDistance(Vector3.Distance(_startPosition, _lastPosition));
        }
    }

    private void DecreaseByTouch()
    {
        if (_startPosition.y - Ball.transform.position.y > 1f)
        {
            if (Settings.GameType == 1)
            {
                TotalPointsEndless -= TotalPointsEndless / 10f;
                GuiHandler.UpdatePoints(TotalPointsEndless);
            }
            else
            {
                TotalPoints -= TotalPoints / 10f;
                GuiHandler.UpdatePoints(TotalPoints);
            }
        }
    }

    private void IncreaseByDistance(float distance)
    {
        if (Settings.GameType == 1)
        {
            TotalPointsEndless += distance * 10f;
            GuiHandler.UpdatePoints(TotalPointsEndless);
            var last = PlayerPrefs.GetFloat("TotalPointsEndless", 0f);
            if (last < TotalPointsEndless)
            {
                Debug.Log(TotalPointsEndless);
                PlayerPrefs.SetFloat("TotalPointsEndless", TotalPointsEndless);
                PlayerPrefs.Save();
            }
        }
        else
        {
            TotalPoints += distance * 10f;
            GuiHandler.UpdatePoints(TotalPoints);
            var last = PlayerPrefs.GetFloat("TotalPoints", 0f);
            if (last < TotalPoints)
            {
                PlayerPrefs.SetFloat("TotalPoints", TotalPoints);
            }
        }
    }

}
