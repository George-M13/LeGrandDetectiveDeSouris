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
     float startY = 40;
    float y;
    public bool hasChecked = false;
    GameObject lastWall = null;
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
        if (Physics.Raycast(pos.transform.position, transform.forward, out hit, distToWall + 1) && pm.IsGrounded == false  && (pm.currentState == PlayerMovement.PlayerState.jumping || pm.currentState == PlayerMovement.PlayerState.walljumping) && lastWall != hit.collider.gameObject)
        {

            if (lastWall != hit.collider.gameObject)
            {
                lastWall = hit.collider.gameObject;
                lastZ = pm.zSpeed;
                lastZ = pm.Map(1, 6, 5, 1, lastZ);
                //startY = 0f + lastZ/10;
                //height = (lastWall.transform.localScale.y + lastZ);
                hasChecked = true;
            }
            pm.currentState = PlayerMovement.PlayerState.climbing;
            //pm.zSpeed = lastSpeed + 2;


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

            if (y <= 2)
            {
                Debug.Log("w");
                if (Input.GetKey(KeyCode.W))
                {
                    pm.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Debug.Log("aoujgikgsd");
                        pm.currentState = PlayerMovement.PlayerState.falling;

                        rb.velocity = new Vector3(0, 3, this.rb.velocity.z);

                    }
                }

                else
                {
                    pm.currentState = PlayerMovement.PlayerState.walljumping;
                    y = 0;
                }

            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("aoujgikgsd");
                pm.currentState = PlayerMovement.PlayerState.jumping;
                
                rb.velocity +=  new Vector3(0, 3,0) ;
                
            }

        }
       else y = startY;
        if (pm.currentState == PlayerMovement.PlayerState.climbing && this.transform.position.y >= (lastWall.gameObject.transform.localScale.y))
        {
            pm.currentState = PlayerMovement.PlayerState.falling;
        }
               

            //Debug.Log(pos.transform.position);

            if (pm.IsGrounded)
        {
            height = 1;
            hasChecked = false;
            lastWall = null;
        }
    }
}
