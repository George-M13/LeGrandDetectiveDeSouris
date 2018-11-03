using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
    public GameObject player;
    public PlayerMovement pm;

    public bool grounded;
    SphereCollider coll;

    float distToGround;

    public void Start()
    {
        coll = this.GetComponent<SphereCollider>();//Get the collider attached to the gameiobject
    }
    public void Update()
    {
        pm = player.GetComponent<PlayerMovement>();
        distToGround = coll.bounds.extents.y;//Get the bounds of the collider in the y direction
        pm.IsGrounded = CheckGrounded();

    }

    bool CheckGrounded() {
    return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);//Raycast from the edge of the collider to see if it is touching a floor
 }
}
