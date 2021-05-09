using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageChange : MonoBehaviour {

    enum MOVE_DIR {NONE , UP, DOWN , LEFT , RIGHT};

	    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

	public Camera mainCamera;

    public float targetY = 0;
    public float cameraMoveSpeed = 10;
    
    public enum ZONE { ABOVE_MAIN , ABOVE_RIGHT , ABOVE_LEFT , BELOW_MAIN, BELOW_LEFT, BELOW_RIGHT}

    public  ZONE zone = ZONE.ABOVE_MAIN;
    private MOVE_DIR moveDirection = MOVE_DIR.NONE;
    void Start()
    {

        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen
    }
 
private void CalculateZone()
{
    if ( moveDirection == MOVE_DIR.DOWN)
    {
        switch (zone)
        {
            case ZONE.ABOVE_MAIN:zone=ZONE.BELOW_MAIN;break;
            case ZONE.ABOVE_LEFT:zone=ZONE.BELOW_LEFT;break;
            case ZONE.ABOVE_RIGHT:zone=ZONE.BELOW_RIGHT;break;
        }
    }
    if ( moveDirection == MOVE_DIR.UP)
    {
        switch (zone)
        {
            case ZONE.BELOW_MAIN:zone=ZONE.ABOVE_MAIN;break;
            case ZONE.BELOW_LEFT:zone=ZONE.ABOVE_LEFT;break;
            case ZONE.BELOW_RIGHT:zone=ZONE.ABOVE_RIGHT;break;
        }
    }

}

    void Update()
    {
        if ( moveDirection == MOVE_DIR.NONE)
        {
            CheckForDragging();
            if ( moveDirection != MOVE_DIR.NONE)
                CalculateZone();
        }
        if ( moveDirection != MOVE_DIR.NONE)
        {

            Debug.Log("Moving - " + moveDirection);
            if ( moveDirection == MOVE_DIR.DOWN)
            {
            mainCamera.transform.Translate(Vector3.down * cameraMoveSpeed * Time.deltaTime);
            if ( mainCamera.transform.position.y <= targetY)
                moveDirection = MOVE_DIR.NONE;
            }
            if ( moveDirection == MOVE_DIR.UP)
            {
            mainCamera.transform.Translate(Vector3.up * cameraMoveSpeed * Time.deltaTime);
            if ( mainCamera.transform.position.y >= targetY)
                moveDirection = MOVE_DIR.NONE;
            }
        }
    }

private void calculateTargetYPos()
{
    float height = 2f * mainCamera.orthographicSize;
    
    //this will only be up or down
    if  (moveDirection ==  MOVE_DIR.UP)
        targetY = mainCamera.transform.position.y + height;
    else
        targetY = mainCamera.transform.position.y - height;
}
    private void CheckForDragging()
    {
          if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list
 
                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            moveDirection = MOVE_DIR.RIGHT;
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            moveDirection = MOVE_DIR.LEFT;
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            moveDirection = MOVE_DIR.UP;
                            Debug.Log("Up Swipe");
                            calculateTargetYPos();
                        }
                        else
                        {   //Down swipe
                        moveDirection = MOVE_DIR.DOWN;
                            Debug.Log("Down Swipe");
                            calculateTargetYPos();
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }

}
