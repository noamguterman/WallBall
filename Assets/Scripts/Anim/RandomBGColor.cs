using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBGColor : MonoBehaviour
{
    public List<Color> Colors;

    private void Awake()
    {
        if (Colors.Count > 0)
        {
            GetComponent<SpriteRenderer>().color = Colors[Random.Range(0, Colors.Count)];
        }
    }
}
