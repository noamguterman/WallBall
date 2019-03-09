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
    private void Start()
    {
        _startPosition = GenerateLevel.GetStartPos();
        _lastPosition = GenerateLevel.GetStartPos();
        GuiHandler.UpdateDistance(Vector3.Distance(_startPosition, _lastPosition));
    }

    void Update()
    {
        if (_lastPosition.y > Ball.transform.position.y)
        {
            _lastPosition = Ball.transform.position;
            GuiHandler.UpdateDistance(Vector3.Distance(_startPosition, _lastPosition));
        }
    }

}
