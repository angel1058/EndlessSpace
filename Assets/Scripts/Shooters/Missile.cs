﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

public bool ResetEnemy = false;
public string tagToKill;

public float MissileStrength;
public GameObject explosion;
 public GameObject littleExplosion;
private  GameObject FirePoint;
 private bool DebugOn = false;

 private bool HadTarget = false;
 private string logPrefix = "[EN-UNITY] [MISSILE] - ";

 private void Start() {
	     GetNewTarget();
 }


public void SetFirePoint(GameObject Fp)
{
	Log("FirePoint set to " + Fp);
	FirePoint = Fp;
}
  void Log(string message)
    {
        if (DebugOn == false)
            return;

        Debug.Log(logPrefix + message);
    }
void OnCollisionEnter2D(Collision2D collision)
{
	Destroy(gameObject);
	if (collision.gameObject.tag == tagToKill)
	{
		GameObject g = Instantiate(explosion ,transform.position , transform.rotation);
		Destroy(g,0.5f);
	if ( collision.gameObject != null)
		collision.gameObject.GetComponent<Faller>().HitIt(MissileStrength);
	}
}


void GetNewTarget()
{ 
	if ( HadTarget && ResetEnemy == false)
		return;
	HadTarget = true;	
	target = FirePoint.gameObject.GetComponent<MissileLauncher>().target;
	if ( target == null)
		Log ("no target");
	else
		Log("We're aiming towards " + target);
}

public void SetTarget(Transform target)
{
	this.target = target;
}

    public Transform target;
    public Rigidbody2D rigidBody;
    public float angleChangingSpeed= 200f;
    public float movementSpeed = 5f;
    void FixedUpdate ()
    {
		if ( transform.position.x < -3 || transform.position.x > 3 || 
		transform.position.y > 5)
			Destroy(gameObject);

		if ( target == null)
			GetNewTarget();
	
	if ( target != null)
	{
        Vector2 direction = (Vector2)target.position - rigidBody.position;
        direction.Normalize ();
        float rotateAmount = Vector3.Cross (direction, transform.up).z;
        rigidBody.angularVelocity = -angleChangingSpeed * rotateAmount;
        rigidBody.velocity = transform.up * movementSpeed ;
    }
	}
}