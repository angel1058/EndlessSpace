using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnDroppers : MonoBehaviour
{
    public float Timer = 2;
    public GameObject dropper;
    private int i = 0;
    float tmpTimer;
    // Use this for initialization
    void Start()
    {
        tmpTimer = Timer;
    }

    // Update is called once per frame
    void Update()
    {
        tmpTimer -= Time.deltaTime;
        if (tmpTimer <= 0f)
        {


            GameObject g = (GameObject)(Instantiate(dropper, (new Vector3(Random.Range(-2.44f, 2.44f), 5, -0.1f)), transform.rotation));
                g.name = "I" + i++;
            tmpTimer = Timer;

        }
    }
}

