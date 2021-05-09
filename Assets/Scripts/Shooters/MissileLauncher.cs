using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : MonoBehaviour {

   private string logPrefix = "[EN-UNITY] [MISSILE LAUNCH] - ";

    private bool DebugOn = false;
    public GameObject missilePreFab;
    // Use this for initialization

    public Transform target;

public float FireRate = 1f;

    void Start()
    {
        StartCoroutine( Shoot() ) ;
        InvokeRepeating("Shoot", 1.0f, FireRate);
    }

        void Log(string message)
    {
        if (!DebugOn)
            return;

        Debug.Log(logPrefix + message);
    }
    IEnumerator   Shoot()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
    
//            target = null;//gameObject.GetComponent<TargetMapper>().target1;
            if ( target == null)
                Log( " no target");
            else
                {
                    Log( " launching towards " + target);
                GameObject missile = Instantiate(missilePreFab, transform.position, transform.rotation);
                missile.gameObject.GetComponent<Missile>().SetFirePoint(gameObject);
                }
     
            yield return new WaitForSeconds(FireRate);
        }
	}
    }

