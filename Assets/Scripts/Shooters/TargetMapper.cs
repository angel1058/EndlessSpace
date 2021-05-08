using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMapper : MonoBehaviour {


public string FallerTag = "Faller";

 public Transform target1 ;

 public float Range;

public bool DebugOn;
 private string logPrefix = "[EN-UNITY] [TARGET-MAPPER] - ";

 void Start () {
        InvokeRepeating("FindTarget", 0, 0.1f);
	}

    void Log(string message)
    {
        if (!DebugOn)
            return;

        Debug.Log(logPrefix + message);
    }
	void FindTarget()
	{

        if ( target1 != null)
        {
            Log(" Already have a target" + target1);
            return;
        }
        Log( " Finding Target");

        GameObject[] fallers = GameObject.FindGameObjectsWithTag(FallerTag);
        float closest = Mathf.Infinity;
        GameObject nearestFaller = null;

        Log( " We have " + fallers.Length + " fallers");

    	foreach(GameObject g in fallers)
        {
            float distanceToFaller = g.transform.position.y;
            if (distanceToFaller < closest)
            {
                closest = distanceToFaller;
                nearestFaller = g;
            }
            Log( "transform pos : " + transform.position.y);
            Log( "transform pos + range : " + transform.position.y + Range);
            Log( "closest : " + closest);
            if (nearestFaller != null && closest <= transform.position.y + Range)
                target1 = nearestFaller.transform;
            else
                target1 = null;
        }

		if (target1 == null )
		    Log( " Target not found");
		else
		    Log( " Target found" + target1.name);
	}
 void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pos = transform.position;
        Vector3 oldpos = pos;
        pos.y += Range;
        oldpos.y += Range;
        pos.x = -3;
        oldpos.x = 3;

        Gizmos.DrawLine(pos, oldpos); 
    }
}
