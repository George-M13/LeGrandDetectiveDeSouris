using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour {

    PlayerMovement pm;
    SphereCollider coll;
    Rigidbody rb;

    public float height;

    GameObject lastWall;

    float lastSpeed;

    float dir;
    public RaycastHit hit;


	// Use this for initialization
	void Start () {
        pm = this.GetComponent<PlayerMovement>();
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        coll = this.GetComponent<SphereCollider>();
        float distToWall = coll.bounds.extents.x;

        if (pm.IsGrounded)
        {
            lastWall = null;
            pm.IsWallRunning = false;
            //jumping = false;
            //timer = 0;
        }
        //if (jumping) timer += Time.deltaTime;
        /*if (timer >= 0.4f)
        {
            pm.IsWallRunning = false;
            jumping = false;
            timer = 0;
        }*/
        if (pm.IsWallRunning)
        {
            if (lastSpeed == 0) lastSpeed = pm.zSpeed;
            pm.zSpeed = 10;
            //Debug.Log(pm.zSpeed);
            rb.velocity = new Vector3(0, height, 8 *pm.Dir );
            height = height - Time.deltaTime;
            //Debug.Log(height);
        }
        else
        {

            height = 0;
        }

        if (Physics.Raycast(transform.position, -transform.right, out hit, distToWall + 0.1f) && hit.collider.gameObject.tag == "wall" && pm.IsGrounded == false)
        {
            pm.IsWallRunning = true;
            if (lastWall != hit.collider.gameObject)
            {
                lastWall = hit.collider.gameObject;
                pm.CanWallJump = true;
            }
            if (lastWall == hit.collider.gameObject && !pm.CanWallJump) pm.IsWallRunning = false;
            if (Input.GetKeyDown(KeyCode.Space) && pm.IsWallRunning && pm.CanWallJump)
            {
                //pm.IsWallRunning = false;
                WallJump(1);
                pm.CanWallJump = false;
            }
        }
        else if (Physics.Raycast(transform.position, transform.right, out hit, distToWall + 0.1f) && hit.collider.gameObject.tag == "wall" && pm.IsGrounded == false)
        {

            pm.IsWallRunning = true;
            if (lastWall != hit.collider.gameObject)
            {
                lastWall = hit.collider.gameObject;

                pm.CanWallJump = true;
            }
            
            if (lastWall == hit.collider.gameObject && !pm.CanWallJump) pm.IsWallRunning = false;
            if (Input.GetKeyDown(KeyCode.Space) && pm.IsWallRunning && pm.CanWallJump)
            {
                WallJump(-1);
                pm.CanWallJump = false;
            }
        }
        else
        {
            pm.IsWallRunning = false;
            pm.CanWallJump = false;
        }

        
        //Debug.Log(rb.velocity.z);
    }


    public void WallJump(int w)
    {
        //jumping = true;
        pm.IsWallRunning = false;
        rb.velocity += new Vector3(0, 3, 0);
        
    }
}


