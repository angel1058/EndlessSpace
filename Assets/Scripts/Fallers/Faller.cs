using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faller : MonoBehaviour {

    public float timeToDie = 0.1f;
    public float maxFallSpeed = 2f;
    public float minFallSpeed = 2f;
    public float minSpinSpeed = 2f;
    public float maxSpinSpeed = 2f;
    // Use this for initialization
    float fallSpeed = 0f;
    float spinSpeed = 0f;
    private float scaleReducer;
    private bool isDying = false;

    public float ScaleSpeed;
    public float MaxScale;
    public GameObject[] Plasma;
    public bool isFalling = false;

    Renderer renderer;
    private float hitCount;

    private Color c;
    public float hitsToKill = 1;

    private float hitByValue;
    void Start () {
        int direction = Random.Range(0, 10);
        if (direction < 5) direction = -1; else direction = 1;
        fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);
        spinSpeed = Random.Range(minSpinSpeed * direction , maxSpinSpeed * direction);
        scaleReducer = transform.localScale.x / hitsToKill;
        //
         Vector3 scale = transform.localScale;
        scale.x = 0;//scaleReducer * timeToDie * Time.deltaTime;
        scale.y = 0;//scaleReducer * timeToDie * Time.deltaTime;

        transform.localScale = scale;

        renderer = GetComponent<SpriteRenderer>();
        c = renderer.material.color;
        c.a = 0;
        renderer.material.color = c;
    }


void fadeIn()
{
        Vector3 scale = transform.localScale;
    	   if (isFalling == false &&  scale.x < MaxScale)
	   {
        scale.x += ScaleSpeed * Time.deltaTime * 0.5f;
        scale.y += ScaleSpeed * Time.deltaTime * 0.5f;
        if ( scale.x >= MaxScale)
            isFalling = true;
        
        if ( c.a < 1)
        {
            c.a += ScaleSpeed * Time.deltaTime * 0.5f;
            renderer.material.color = c;
        }
        transform.localScale = scale;
	   }
}
	// Update is called once per frame
	void Update ()
    {
       
       fadeIn();

        MoveAndSpin();
        if (isDying)
            DieSlowly();
    }

    public void HitIt(float hit)
    {
        if ( isDying || isFalling == false)
            return;

        hitCount += hit;

        if ( hitCount >= hitsToKill)
        {
                Die();
                return;
        }
        hitByValue = hit;
        oldXScale = transform.localScale;
        isDying = true;
    }

    private void DieSlowly()
    {
        Vector3 scale = transform.localScale;
        scale.x -= (scaleReducer*hitByValue) * timeToDie * Time.deltaTime;
        scale.y -= (scaleReducer*hitByValue) * timeToDie * Time.deltaTime;

        transform.localScale = scale;
        if (scale.x <= oldXScale.x - (scaleReducer*hitByValue))
            isDying = false;

        if (scale.x <= 0.1)
            Die();
    }

    void Die()
    {
    if ( Random.Range(0.0f,1.0f) > 0.5)
        {

        int elements = Plasma.Length;
        int rnd = Random.Range(0,elements);
        Instantiate(Plasma[rnd] , gameObject.transform.position,  Quaternion.identity);
        }
        Destroy(gameObject);
        GameMgr.Instance.Fallen();
   
    }

    private void MoveAndSpin()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);

        if ( transform.position.x < -3 || transform.position.x > 3)
        {
		    Destroy(gameObject);
            return;
        }
        if ( transform.position.y < -4.32 )
		    Destroy(gameObject);
    }
    private Vector3 oldXScale;
    private void OnMouseDown()
    {
      //  isDying = true;
       HitIt(1);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.transform.gameObject.name == "Ground")
            Destroy(gameObject);
    }
}
