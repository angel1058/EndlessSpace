using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaDrop : MonoBehaviour {
public float ScaleSpeed = 20f;
	public float fallSpeed;
	// Use this for initialization
	void Start () {
		   Vector3 scale = transform.localScale;
        scale.x = 0;//scaleReducer * timeToDie * Time.deltaTime;
        scale.y = 0;//scaleReducer * timeToDie * Time.deltaTime;

        transform.localScale = scale;
	}
	
	// Update is called once per frame
	void Update () {
	   Vector3 scale = transform.localScale;
	   if ( scale.x < 1)
	   {
        scale.x += ScaleSpeed * fallSpeed*Time.deltaTime;
        scale.y += ScaleSpeed * fallSpeed*Time.deltaTime;

        transform.localScale = scale;
	   }

		 transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
		 if ( transform.position.y < -4.32)
		 	fallSpeed = 0.25f;

		if ( transform.position.y < -6.5)
			Destroy(gameObject);
	}

}
