using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnFallers  : MonoBehaviour
{
    public float Timer = 2;
    public GameObject[] fallers;
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
        int elements = fallers.Length;
        int rnd = Random.Range(0,elements);

        tmpTimer -= Time.deltaTime;
        if (tmpTimer <= 0f)
        {

GameObject g = (GameObject)(Instantiate(fallers[rnd], (new Vector3(Random.Range(-2.44f, 2.44f), 5, -0.1f)), transform.rotation));
                g.name = "F2_" + i++;
            tmpTimer = Timer;

        }
    }
}

