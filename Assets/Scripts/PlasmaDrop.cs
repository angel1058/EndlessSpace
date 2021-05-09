using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaDrop : MonoBehaviour {
public float ScaleSpeed = 20f;
	public float originalFallSpeed;
	public float fallThroughGround = 0.25f;
	// Use this for initialization
	private float fallSpeed;
	void Start () {
		   Vector3 scale = transform.localScale;
        scale.x = 0;//scaleReducer * timeToDie * Time.deltaTime;
        scale.y = 0;//scaleReducer * timeToDie * Time.deltaTime;

        transform.localScale = scale;
		fallSpeed = originalFallSpeed;
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
else{
		 transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
		 if ( transform.position.y < -4.88f)
		 	fallSpeed = originalFallSpeed;
		 else if ( transform.position.y < -3.79f)
		 	fallSpeed = fallThroughGround;
}
		if ( transform.position.y < -6.5)
		{
			Destroy(gameObject);
		       Debug.Log(GameMgr.Instance.FallersDestroyed);
 
		}
	}

}
