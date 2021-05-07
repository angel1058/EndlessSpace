using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTarget : MonoBehaviour {


    public float Range = 3.0f;
    public string FallerTag = "Faller";
    public Transform target = null;
    public Transform Emitter;
    public Vector3 startingPos;
    // Use this for initialization
    void Start () {
        startingPos = transform.position;
        InvokeRepeating("UpdateTarget", 0, 0.1f);
	}
    private float lastAngle;
    // Update is called once per frame
    void Update() {
        Vector3 targetPos;
        float angleOffset;
        if (target == null)
        {
            targetPos = startingPos;
            angleOffset = 0f;
        }
        else
        {
            angleOffset = -90;
            targetPos = target.position;
        }
        Vector3 dir = targetPos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle + angleOffset, Vector3.forward);
        //transform.rotation = Quaternion.Lerp(Quaternion.AngleAxis(angle + angleOffset, Vector3.forward), transform.rotation, Time.deltaTime * 0.3f);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle + angleOffset, Vector3.forward), 0.1f);
    }

    void UpdateTarget()
    {
        if ( target != null)
        {
            return;
        }
        GameObject[] fallers = GameObject.FindGameObjectsWithTag(FallerTag);
        float closest = Mathf.Infinity;
        GameObject nearestFaller = null;

    foreach(GameObject g in fallers)
        {
            float distanceToFaller = g.transform.position.y;
            if (distanceToFaller < closest)
            {
                closest = distanceToFaller;
                nearestFaller = g;
            }

            if (nearestFaller != null && closest <= Emitter.position.y + Range)
                target = nearestFaller.transform;
            else
                target = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 pos = Emitter.position;
        Vector3 oldpos = pos;
        pos.y += Range;
        oldpos.y += Range;
        pos.x = -3;

        oldpos.x = 3;
      //  pos.x += Range;

        Gizmos.DrawLine(pos, oldpos); 
    }
}
