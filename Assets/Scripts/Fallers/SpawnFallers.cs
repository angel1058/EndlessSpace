using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnFallers  : MonoBehaviour
{

    public float SpawnRate = 1.0f;
    public GameObject[] fallers;
    private static  int i = 0;

    public Transform spawnPoint;
    private int fallerTypeCount ;

    [Header("Utility Stuff")]
    public bool isDisabled = false;
    // Use this for initialization
    void Start()
    {
        fallerTypeCount = fallers.Length;
        StartCoroutine( Drop() ) ;
        InvokeRepeating("Drop", 1.0f, SpawnRate);
    }

    IEnumerator Drop()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            if ( isDisabled == true)
                yield return new WaitForSeconds(SpawnRate);
            else
            {
            int rnd = Random.Range(0,fallerTypeCount);
            GameObject g = (GameObject)(Instantiate(fallers[rnd], (new Vector3(Random.Range(-2.44f, 2.44f), spawnPoint.position.y, -0.1f)), transform.rotation));
            g.name = "F_" + rnd +  "_" + i++;
          
            yield return new WaitForSeconds(SpawnRate);
            }
        }
    }

  
}

