using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SpawnWallBalls : MonoBehaviour
{
    public float Direction;
    public float Delay;
    [SerializeField] private GameObject Prefab;
    public float SpeedBullet = 1f;
    public float Size = 1f;
    private SpriteRenderer _renderer;
    
    IEnumerator Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        while (true)
        {
            _renderer.DOKill(true);
            _renderer.DOColor(Color.red, Delay / 2);
            yield return new WaitForSeconds(Delay / 2 + Random.Range(0f, 0.2f));
            _renderer.DOKill(true);
            _renderer.DOColor(Color.white, Delay / 2);
            Spawn();
            yield return new WaitForSeconds(Delay / 2 + Random.Range(0f, 0.2f));
        }
    }

    private void Spawn()
    {
        var g = Instantiate(Prefab, transform.position, Quaternion.identity);
        g.transform.localScale = Vector3.one * Size;
        g.transform.DOMoveX(g.transform.position.x + Direction, SpeedBullet);
        Destroy(g, 10f);
    }

}
