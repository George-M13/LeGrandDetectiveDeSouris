using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour {

    public PlayerMovement PlayerMovementVars;

    public List<Vector3> IdleAnimationPosition;
    public List<Vector3> IdleAnimationRotation;
    private int IdleAnimationPositionCounter = 0;
    private int IdleAnimationAnimationCounter = 0;
    public float IdleHeadBobSpeed = 3;

    public List<Vector3> RunAnimationPosition;
    public List<Vector3> RunAnimationRotation;
    private int RunAnimationPositionCounter = 0;
    private int RunAnimationAnimationCounter = 0;

    public List<Vector3> WallRunAnimationPosition;
    public List<Vector3> WallRunAnimationRotation;
    private int WallRunAnimationPositionCounter = 0;
    private int WallRunAnimationRotationCounter = 0;

    private float step;


    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void LateUpdate()
    {
        switch (PlayerMovementVars.currentState)
        {
            case PlayerMovement.PlayerState.movingforward:
                float speed = Mathf.Abs((PlayerMovementVars.localVel.z) / 5);
                step = Time.deltaTime * speed;

                this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, RunAnimationPosition[RunAnimationPositionCounter], step);
                if (this.transform.localPosition == RunAnimationPosition[RunAnimationPositionCounter])
                {
                    RunAnimationPositionCounter++;
                    if (RunAnimationPositionCounter > (RunAnimationPosition.Count - 1)) RunAnimationPositionCounter = 0;
                }

                break;


            case PlayerMovement.PlayerState.idle:

                step = Time.deltaTime * IdleHeadBobSpeed;

                this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, IdleAnimationPosition[IdleAnimationPositionCounter], step);
                if (this.transform.localPosition == IdleAnimationPosition[IdleAnimationPositionCounter])
                {
                    IdleAnimationPositionCounter++;
                    if (IdleAnimationPositionCounter > (IdleAnimationPosition.Count - 1)) IdleAnimationPositionCounter = 0;
                }
                
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(IdleAnimationRotation[IdleAnimationAnimationCounter]), step);

                break;
            case PlayerMovement.PlayerState.leftwallrunning:
                step = Time.deltaTime * 6;
                Debug.Log("Heyo " + WallRunAnimationRotation[0]);
                //his.transform.rotation = Quaternion.AngleAxis(30, Vector3.forward);
                //this.transform.rotation = Quaternion.Euler(WallRunAnimationRotation[WallRunAnimationRotationCounter]);
                Debug.Log(this.transform.rotation.z);
                Quaternion adjustedValue =  (Quaternion.Euler(this.transform.localRotation.x, this.transform.localRotation.y, WallRunAnimationRotation[WallRunAnimationRotationCounter].z));
                this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, adjustedValue, step);
                break;
            case PlayerMovement.PlayerState.falling:

                break;
            case PlayerMovement.PlayerState.climbing:
                break;
            case PlayerMovement.PlayerState.jumping:
                break;
            case PlayerMovement.PlayerState.walljumping:
                break;
            case PlayerMovement.PlayerState.pipeclimbing:
                break;
            case PlayerMovement.PlayerState.pipeswinging:
                break;
            case PlayerMovement.PlayerState.pipejumping:
                break;
        }
    }
}
