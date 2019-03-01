using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayHandler : MonoBehaviour
{
    public GenerateLevel GenerateLevel;

    private void Start()
    {
        GenerateLevel.Generate();
    }
}
