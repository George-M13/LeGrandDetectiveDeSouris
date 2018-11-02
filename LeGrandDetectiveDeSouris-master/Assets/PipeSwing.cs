using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSwing : MonoBehaviour {
    PlayerMovement pm;
    Rigidbody rb;

    public GameObject currentPipe;
    float tetherLength = 10;
    float speed = 1;

    Vector3 heading;
    Vector3 sphereHeading;

    float distanceCap = 10;

	// Use this for initialization
	void Start () {
        pm = this.GetComponent<PlayerMovement>();
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        /* Vector3 testPos = rb.velocity + this.transform.position * Time.deltaTime;
         RaycastHit interSection;
         if(Physics.Raycast(this.transform.position, testPos, out interSection))
         {
             testPos = interSection.transform.position + interSection.transform.position.normalized;
         }
         rb.velocity = (testPos - this.transform.position) / Time.deltaTime;
         if (rb.velocity.magnitude >= 3) rb.velocity = rb.velocity.normalized * 3;
         this.transform.position = testPos;
         if((testPos - currentPipe.transform.position).magnitude >= tetherLength)
         {
             testPos = (testPos - currentPipe.transform.position).normalized * tetherLength;
         } */
        /*if (pm.currentState == PlayerMovement.PlayerState.pipeswinging)
        {
            heading = transform.position - currentPipe.transform.position;
            if (heading.magnitude < distanceCap) distanceCap = heading.magnitude;
            sphereHeading = heading.normalized * distanceCap;
            transform.position = currentPipe.transform.position + sphereHeading;
            Debug.Log(heading);
            Debug.Log(sphereHeading);

   
        }
            //this.transform.RotateAround(currentPipe.transform.position, transform.right, rb.velocity.z);


        else speed = 1;*/

        //currentPipe.GetComponent<HingeJoint>().connectedBody = rb;
	}
}
