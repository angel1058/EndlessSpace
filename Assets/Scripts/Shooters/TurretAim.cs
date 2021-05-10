using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour {


 public Transform firePoint = null;
public float RotationSpeed = 5f;

   private string logPrefix = "[EN-UNITY] [TURRET-AIM] - ";
    
    public Transform target = null;
    public Vector3 startingPos;

    public GameObject GameManager;

    public bool DebugOn;
    // Use this for initialization
    void Start () {
        startingPos = transform.position;
      //  InvokeRepeating("UpdateTarget", 0, 0.1f);
	}

       void Log(string message)
    {
        if (DebugOn == false)
            return;

        Debug.Log(logPrefix + message);
    }
    private float lastAngle;
    // Update is called once per frame
    void Update() {

        if ( target == null)
        {
            target = GameManager.GetComponent<TargetMapper>().GetTarget(this.gameObject);
            if ( target == null)
            {
                Log( " No target");
                return;
            }
            else
            {
                Log( " target aquired");
                //tell the shooter thing...
                firePoint.GetComponent<MissileLauncher>().target = target;
            }
        }


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
