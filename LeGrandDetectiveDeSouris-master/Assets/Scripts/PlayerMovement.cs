using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Rigidbody rb;
    Vector3 movement;
    WallRun wr;
    
    float x;
    public float z;
    float ZEDdir;
    float slideSpeed = 0;
    public float slideCounter = 0;

    public int CurrentForearmCycle = 0;

    private float acceleration = 2f;
    public float zSpeed = 1;
    public float xSpeed = 1;
    public float MaxSpeed = 6;
    public float Dir;
    public float count = 0;

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
        movingforward,
        movingbackward,
        leftwallrunning,
        rightwallrunning,
        falling,
        climbing,
        jumping,
        walljumping,
        gettingup,
        pipeclimbing,
        pipeclimbingidle,
        pipeclimbingturn,
        pipeswinging,
        pipejumping,
        sliding
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
                xSpeed = 1;
                zSpeed = 1;
                break;
            case PlayerState.movingforward:
                Move();
                break;
            case PlayerState.movingbackward:
                Move();
                break;;
            case PlayerState.falling:
                
                break;
            case PlayerState.jumping:
                //Move();
                count += 2 * Time.deltaTime;
                if (count >= 1.5f)
                {
                    currentState = PlayerState.falling;
                    count = 0;
                }
                break;
            case PlayerState.pipeclimbing:
                zSpeed = 0;
                xSpeed = 0;
                break;
            case PlayerState.pipeswinging:
               
                break;
        }
        if(currentState == PlayerState.jumping)
        {
            if (rb.velocity.y <= 0) currentState = PlayerState.falling;
        }
        if (!IsWallRunning) x = Input.GetAxis("Horizontal");
        //Get the input
        if(currentState != PlayerState.pipeclimbing) z = Input.GetAxis("Vertical");
        //Debug.Log(currentState);
        //Debug.Log(rb.velocity);

        //Debug.Log(ZEDdir);
        //Debug.Log(currentState);
        localVel = transform.InverseTransformDirection(rb.velocity);//Convert velocity from world to local
        localVel.x = x * xSpeed; //Modify the velocity
                                 /*if (transform.rotation.y > 0 && transform.rotation.y < 1)
                                 {
                                     ZEDdir = 1;
                                 }
                                 else ZEDdir = -1;*/

        localVel.z = z * zSpeed;
        if (currentState == PlayerState.pipeclimbing || currentState == PlayerState.pipeswinging || currentState == PlayerState.climbing || currentState == PlayerState.gettingup || currentState == PlayerState.jumping || currentState == PlayerState.falling) {


        } else if(IsGrounded)
        {
            
            rb.velocity = transform.TransformDirection(localVel);
        }
        //Debug.Log(rb.velocity.z);

        //Convert back from local to world

        if (zSpeed >= MaxSpeed && (!IsSliding)) zSpeed = MaxSpeed;
        if (zSpeed >= 10 && (!IsSliding)) zSpeed = 10;
        if (xSpeed >= 6 && (!IsSliding)) xSpeed = 6;//Limit the speed

        if (IsGrounded && isMoving() && currentState != PlayerState.jumping) currentState = PlayerState.movingforward;

        if (!isMoving()&&IsGrounded)
        {
            currentState = PlayerState.idle;
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded == true)//If the player presses the space bar they can jump
        {
            IsGrounded = false;
            currentState = PlayerState.jumping;
            this.rb.velocity += Vector3.up * 5f;
        }

        //if (IsGrounded) IsJumping = false;


        RaycastHit hit;
        if (rb.velocity.z >= 1)
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
        //while movingforward above a certain speed
        //and not jumping so that it can't trigger in air
        /*if (Physics.Raycast(transform.position, transform.forward, out hit, velMap) && IsGrounded == true && (rb.velocity.z >= 4 || rb.velocity.z <= -4) && hit.collider.tag == "climable") 
         {
             Vault(5.5f);
         }*/

        if (Input.GetKeyDown(KeyCode.S) && IsGrounded)
        {
            BackStep();
        }

        //if (currentState == PlayerState.movingforward && !IsGrounded ) currentState = PlayerState.falling;

    }

    //Movement check 
    //only returns true if movingforward
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
        if (z > 0) currentState = PlayerState.movingforward;
        else if (z < 0) currentState = PlayerState.movingbackward;

        if (!IsSliding)
        {
            zSpeed += Time.deltaTime * acceleration;
            xSpeed += Time.deltaTime * acceleration;
        } //Add acceleration if the player is movingforward
        else if (!IsSliding)
        {
            
        }
        if (IsGrounded) IsWallRunning = false;
        
    }

    public void Climb(GameObject target)
    {
        
        x = Input.GetAxis("Horizontal");
        //Get the input
        float y = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(0, y * slideSpeed, 0);
        if (y == 0) currentState = PlayerState.pipeclimbingidle;
        if (y == 1) slideSpeed = 3;
        if (y == -1)
        {
            slideSpeed = 1;
            slideCounter += Time.deltaTime;

        }
        else slideCounter = 0;
        if (slideCounter >= 1) slideSpeed = 5;
        else slideSpeed = 1;
        //Debug.Log(-(x * 30 * Time.deltaTime));
        transform.RotateAround(target.transform.position,transform.up, -(x * 30 * Time.deltaTime));
        if (x > 0 || x < 0) currentState = PlayerState.pipeclimbingturn;
        if (Input.GetKey(KeyCode.Space)){
            currentState = PlayerState.jumping;
            zSpeed = 3;
            rb.velocity = transform.up * 3;
            rb.velocity = transform.forward * 5;
            

        }
        
    }
}
