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
    
    private void Start()
    {
        _startPosition = GenerateLevel.GetStartPos();
        _lastPosition = GenerateLevel.GetStartPos();
        GuiHandler.UpdateDistance(Vector3.Distance(_startPosition, _lastPosition));
        GuiHandler.UpdatePoints(TotalPoints);
    }

    void Update()
    {
        if (_lastPosition.y > Ball.transform.position.y)
        {
            IncreaseByDistance(_lastPosition.y - Ball.transform.position.y);
            _lastPosition = Ball.transform.position;
            GuiHandler.UpdateDistance(Vector3.Distance(_startPosition, _lastPosition));
        }

        if (Input.GetMouseButtonUp(0))
        {
            DecreaseByTouch();
        }
    }

    private void DecreaseByTouch()
    {
        if (_startPosition.y - Ball.transform.position.y > 1f)
        {
            TotalPoints -= TotalPoints / 10f;
            GuiHandler.UpdatePoints(TotalPoints);
        }
    }

    private void IncreaseByDistance(float distance)
    {
        TotalPoints += distance;
        GuiHandler.UpdatePoints(TotalPoints);
    }

}
