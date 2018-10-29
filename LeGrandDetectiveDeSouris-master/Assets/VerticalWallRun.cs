using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalWallRun : MonoBehaviour
{
    PlayerMovement pm;
    Rigidbody rb;
    public GameObject pos;
    public float height = 10;
    float lastZ;
    float y = 8;
    public bool isFalling = false;
    GameObject lastWall = null;
    // Use this for initialization
    void Start()
    {
        pm = this.GetComponent<PlayerMovement>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        float distToWall = this.GetComponent<SphereCollider>().bounds.extents.z;
        if (Physics.Raycast(pos.transform.position, transform.forward, out hit, distToWall + 1) && hit.collider.gameObject.tag == "wall" && pm.IsGrounded == false  && !isFalling && pm.IsJumping)
        {
            if (lastWall != hit.collider.gameObject)
            {
                lastWall = hit.collider.gameObject;
                lastZ = pm.zSpeed;
                lastZ = pm.Map(0, 6, 0, 4, lastZ);
                height = 6 + lastZ;
            }
            pm.currentState = PlayerMovement.PlayerState.climbing;
            pm.GetComponent<Rigidbody>().velocity = new Vector3(0, height, 0);
            height -= (y) * Time.deltaTime;
            Debug.Log(height);
            if (height <= 0 && !isFalling)
            {
                isFalling = true;
            }
            //pm.zSpeed = lastSpeed + 2;


        }
        else
        {
            height = 4;
            y = 8;
            
            //isFalling = true;
            
        }
        //Debug.Log(pos.transform.position);

        if (pm.IsGrounded)
        {
            lastWall = null;
            isFalling = false;
        }
    }
}
