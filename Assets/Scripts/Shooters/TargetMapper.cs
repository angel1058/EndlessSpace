using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMapper : MonoBehaviour {

[Header("Falling Stuff")]
public string FallerTag = "Faller";

[Header("Aiming Stuff")]

public Transform Ground = null;
public Transform Sky = null;


[Range(0.0f,1.0f)]
public float Range = 1f;

private float groundYPos;
private float skyYPos;

private float height;
 
[Header("Utils")]
public bool DebugOn;

 private string logPrefix = "[EN-UNITY] [TARGET-MAPPER] - ";

List<Transform> targets = new List<Transform>();
 void Start () {
        groundYPos = Ground.position.y;
        skyYPos = Sky.position.y;
        height = skyYPos - groundYPos;
	}

    void Log(string message)
    {
        if (DebugOn == false)
            return;

        Debug.Log(logPrefix + message);
    }

public void RemoveTarget(Transform target)
{
    targets.Remove(target);
}


public Transform GetTarget(GameObject requester)
{
        groundYPos = Ground.position.y;
        skyYPos = Sky.position.y;
        height = skyYPos - groundYPos;
        //Log("Request for Target from " + requester.name);
        GameObject[] fallers = GameObject.FindGameObjectsWithTag(FallerTag);
        float closest = Mathf.Infinity;
        GameObject nearestFaller = null;

        float inRangeValue= groundYPos + (height * Range);

        Transform nextTarget = null;
    	foreach(GameObject g in fallers)
        {
            if ( targets.Contains(g.transform))
            {
                Log(g + " is already a target - skipping");
                continue;
            }

            //if the object isn't fully visible ( not falling - skip)
            if ( g.GetComponent<Faller>().isFalling == false)
                continue;

            //if we're here, this is a potential target
            float distanceToFaller = g.transform.position.y - inRangeValue;
            Log(distanceToFaller.ToString());
            if ( distanceToFaller > 0)
            {
                    Log(g + "not in range");
                    continue;
            }

            //if we are here, we are in range
            if (distanceToFaller < closest)
            {
                closest = distanceToFaller;
                nearestFaller = g;
                nextTarget = nearestFaller.transform;
            }

        }

        if ( nextTarget != null)
            {
                targets.Add(nextTarget);
                Log("We have a target - sending " + nextTarget.name + " to " + requester.name);
            }
            else
            Log("no next target");
        return nextTarget;
    }
	
 void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 pos = transform.position;
        Vector3 oldpos = pos;
        pos.y = groundYPos;
        oldpos.y = groundYPos;
        pos.x = -3;
        oldpos.x = 3;

        Gizmos.DrawLine(pos, oldpos); 


        Gizmos.color = Color.yellow;
        pos.y = groundYPos + height * Range;
        oldpos.y = groundYPos + height * Range;

        Gizmos.DrawLine(pos, oldpos); 

    }
}
