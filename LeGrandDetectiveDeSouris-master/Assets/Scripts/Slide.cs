using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour {
    Rigidbody rb;
    PlayerMovement pm;
    SphereCollider col;
    public Camera _camera;
    public Transform CHeight;
    public Transform NewHeight;
        
    public float speed = 10;

    float r;
	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
        col = this.GetComponent<SphereCollider>();
        pm = this.GetComponent<PlayerMovement>();
        r = col.radius;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftControl) && pm.IsGrounded)
        {
            speed = pm.zSpeed + 2;
        }
        if (Input.GetKey(KeyCode.LeftControl) && pm.IsGrounded)
        {
            col.radius /= 2;
            _camera.transform.position = NewHeight.transform.position;
            pm.IsSliding = true;
            Sliding();
            //Debug.Log(rb.velocity.z);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            col.radius = r;
            _camera.transform.position = CHeight.transform.position;
            speed = 10;
            pm.IsSliding = false;
        }
        if (speed <= 0) speed = 0;
	}

    void Sliding()
    {
        pm.currentState = PlayerMovement.PlayerState.sliding;
        pm.zSpeed = speed;
        speed = speed - 3*Time.deltaTime;
    }
}
