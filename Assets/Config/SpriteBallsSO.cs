using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteBallsSO", menuName = "Settings/SpriteBallsSO", order = 1)]

public class SpriteBallsSO : ScriptableObject
{
    public BallSettings BallSettings;
    public Sprite[] _list;
}


[System.Serializable]
public class BallSettings
{
    public Vector3 FirstScale = new Vector3(0.5f, 2f, 1f);
    public float TransitionSpeed1 = 0.1f;
    public Vector3 SecondScale = new Vector3(2f, 0.5f, 1f);
    public float TransitionSpeed2 = 0.1f;
}