using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour {


 public Transform firePoint = null;
public float RotationSpeed = 5f;

   private string logPrefix = "[EN-UNITY] [TURRET-AIM] - ";
    
    public Transform target = null;
    public Vector3 startingPos;

    public bool DebugOn;
    // Use this for initialization
    void Start () {
        startingPos = transform.position;
      //  InvokeRepeating("UpdateTarget", 0, 0.1f);
	}

       void Log(string message)
    {
        if (!DebugOn)
            return;

        Debug.Log(logPrefix + message);
    }
    private float lastAngle;
    // Update is called once per frame
    void Update() {
        target = firePoint.gameObject.GetComponent<TargetMapper>().target1;

        if ( target == null)
        {
            Log( " No target");
            return;
        }
        Log( " target aquired");

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
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle + angleOffset, Vector3.forward), RotationSpeed * Time.deltaTime);
    }


   
}
