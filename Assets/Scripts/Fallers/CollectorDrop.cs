using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorDrop : MonoBehaviour {

    public float maxFallSpeed = 2f;
    public float minFallSpeed = 2f;
    public float minSpinSpeed = 2f;
    public float maxSpinSpeed = 2f;
    // Use this for initialization
    float fallSpeed = 0f;
    float spinSpeed = 0f;

    void Start () {
        int direction = Random.Range(0, 10);
        if (direction < 5) direction = -1; else direction = 1;
        fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);
        spinSpeed = Random.Range(minSpinSpeed * direction , maxSpinSpeed * direction);

        // GetComponent<Rigidbody2D>().gravityScale = -5f;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.transform.gameObject.name == "Ground")
            Destroy(gameObject);
    }
}
