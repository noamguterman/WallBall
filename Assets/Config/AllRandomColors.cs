using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RingColors", menuName = "Settings/RingColors", order = 1)]
public class AllRandomColors: ScriptableObject
{
    public List<RandomColors> List;
}
    
[System.Serializable]
public class RandomColors
{
    public Color start;
    public Color end;

    public RandomColors(Color start, Color end)
    {
        this.start = start;
        this.end = end;
    }
}

