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
    public Chunck Chunck3;

    public LevelSettings LevelSettings; 

    private List<GameObject> _allRandomWalls = new List<GameObject>();
    private List<GameObject> chunks = new List<GameObject>();
    public void Generate()
    {
        float time = Storage.AmountPlayed;
        int amountOfWall = (int) AllSettings.AmountOfRingsOnSide.Evaluate(time);
        var ballHigh = AllSettings.StartPosition.Evaluate(time);
        var ballSize = AllSettings.SizeOfSideBalls.Evaluate(time);
        var speed = AllSettings.BallObstacleSpeed.Evaluate(time);
        GenerateLevelArgs(amountOfWall, ballHigh, ballSize, speed);

        var level = LevelSettings.GetLevel(Storage.AmountPlayed);
        var startPos = StartBall.transform.position;
        var endPos = StartBall.transform.position + Vector3.up * (ballHigh);
        
        GeneratePowerUps(StartLeftWall.transform.position,
            StartBall.transform.position + Vector3.up * (ballHigh), level.PowerUpAmount);


        if (level.AmountChunk1)
        {
            GenerateChucks(Chunck1, startPos, endPos / 3,
                ballSize, speed);
        }

        if (level.AmountChunk2)
        {
            GenerateChucks(Chunck2, startPos + endPos / 3, endPos * 2 / 3,
                ballSize, speed);
        }

        if (level.AmountChunk3)
        {
            GenerateChucks(Chunck3, startPos + endPos * 2 / 3, endPos,
                ballSize, speed);
        }

        RemoveDublicates();
    }

    private void RemoveDublicates()
    {
        for (int i = 0; i < _allRandomWalls.Count; i++)
        {
            var wall = _allRandomWalls[i];
            var list = _allRandomWalls.FindAll(_ =>
            {
                if (_ != null)
                {
                    if (_ != wall)
                    {
                        var dif = Mathf.Abs(_.transform.position.y - wall.transform.position.y);
                        return dif < 1f;
                    }
                }
                return false;
            });
            
            foreach (var o in list)
            {
                Destroy(o.gameObject);
            }
        }
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
        chunks.Add(c.gameObject);
        var high = c.High.position.y;
        var low = c.Low.position.y;

        DestroyWallsInRange(high, low, false);
        
        var walls = c.GetComponentsInChildren<SpawnWallBalls>();
        foreach (var wall in walls)
        {
            wall.Size = size;
            wall.SpeedBullet = speed;
            _allRandomWalls.Add(wall.gameObject);
        }
    }

    public void DestroyWallsInRange(float high, float low, bool withChunks)
    {
        var list = _allRandomWalls.FindAll((o => { return o.transform.position.y < high && o.transform.position.y > low; }));
        if (withChunks)
        {
            foreach (var chunk in chunks)
            {
                var l = chunk.GetComponentsInChildren<SpawnWallBalls>();
                for (int i = 0; i < l.Length; i++)
                {
                    list.Add(l[i].gameObject);
                }
            }
        }

        foreach (var o in list)
        {
            _allRandomWalls.Remove(o);
            Destroy(o.gameObject);
        }
    }

    public Vector3 GetStartPos()
    {
        float time = Storage.AmountPlayed;
        var ballHigh = AllSettings.StartPosition.Evaluate(time);
        return StartBall.transform.position + Vector3.up * (ballHigh) + Vector3.up * 10f;
    }

    public Vector3 GetEndPos()
    {
        return StartBall.transform.position;
    }

    private void GenerateLevelArgs(int amountOfWalls, float ballHigh, float sizeOfBall, float speedOfBall)
    {
        float offset = ballHigh / (float) amountOfWalls;

        MainBall.transform.position = StartBall.transform.position + Vector3.up * (ballHigh) + Vector3.up * 10f;
        
        for (int i = 0; i < amountOfWalls; i++)
        {
            InitLeftWall(StartLeftWall.transform.position.y + (i * offset), sizeOfBall, speedOfBall);
            InitRightWall(StartRightWall.transform.position.y + (i * offset), sizeOfBall, speedOfBall);
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
