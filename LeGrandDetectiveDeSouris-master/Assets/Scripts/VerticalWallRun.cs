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
    public float y;
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
        if (Physics.Raycast(pos.transform.position, transform.forward, out hit, distToWall + 0.25f) && pm.IsGrounded == false  && (pm.currentState == PlayerMovement.PlayerState.jumping || pm.currentState == PlayerMovement.PlayerState.walljumping) && lastWall != hit.collider.gameObject && hit.transform.tag == "wall")
        {
            Debug.Log("HIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIT");
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

        if (y <= 2)
        {

            /*if (Input.GetKey(KeyCode.W) )
            {

            }

            else
            {
                pm.currentState = PlayerMovement.PlayerState.walljumping;
                y = 0;
            }*/
            Debug.Log(this.transform.position.y);
            if (Input.GetKey(KeyCode.Mouse1) && pm.currentState == PlayerMovement.PlayerState.climbing)
            {
                
                if (Input.GetKeyDown(KeyCode.Space))
                {


                    rb.velocity = transform.up * 2;
                    rb.velocity = transform.forward * 2;
                    pm.currentState = PlayerMovement.PlayerState.walljumping;
                }else pm.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
            else
            {
                pm.zSpeed = 0;
                pm.currentState = PlayerMovement.PlayerState.falling;
            }

        }
        if (hasChecked && (pm.currentState == PlayerMovement.PlayerState.jumping || pm.currentState == PlayerMovement.PlayerState.climbing))
        {
            Debug.Log(lastWall.gameObject.transform.lossyScale.y);

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
       else y = startY;
        
        if (pm.currentState == PlayerMovement.PlayerState.climbing && this.transform.position.y >= (lastWall.gameObject.transform.lossyScale.y))
        {
            pm.zSpeed = 1;
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
