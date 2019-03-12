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

    public Transform Parent;
    private Color _randColor;

    private void Awake()
    {
        _randColor =  Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    IEnumerator Start()
    {
        RandomSpritesColor();
        _renderer = GetComponent<SpriteRenderer>();
        //while (true)
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

    private void RandomSpritesColor()
    {
        var c = GetComponentsInChildren<SpriteRenderer>();
        foreach (var a in c)
        {
            a.color = _randColor;
        }

        var t = transform.Find("Sprites");
        t.localScale = Vector3.one * Size * 0.8f;
        t = transform.Find("Mask");
        t.transform.localScale = new Vector3(Size * 0.8f, 1f, 1f);
    }

    private void Spawn()
    {
        var g = Instantiate(Prefab, transform.position, Quaternion.identity);
        g.transform.localScale = Vector3.one * Size;
        g.transform.parent = Parent;

        var rig = g.GetComponent<Rigidbody2D>();
        rig.AddForce(Vector2.right * Direction * SpeedBullet);
        g.GetComponent<SpriteRenderer>().color = _randColor;
        //g.transform.DOMoveX(g.transform.position.x + Direction, SpeedBullet);
        //Destroy(g, 10f);
    }

}
