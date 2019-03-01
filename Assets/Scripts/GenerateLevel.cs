using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public Storage Storage;
    public AllSettings AllSettings;


    public GameObject MainBall;
    public GameObject PrefabLeftWall;
    public GameObject PrefabRightWall;

    public GameObject StartLeftWall;
    public GameObject StartRightWall;
    public GameObject StartBall;
    
    public void Generate()
    {
        float time = Storage.AmountPlayed;
        int amountOfWall = (int) AllSettings.AmountOfRingsOnSide.Evaluate(time);
        int ballHigh = (int) AllSettings.StartPosition.Evaluate(time);
        GenerateLevelArgs(amountOfWall, 4, 1, 1);
    }


    private void GenerateLevelArgs(int amountOfWalls, int ballHigh, float sizeOfBall, float speedOfBall)
    {
        MainBall.transform.position = StartBall.transform.position + Vector3.up * (ballHigh * 10);
        
        for (int i = 0; i < amountOfWalls; i++)
        {
            InitLeftWall(StartLeftWall.transform.position.y + (i * 10), sizeOfBall, speedOfBall);
            InitRightWall(StartRightWall.transform.position.y + (i * 10), sizeOfBall, speedOfBall);
        }
    }

    private void InitLeftWall(float y, float size, float speed)
    {
        var g = Instantiate(PrefabLeftWall);
        g.transform.position = new Vector3(StartLeftWall.transform.position.x, y);
    }

    private void InitRightWall(float y, float size, float speed)
    {
        var g = Instantiate(PrefabRightWall);
        g.transform.position = new Vector3(StartRightWall.transform.position.x, y);        
    }

}
