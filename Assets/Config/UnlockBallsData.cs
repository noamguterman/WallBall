using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class UnlockBallsData
{
    private static UnlockBallsData _instance;
    public static UnlockBallsData Instance
    {
        get
        {
            if (_instance == null)
            {
                var s = PlayerPrefs.GetString("UnlockBallsData", JsonUtility.ToJson(new UnlockBallsData()));
                _instance = JsonUtility.FromJson<UnlockBallsData>(s);
                _instance.openedVideo[0] = true;
            }

            return _instance;
        }
    }

    public void Open(int index)
    {
        openedVideo[index] = true;
        var json = JsonUtility.ToJson(_instance);
        PlayerPrefs.SetString("UnlockBallsData", json);
        PlayerPrefs.Save();
    }

    public int OpenRandom()
    {
        var locked = new List<int>();
        for (int i = amountForVideos; i < openedVideo.Length; i++)
        {
            if (openedVideo[i] == false)
            {
                locked.Add(i);
                Debug.Log(i);
            }
        }

        if (locked.Count == 0)
            return -1;

        var rand = Random.Range(0, locked.Count);
        Open(rand);
        return rand;
    }
    
    public int currentBall = 0;
    public static int amountForVideos = 16;
    public bool[] openedVideo = new bool[48];
}
