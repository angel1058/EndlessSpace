using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorDrop : MonoBehaviour {

    public float timeToDie = 0.3f;
    public float maxFallSpeed = 2f;
    public float minFallSpeed = 2f;
    public float minSpinSpeed = 2f;
    public float maxSpinSpeed = 2f;
    // Use this for initialization
    float fallSpeed = 0f;
    float spinSpeed = 0f;
    private float scaleReducer;
    private bool isDying = false;

    public int hitsToKill = 3;
    void Start () {
        int direction = Random.Range(0, 10);
        if (direction < 5) direction = -1; else direction = 1;
        fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);
        spinSpeed = Random.Range(minSpinSpeed * direction , maxSpinSpeed * direction);
        scaleReducer = transform.localScale.x / hitsToKill;
    }
	
	// Update is called once per frame
	void Update ()
    {
        MoveAndSpin();
        if (isDying)
            DieSlowly();

    }

    private void HitIt()
    {
        Vector3 scale = transform.localScale;
        scale.x -= scaleReducer;// * Time.deltaTime;
        scale.y -= scaleReducer;// * Time.deltaTime;

        transform.localScale = scale;

        if (scale.x <=0)
            DestroyObject(gameObject);

    }

    private void DieSlowly()
    {
        Vector3 scale = transform.localScale;
        scale.x -= scaleReducer * timeToDie * Time.deltaTime;
        scale.y -= scaleReducer * timeToDie * Time.deltaTime;

        transform.localScale = scale;
        if (scale.x <= oldXScale.x - scaleReducer)
            isDying = false;

        if (scale.x <= 0)
            DestroyObject(gameObject);
    }

    private void MoveAndSpin()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
    }
    private Vector3 oldXScale;
    private void OnMouseDown()
    {
        oldXScale = transform.localScale;
        isDying = true;
//        HitIt();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.transform.gameObject.name == "Ground")
            Destroy(gameObject);
    }
}
