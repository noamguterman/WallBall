using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpawnWallBalls : MonoBehaviour
{
    public float Direction;
    public float Delay;
    public GameObject Prefab;
    public float SpeedBullet = 1f;
    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(Delay);
            Spawn();
        }
    }

    private void Spawn()
    {
        var g = Instantiate(Prefab, transform.position, Quaternion.identity);
        g.transform.DOMoveX(g.transform.position.x + Direction, SpeedBullet);
        Destroy(g, 10f);
    }

}
