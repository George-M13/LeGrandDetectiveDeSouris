using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmContoller : MonoBehaviour
{
    public WallRunning wr;
    public bool isRight = true;

    public PlayerMovement PlayerMovementVars;

    public GameObject EndOfArmContainer;
    public GameObject BegOfArmContainer;

    public GameObject EndOfArmModel;
    public GameObject BegOfArmModel;

    public GameObject Player;

    private float Speed;
    private float step;

    private float endLength;
    private float begLength;




    public List<Quaternion> adjustedValues;

    public List<Vector3> ForearmRun = new List<Vector3>();
    public List<Vector3> UpperArmRun = new List<Vector3>();

    public List<Vector3> ForearmIdle = new List<Vector3>();
    public List<Vector3> UpperArmIdle = new List<Vector3>();

    public List<Vector3> ForearmFalling = new List<Vector3>();
    public List<Vector3> UpperArmFalling = new List<Vector3>();

    public List<Vector3> ForearmJumping = new List<Vector3>();
    public List<Vector3> UpperArmJumping = new List<Vector3>();

    /* public Vector3 ForearmRun2;
     public Vector3 UpperArmRun2;*/

    //private int PlayerMovementVars.CurrentForearmCycle = 0;
    private int currentUpperArmCycle = 0;
    // Use this for initialization
    void Start()
    {
        Speed = 4;
        endLength = EndOfArmModel.GetComponent<MeshRenderer>().bounds.extents.x;
        begLength = BegOfArmModel.GetComponent<MeshRenderer>().bounds.extents.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
      
        switch (PlayerMovementVars.currentState)
        {
            case PlayerMovement.PlayerState.movingforward:
                Speed = (PlayerMovementVars.localVel.z) * 2;
                step = (Speed * Time.deltaTime);

                Animate(ForearmRun, UpperArmRun);
                break;


            case PlayerMovement.PlayerState.idle:

                Speed = 1f;
                step = (Speed * Time.deltaTime);

                Animate(ForearmIdle, UpperArmIdle);

                break;
            case PlayerMovement.PlayerState.leftwallrunning:
                break;
            case PlayerMovement.PlayerState.falling:
                Speed = (PlayerMovementVars.localVel.y) * 40;

                step = (Speed * Time.deltaTime);

                Animate(ForearmFalling, UpperArmFalling);

                break;
            case PlayerMovement.PlayerState.climbing:
                break;
            case PlayerMovement.PlayerState.jumping:
                PlayerMovementVars.CurrentForearmCycle = 0;

                Speed = 1f;
                step = (Speed * Time.deltaTime);

                Animate(ForearmJumping, UpperArmJumping);
                break;
            case PlayerMovement.PlayerState.walljumping:
                break;
            case PlayerMovement.PlayerState.pipeclimbing:
                break;
            case PlayerMovement.PlayerState.pipeswinging:
                break;
            case PlayerMovement.PlayerState.pipejumping:
                break;

            default:
                //Console.WriteLine("Default case");
                break;
        }
    }
    private void cycleOutOfRangeCheck(List<Vector3> animationCycle)
    {
        if (PlayerMovementVars.CurrentForearmCycle > animationCycle.Count - 1) PlayerMovementVars.CurrentForearmCycle = 0;
    }
    private void cycleChange(List<Vector3> animationCycle, Quaternion ForeArmAdjusted)
    {
        

        if (BegOfArmContainer.transform.rotation == ForeArmAdjusted)
        {
            PlayerMovementVars.CurrentForearmCycle++;
            if (PlayerMovementVars.CurrentForearmCycle == animationCycle.Count) PlayerMovementVars.CurrentForearmCycle = 0;
        }
        else if (BegOfArmContainer.transform.rotation == new Quaternion(ForeArmAdjusted.x * -1, ForeArmAdjusted.y * -1, ForeArmAdjusted.z * -1, ForeArmAdjusted.w * -1))
        {
            PlayerMovementVars.CurrentForearmCycle++;
            if (PlayerMovementVars.CurrentForearmCycle == animationCycle.Count) PlayerMovementVars.CurrentForearmCycle = 0;
        }
    }
    private List<Quaternion> AdjustQuaternion(List<Vector3> foreArmCycle, List<Vector3> upperArmCycle)
    {
        List<Quaternion> retVal = new List<Quaternion>();



        if (isRight)
        {
            retVal.Add(Quaternion.Euler(new Vector3(foreArmCycle[PlayerMovementVars.CurrentForearmCycle].x + Player.transform.eulerAngles.x, foreArmCycle[PlayerMovementVars.CurrentForearmCycle].y + Player.transform.eulerAngles.y, foreArmCycle[PlayerMovementVars.CurrentForearmCycle].z + Player.transform.eulerAngles.z)));
            retVal.Add(Quaternion.Euler(new Vector3(upperArmCycle[PlayerMovementVars.CurrentForearmCycle].x + Player.transform.eulerAngles.x, upperArmCycle[PlayerMovementVars.CurrentForearmCycle].y + Player.transform.eulerAngles.y, upperArmCycle[PlayerMovementVars.CurrentForearmCycle].z + Player.transform.eulerAngles.z)));
        }
        else
        {
            retVal.Add(Quaternion.Euler(new Vector3((foreArmCycle[PlayerMovementVars.CurrentForearmCycle].x * -1) + Player.transform.eulerAngles.x, (((foreArmCycle[PlayerMovementVars.CurrentForearmCycle].y - 90) * -1) + 90) + Player.transform.eulerAngles.y, foreArmCycle[PlayerMovementVars.CurrentForearmCycle].z + Player.transform.eulerAngles.z)));
            retVal.Add(Quaternion.Euler(new Vector3(upperArmCycle[PlayerMovementVars.CurrentForearmCycle].x + Player.transform.eulerAngles.x, upperArmCycle[PlayerMovementVars.CurrentForearmCycle].y + Player.transform.eulerAngles.y, upperArmCycle[PlayerMovementVars.CurrentForearmCycle].z + Player.transform.eulerAngles.z)));
        }

        return retVal;
    }

    private void Animate(List<Vector3> foreArmCycle, List<Vector3> upperArmCycle)
    {
        cycleOutOfRangeCheck(foreArmCycle);
        adjustedValues = AdjustQuaternion(foreArmCycle, upperArmCycle);
        cycleChange(foreArmCycle, adjustedValues[0]);

        BegOfArmContainer.transform.rotation = Quaternion.Lerp(BegOfArmContainer.transform.rotation, adjustedValues[0], step);
        EndOfArmContainer.transform.rotation = Quaternion.Lerp(EndOfArmContainer.transform.rotation, adjustedValues[1], step);
    }
}

