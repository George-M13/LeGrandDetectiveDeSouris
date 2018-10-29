using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody rb;
    Vector3 movement;
    WallRun wr;
    
    float x;
    float z;
    float ZEDdir;
    private float acceleration = 2f;
    public float zSpeed = 0;
    public float xSpeed = 0;
    public float MaxSpeed = 6;
    public float Dir;

    public bool IsJumping;
    public bool IsGrounded;
    public bool IsWallRunning = false;
    public bool CanWallJump;
    public bool CanWallRun = true;
    public bool IsSliding = false;
    public bool IsOnConveyor = false;
    public GameObject Conveyor;

    float velMap;


    public Vector3 localVel;

    public PlayerState currentState;

    public enum PlayerState
    {
        idle,
        moving,
        wallrunning,
        falling,
        climbing
    }


    void Start () {
        rb = this.GetComponent<Rigidbody>();
        wr = this.GetComponent<WallRun>();
	}


    void FixedUpdate()
    {

        switch (currentState)
        {
            case PlayerState.idle:
                xSpeed = 0;
                zSpeed = 0;
                break;
            case PlayerState.moving:
                Move();
                break;
            case PlayerState.wallrunning:
                break;
            case PlayerState.falling:
                break;
        }
        if (!IsWallRunning) x = Input.GetAxis("Horizontal");
        //Get the input
        z = Input.GetAxis("Vertical");

        //Debug.Log(rb.velocity);

        //Debug.Log(ZEDdir);

        localVel = transform.InverseTransformDirection(rb.velocity);//Convert velocity from world to local
        localVel.x = x * xSpeed; //Modify the velocity
                                 /*if (transform.rotation.y > 0 && transform.rotation.y < 1)
                                 {
                                     ZEDdir = 1;
                                 }
                                 else ZEDdir = -1;*/

        localVel.z = z * zSpeed;
        rb.velocity = transform.TransformDirection(localVel);//Convert back from local to world
        if (zSpeed >= MaxSpeed && (!IsSliding)) zSpeed = MaxSpeed;
        if (xSpeed >= 6 && (!IsSliding)) xSpeed = 6;//Limit the speed

        if (currentState != PlayerState.wallrunning && isMoving()) currentState = PlayerState.moving;

        if (!isMoving()) currentState = PlayerState.idle;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded == true)//If the player presses the space bar they can jump
        {
            this.rb.velocity += Vector3.up * 5.3f;
            IsJumping = true;
            IsGrounded = false;
        }

        if (IsGrounded) IsJumping = false;


        RaycastHit hit;
        if (rb.velocity.z > 1)
        {
            Dir = 1;
            velMap = Map(1, 6, 1, 2, rb.velocity.z); //Map the players Z velocity from 0 - 10 to 1 - 3
                                                     //so that the player jumps earlier if they are going faster
        }
        else if (rb.velocity.z <= -1)
        {
            Dir = -1;
            velMap = Map(-1, -6, 1, 3, rb.velocity.z);
        }
        else velMap = 0f;


        //Check if the raycast hit in the mapped distance 
        //while moving above a certain speed
        //and not jumping so that it can't trigger in air
        /*if (Physics.Raycast(transform.position, transform.forward, out hit, velMap) && IsGrounded == true && (rb.velocity.z >= 4 || rb.velocity.z <= -4) && hit.collider.tag == "climable") 
         {
             Vault(5.5f);
         }*/

        if (Input.GetKeyDown(KeyCode.S) && IsGrounded)
        {
            BackStep();
        }

    }

    //Movement check 
    //only returns true if moving
    bool isMoving() 
    {
        if (x > 0 || x < 0 || z > 0 || z < 0) return true;
        else return false;
    }


    //Make the player jump
    public void Vault(float h) 
    {
        this.rb.velocity += Vector3.up * h;
        IsGrounded = false;
    }
    


    //Maps a value from an old range to a new range
    //Used the maths that the processing map function uses
    //https://github.com/processing/processing/blob/master/core/src/processing/core/PApplet.java#L4844

    public float Map(float oldMin, float oldMax, float newMin, float newMax, float oldNum)
    {

        float oldRange = (oldMax - oldMin);
        float newRange = (newMax - newMin);
        float newNum = newMin + newRange * ((oldNum - oldMin)/oldRange);

        return (newNum);
    }

    public void BackStep()
    {
        rb.velocity += transform.up * 3;
        rb.velocity -= new Vector3(0, 0, 3);
    }

    public void Move()
    {
        x = Input.GetAxis("Horizontal");
        //Get the input
        z = Input.GetAxis("Vertical");

        if ( !IsSliding)
        {
            zSpeed += Time.deltaTime * acceleration;
            xSpeed += Time.deltaTime * acceleration;
        } //Add acceleration if the player is moving
        else if (!IsSliding)
        {
            
        }
        if (IsGrounded) IsWallRunning = false;
        

            
    }
}
