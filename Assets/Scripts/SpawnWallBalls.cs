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

    private Rigidbody2D _rig;

    private void Awake()
    {
        _randColor =  Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    IEnumerator Start()
    {
        yield return null;
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

        if (Size > 2f)
        {
            Size = 2f;
        }

        if (Size < 0.8f)
        {
            Size = 0.8f;
        }

        var t = transform.Find("Sprites");
        t.localScale = new Vector3(Size * 0.6f, Size * 0.45f);
        t = transform.Find("Mask");
        t.transform.localScale = new Vector3(Size * 0.7f, 1f, 1f);
    }

    private void Update()
    {
        if (_rig == null)
            return;
        
        if (Settings.GameType == 1)
        {
            if (Time.deltaTime > float.Epsilon)
            {
                _rig.velocity *= 1 + Time.deltaTime / 50f;
            }
        }
    }

    private void Spawn()
    {
        Parent.localScale = new Vector3(1f, Size/2f, 1f);
        var g = Instantiate(Prefab, transform.position + Vector3.left * Mathf.Clamp(Direction, -1f, 1f) * 2f, Quaternion.identity);
        g.transform.localScale = Vector3.one * Size;
        g.transform.parent = Parent;

        _rig = g.GetComponent<Rigidbody2D>();
        _rig.AddForce(Vector2.right * Direction * SpeedBullet);
        g.GetComponent<SpriteRenderer>().color = _randColor;
        //g.transform.DOMoveX(g.transform.position.x + Direction, SpeedBullet);
        //Destroy(g, 10f);
    }

}
