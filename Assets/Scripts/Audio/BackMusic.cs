using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMusic : MonoBehaviour
{
    public static BackMusic Instance;
    private void Awake()
    {
        if (Instance != this)
        {
            if (Instance == null)
            {
                GetComponent<AudioSource>().Play();
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else
            {
                GetComponent<AudioSource>().Stop();
                Destroy(gameObject);
            }
        }
    }
}
