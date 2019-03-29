using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour {

    private float timeBtwSpawns;
    public float startTimeBtwSpawns;

    public GameObject echo;
    private MainBall mainBall;

    void Start()
{
        mainBall = GetComponent<MainBall>();
    }

    void Update(){

        if (mainBall._trail.gameObject.activeSelf == true)
        {
            if (timeBtwSpawns <= 0)
            {
                // spawn echo game object
                GameObject instance = (GameObject)Instantiate(echo, transform.position, Quaternion.identity);
                Destroy(instance, 2f);
                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
    }
}
