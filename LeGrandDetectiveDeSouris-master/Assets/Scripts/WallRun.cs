using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour {

    Rigidbody rb;
    PlayerMovement pm;
    bool grounded;
    bool wallRunning;
    int whichWall = 0;
    float distToWall;

    public GameObject lastWall;

    public float wallTime = 0f;
    SphereCollider coll;

	// Use this for initializatino
	void Start () {
        rb = this.GetComponent<Rigidbody>();
        pm = this.GetComponent<PlayerMovement>();
        coll = this.GetComponent<SphereCollider>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        RaycastHit hit;
        grounded = pm.IsGrounded;         //Storing variable for easy access
        pm.IsWallRunning = wallRunning;
        distToWall = coll.bounds.extents.x;


        //Raycast both left and right of the player and check if they are not grounded
        if (Physics.Raycast(transform.position, -transform.right, out hit, distToWall +0.1f) && hit.collider.gameObject.tag == "wall" && grounded == false)
        {
            rb.useGravity = false; //Turn off the gravity for the player
            wallRunning = true;
            if (lastWall != hit.collider.gameObject) //Check that the last wall that the player was running on isn't the current wall
            {
                lastWall = hit.collider.gameObject; //If it isn't then store is as the new last wall
                pm.CanWallJump = true; // the player can wall jump
            }
            //rb.AddForce(transform.right * 5);
            whichWall = 1;
            }
        else if(Physics.Raycast(transform.position, transform.right, out hit, distToWall +0.1f) && hit.collider.gameObject.tag == "wall" && grounded == false)
        {
            rb.useGravity = false; //Turn off the gravity for the player
            wallRunning = true;
            if (lastWall != hit.collider.gameObject) //Check that the last wall that the player was running on isn't the current wall
            {
                lastWall = hit.collider.gameObject; //If it isn't then store is as the new last wall
                pm.CanWallJump = true; // the player can wall jump
            }
           // rb.AddForce(-transform.right * 5);
            whichWall = 2;
        }
        else
        {
            rb.useGravity = true; //If they are not wallrunning turn on the gravity
            wallRunning = false;                              //they are not wallrunning
            pm.CanWallJump = false;                           //they can't wall jump
            whichWall = 0;
        }
        if (wallRunning == true)
        {
            wallTime += 1 * Time.deltaTime; //Start a timer from when the player starts wallrunning
        }
        else wallTime = 0;
        if(wallTime >= 1)//If the timer reaches it's limit stop the player form wallrunning
        {
            rb.useGravity = true;
            wallRunning = false;
            pm.CanWallJump = false;
            whichWall = 0;
        }

    }

    public void WallJump()
    {
        if(whichWall == 1)
        {
            rb.velocity += Vector3.up * 3;
            //rb.velocity += transform.right * 3;
        }else if (whichWall == 2)
        {
            rb.velocity += Vector3.up * 3;
            //rb.velocity += -transform.right * 3;
        }
    }
}
