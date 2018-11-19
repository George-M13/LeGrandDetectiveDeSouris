using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalWallRun : MonoBehaviour
{
    PlayerMovement pm;
    Rigidbody rb;
    public GameObject pos;
    public float height = 1;
    float counter = 0;
    float lastZ;
    float startY = 50;
    Vector3 dir = new Vector3(0,0,0);
    public float y;
    public bool hasChecked = false;
    public bool wallGrab = false;
    public bool isGettingUp = false;

    public float h;
    public GameObject lastWall = null;
    Vector3 lastPos = new Vector3(0, 0, 0);


    public GameObject Floor;
    // Use this for initialization
    void Start()
    {
        pm = this.GetComponent<PlayerMovement>();
        rb = this.GetComponent<Rigidbody>();
        y = startY * lastZ;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        RaycastHit hit;
        float distToWall = this.GetComponent<SphereCollider>().bounds.extents.z;

        //Debug.Log(this.transform.forward);
        if (Physics.Raycast(transform.position, transform.forward, out hit, distToWall + 0.25f) && pm.IsGrounded == false && (pm.currentState == PlayerMovement.PlayerState.jumping || pm.currentState == PlayerMovement.PlayerState.walljumping) && lastWall != hit.collider.gameObject)
        {
            if (lastWall != hit.collider.gameObject)
            {
                //Debug.Log("dfkjedknf");
                lastWall = hit.collider.gameObject;
                lastZ = pm.zSpeed;
                lastZ = pm.Map(1, 6, 5, 1, lastZ);
                lastPos = this.transform.position;
                //startY = 0f + lastZ/10;
                //height = (lastWall.transform.localScale.y + lastZ);
                hasChecked = true;
            }
            pm.currentState = PlayerMovement.PlayerState.climbing;
            //pm.zSpeed = lastSpeed + 2;


        }
        else if (pm.currentState == PlayerMovement.PlayerState.climbing)
        {
            pm.currentState = PlayerMovement.PlayerState.climbing;
        }

        if (Input.GetKey(KeyCode.Mouse1) && (pm.currentState == PlayerMovement.PlayerState.climbing || pm.currentState == PlayerMovement.PlayerState.wallgrab) && y <=2)
        {
            pm.currentState = PlayerMovement.PlayerState.wallgrab;

            if (Input.GetKeyDown(KeyCode.Space))
            {

                Debug.Log("Jumpingaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                rb.velocity = this.transform.up * 2;
                rb.velocity = this.transform.forward * 5;
                pm.currentState = PlayerMovement.PlayerState.walljumping;
            }
            else
            {
                Debug.Log("WHY");
                pm.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
        }
        else if (y <= 2 && pm.currentState == PlayerMovement.PlayerState.climbing)
        {
            pm.currentState = PlayerMovement.PlayerState.falling;
            pm.zSpeed = 0;

            /*if (Input.GetKey(KeyCode.W) )
            {

            }

            else
            {
                pm.currentState = PlayerMovement.PlayerState.walljumping;
                y = 0;
            }*/


        }else if (!Input.GetKey(KeyCode.Mouse1) && pm.currentState == PlayerMovement.PlayerState.wallgrab)
        {
            pm.currentState = PlayerMovement.PlayerState.falling;
            pm.zSpeed = 0;
        }
        if (hasChecked && (pm.currentState == PlayerMovement.PlayerState.jumping || pm.currentState == PlayerMovement.PlayerState.climbing))
        {
            

            pm.currentState = PlayerMovement.PlayerState.climbing;
            //Debug.Log(y );
            pm.GetComponent<Rigidbody>().velocity = new Vector3(0, y * Time.deltaTime * 10, 0);
            if (counter >= 1)
            {

                y = (y / 1.8f);
                counter = 0;
                
            }

            counter += 10 * Time.deltaTime;

            
            

        }
       else if(pm.currentState != PlayerMovement.PlayerState.wallgrab)y = startY;
        //Debug.Log(this.transform.position.y - Floor.transform.position.y);
        RaycastHit wallHit;
        //Debug.Log(this.transform.position.y - pos.transform.localScale.y);
        if ((pm.currentState == PlayerMovement.PlayerState.climbing || pm.currentState == PlayerMovement.PlayerState.wallgrab) && !Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y-0.5f, this.transform.position.z), transform.forward, out wallHit, 1f))
        {
            if (Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y - pos.transform.localScale.y, this.transform.position.z), transform.forward, out wallHit, 1f)){
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAH");
                pm.zSpeed = 1;
                rb.velocity = new Vector3(0, 0, 0);
                //dir = rb.vec
                pm.currentState = PlayerMovement.PlayerState.gettingup;
            }
        }
        //Debug.DrawRay(new Vector3(this.transform.position.x, this.transform.position.y - pos.transform.localScale.y, this.transform.position.z), -transform.up, Color.green);
        if (pm.currentState == PlayerMovement.PlayerState.gettingup && !pm.IsGrounded)
        {
            if (h >= 2) pm.currentState = PlayerMovement.PlayerState.falling;
            //rb.velocity = new Vector3(0, h, h);
            Vector3 climbVel = transform.InverseTransformDirection(rb.velocity);
            climbVel.y = h;
            climbVel.z = h;
            climbVel.x = 0;
            rb.velocity = transform.TransformDirection(climbVel);
            Debug.Log("start");
            h = h + 1f * Time.deltaTime;

            RaycastHit floorHit;

            if (Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y - pos.transform.localScale.y, this.transform.position.z), -transform.up, out floorHit, 1f))
            {

                Debug.Log("floor");
                if (floorHit.transform.gameObject == lastWall)
                {
                    pm.currentState = PlayerMovement.PlayerState.falling;
                }
                //else if (h >= 1f) pm.currentState = PlayerMovement.PlayerState.falling;
            }
            RaycastHit midHit;
            RaycastHit bottomHit;
            if (!Physics.Raycast(this.transform.position, this.transform.forward, out midHit, 2f))
            {
                if (!Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y -1, this.transform.position.z), transform.forward, out bottomHit, 2f))
                {
                    pm.currentState = PlayerMovement.PlayerState.falling;
                }

            }
        }
        else h = 0;
               

            //Debug.Log(pos.transform.position);

            if (pm.IsGrounded)
        {
            height = 1;
            hasChecked = false;
            lastWall = null;
            lastPos = Vector3.zero;
        }
    }

}
