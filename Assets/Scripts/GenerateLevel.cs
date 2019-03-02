using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public Storage Storage;
    public AllSettings AllSettings;

    public GameObject MainBall;
    public SpawnWallBalls PrefabLeftWall;
    public SpawnWallBalls PrefabRightWall;
    public SpeedUp SpeedUpPrefab;
    
    public GameObject StartLeftWall;
    public GameObject StartRightWall;
    public GameObject StartBall;

    public Chunck Chunck1;
    public Chunck Chunck2;

    private List<GameObject> _allRandomWalls = new List<GameObject>();
    
    public void Generate()
    {
        float time = Storage.AmountPlayed;
        int amountOfWall = (int) AllSettings.AmountOfRingsOnSide.Evaluate(time);
        var ballHigh = AllSettings.StartPosition.Evaluate(time);
        var ballSize = AllSettings.SizeOfSideBalls.Evaluate(time);
        var speed = AllSettings.BallObstacleSpeed.Evaluate(time);
        GenerateLevelArgs(amountOfWall, ballHigh, ballSize, speed);
        GeneratePowerUps(StartLeftWall.transform.position, 
            StartBall.transform.position + Vector3.up * (ballHigh * 10), 2);
        GenerateChucks(Chunck1, StartBall.transform.position, StartBall.transform.position + Vector3.up * (ballHigh * 5),
            ballSize, speed);
        
        GenerateChucks(Chunck2, StartBall.transform.position + StartBall.transform.position + Vector3.up * (ballHigh * 5), 
            StartBall.transform.position + Vector3.up * (ballHigh * 10) - Vector3.up * 10f,
            ballSize, speed);
    }

    private void GeneratePowerUps(Vector3 startPos, Vector3 maxPos, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var y = Random.Range(startPos.y, maxPos.y);
            var g = Instantiate(SpeedUpPrefab, new Vector3(startPos.x, y, startPos.z), Quaternion.identity);
        }
    }

    private void GenerateChucks(Chunck chunck, Vector3 start, Vector3 end, float size, float speed)
    {
        var randomPos = Random.Range(start.y, end.y);

        var c =Instantiate(chunck, new Vector3(start.x, randomPos, start.z), Quaternion.identity);
        
        var high = c.High.position.y;
        var low = c.Low.position.y;
        
        var list = _allRandomWalls.FindAll((o => { return o.transform.position.y < high && o.transform.position.y > low; }));
        foreach (var o in list)
        {
            _allRandomWalls.Remove(o);
            Destroy(o.gameObject);
        }
        
        var walls = c.GetComponentsInChildren<SpawnWallBalls>();
        foreach (var wall in walls)
        {
            wall.Size = size;
            wall.SpeedBullet = speed;
        }
    }

    public Vector3 GetStartPos()
    {
        float time = Storage.AmountPlayed;
        var ballHigh = AllSettings.StartPosition.Evaluate(time);
        return StartBall.transform.position + Vector3.up * (ballHigh * 10);
    }

    private void GenerateLevelArgs(int amountOfWalls, float ballHigh, float sizeOfBall, float speedOfBall)
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
        y += Random.Range(-2f, 2f);
        g.transform.position = new Vector3(StartLeftWall.transform.position.x, y);
        g.Size = size;
        g.SpeedBullet = speed;
        _allRandomWalls.Add(g.gameObject);
    }

    private void InitRightWall(float y, float size, float speed)
    {
        var g = Instantiate(PrefabRightWall);
        y += Random.Range(-7f, 7f);
        g.transform.position = new Vector3(StartRightWall.transform.position.x, y);        
        g.Size = size;
        g.SpeedBullet = speed;
        _allRandomWalls.Add(g.gameObject);

    }

}
