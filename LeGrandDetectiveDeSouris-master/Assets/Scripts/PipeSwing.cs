using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSwing : MonoBehaviour {
    PlayerMovement pm;
    Rigidbody rb;
    PipeSwingStateSwitch pSwitch;
    public GameObject sp;
    public GameObject playerPos;
    bool forwardPressed = false;
    bool backwardPressed = false;

    public GameObject currentPipe;
    float tetherLength = 10;
    float speed = 1;

    Vector3 heading;
    Vector3 sphereHeading;

    bool attached = false;
    bool dismounted = false;

    float distanceCap = 10;

	// Use this for initialization
	void Start () {
        pm = this.GetComponent<PlayerMovement>();
        rb = this.GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rb.velocity);
        if (currentPipe != null)pSwitch = currentPipe.GetComponent<PipeSwingStateSwitch>();
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
            heading = rb.velocity - currentPipe.transform.position;
            if (heading.magnitude < distanceCap) distanceCap = heading.magnitude;
            sphereHeading = heading.normalized * distanceCap;
            rb.velocity = currentPipe.transform.position + sphereHeading;
            Debug.Log(heading);
            Debug.Log(sphereHeading);

   
        }
            //this.transform.RotateAround(currentPipe.transform.position, transform.right, rb.velocity.z);


        else speed = 1;*/

        //currentPipe.GetComponent<HingeJoint>().connectedBody = rb;
       
        if (attached)
        {
            if (this.transform.position != sp.transform.position)
            {
                rb.velocity = Vector3.zero;
                this.transform.position = Vector3.Lerp(this.transform.position, sp.transform.position, 0.1f);
                rb.useGravity = false;
                rb.velocity = Vector3.zero;
            }
            else this.transform.position = sp.transform.position;

            if (Input.GetKeyDown(KeyCode.W) && !forwardPressed)
            {
                sp.GetComponent<Rigidbody>().AddForce(-sp.transform.forward * 10, ForceMode.Impulse);
                forwardPressed = true;
                backwardPressed = false;
            }
            if (Input.GetKeyDown(KeyCode.S)&& !backwardPressed)
            {
                
                sp.GetComponent<Rigidbody>().AddForce(sp.transform.forward * 20, ForceMode.Impulse);
                backwardPressed = true;
                forwardPressed = false;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                attached = false;
                rb.useGravity = true;
                pSwitch.dismounted = true;
                backwardPressed = false;
                forwardPressed = false;
                currentPipe = null;
                rb.velocity = Vector3.zero;
                //Debug.Log("AMJAMPINGAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                rb.velocity = new Vector3(0, 3, 3);
                //pm.currentState = PlayerMovement.PlayerState.pipejumping;
            }
        }else
        {
            backwardPressed = false;
            forwardPressed = false;
        }
        if (currentPipe != null)
        {
           
            attached = true;
            
        }


       
    }
}
