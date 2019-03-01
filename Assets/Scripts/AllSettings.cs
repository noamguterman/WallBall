using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSettings : MonoBehaviour
{
//1.starting position of character - aka game field length 
//2.amount of side rings shooging obstacle balls - for each side
//3.size of the side balls obstacle
//4.speed of side balls obstacles
    public AnimationCurve StartPosition = AnimationCurve.Linear(0f, 0f, 100f, 100f);
    public AnimationCurve AmountOfRingsOnSide =  AnimationCurve.Linear(0f, 0f, 100f, 100f);
    public AnimationCurve SizeOfSideBalls =  AnimationCurve.Linear(0f, 0f, 100f, 100f);
    public AnimationCurve BallObstacleSpeed =  AnimationCurve.Linear(0f, 0f, 100f, 100f);
}
