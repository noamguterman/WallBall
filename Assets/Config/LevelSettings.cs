using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelSettings", menuName = "Settings/LevelSettings", order = 1)]
public class LevelSettings : ScriptableObject
{
    public List<Level> Levels;

    public Level GetLevel(int index)
    {
        if (index >= Levels.Count)
        {
            index = Levels.Count - 1;
        }

        return Levels[index];
    }
}

[System.Serializable]
public class Level
{
    public bool AmountChunk1;
    public bool AmountChunk2;
    public bool AmountChunk3;

    public int PowerUpAmount = 0;
}
