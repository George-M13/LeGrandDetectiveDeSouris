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
        RaycastHit hit;

    }

    bool CheckGrounded() {
        Vector3 leftRayRotation = Quaternion.AngleAxis(-20, transform.right) * -transform.up;
        Vector3 rightRayRotation = Quaternion.AngleAxis(20, transform.right) * -transform.up;
        Vector3 leftFrontRayRotation = Quaternion.AngleAxis(30, transform.up) * leftRayRotation;
        Vector3 rightFrontRayRotation = Quaternion.AngleAxis(-30, transform.up) * leftRayRotation;
        Debug.DrawRay(transform.position, leftRayRotation, Color.cyan);
        Debug.DrawRay(transform.position, -Vector3.up, Color.cyan);
        Debug.DrawRay(transform.position, rightRayRotation, Color.cyan);
        Debug.DrawRay(transform.position, leftFrontRayRotation, Color.cyan);
        Debug.DrawRay(transform.position, rightFrontRayRotation, Color.cyan);
        if (Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f)) return true;
        else if((Physics.Raycast(transform.position, leftRayRotation/2, distToGround + 0.1f))) return true;
        else if ((Physics.Raycast(transform.position, rightRayRotation/2, distToGround + 0.1f))) return true;
        else if ((Physics.Raycast(transform.position, leftRayRotation, distToGround + 0.1f))) return true;
        else if ((Physics.Raycast(transform.position, rightRayRotation, distToGround + 0.1f))) return true;
        else if ((Physics.Raycast(transform.position, leftFrontRayRotation, distToGround + 0.1f))) return true;
        else if ((Physics.Raycast(transform.position, rightFrontRayRotation, distToGround + 0.1f))) return true;
        return false;//Raycast from the edge of the collider to see if it is touching a floor
 }
}
