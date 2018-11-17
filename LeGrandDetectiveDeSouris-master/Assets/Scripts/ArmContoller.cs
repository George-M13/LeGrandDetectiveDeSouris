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


    private float time;

    public List<Quaternion> adjustedValues;

    public List<Vector3> ForearmRun = new List<Vector3>();
    public List<Vector3> UpperArmRun = new List<Vector3>();
    public List<List<Vector2>> RunBezier = new List<List<Vector2>>();
    public float runDuration;

    public List<Vector3> ForearmIdle = new List<Vector3>();
    public List<Vector3> UpperArmIdle = new List<Vector3>();
    public List<List<Vector2>> IdleBezier = new List<List<Vector2>>();
    public float idleDuration;


    public List<Vector3> ForearmFalling = new List<Vector3>();
    public List<Vector3> UpperArmFalling = new List<Vector3>();
    public List<List<Vector2>> FallingBezier = new List<List<Vector2>>();
    public float fallingDuration;


    public List<Vector3> ForearmJumping = new List<Vector3>();
    public List<Vector3> UpperArmJumping = new List<Vector3>();
    public List<List<Vector2>> JumpingBezier = new List<List<Vector2>>();
    public float jumpingDuration;


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
        time += Time.deltaTime;
      
        switch (PlayerMovementVars.currentState)
        {
            case PlayerMovement.PlayerState.movingforward:
                Speed = (PlayerMovementVars.localVel.z) * 2;
                step = (Speed * Time.deltaTime);

                Animate(ForearmRun, UpperArmRun, runDuration, RunBezier);
                break;


            case PlayerMovement.PlayerState.idle:

                Speed = 1f;
                step = (Speed * Time.deltaTime);

                Animate(ForearmIdle, UpperArmIdle, idleDuration, IdleBezier);

                break;
            case PlayerMovement.PlayerState.leftwallrunning:
                break;
            case PlayerMovement.PlayerState.falling:
                Speed = (PlayerMovementVars.localVel.y) * 10;

                step = (Speed * Time.deltaTime);

                Animate(ForearmFalling, UpperArmFalling, fallingDuration, FallingBezier);

                break;
            case PlayerMovement.PlayerState.climbing:
                break;
            case PlayerMovement.PlayerState.jumping:
                PlayerMovementVars.CurrentForearmCycle = 0;

                Speed = 1f;
                step = (Speed * Time.deltaTime);

                Animate(ForearmJumping, UpperArmJumping, jumpingDuration, JumpingBezier);
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
    private List<Quaternion> AdjustQuaternion(List<Vector3> foreArmCycle, List<Vector3> upperArmCycle, float animationDuration, List<List<Vector2>> bezier)
    {
        List<Quaternion> retVal = new List<Quaternion>();

        Vector3 foreArmBeziered = BezierGraph(foreArmCycle[PlayerMovementVars.CurrentForearmCycle], bezier[PlayerMovementVars.CurrentForearmCycle][0], bezier[PlayerMovementVars.CurrentForearmCycle][1], animationDuration);
        Vector3 upperArmBeziered = BezierGraph(foreArmCycle[PlayerMovementVars.CurrentForearmCycle], bezier[PlayerMovementVars.CurrentForearmCycle][0], bezier[PlayerMovementVars.CurrentForearmCycle][1], animationDuration);

        if (isRight)
        {
            retVal.Add(Quaternion.Euler(new Vector3(foreArmBeziered.x + Player.transform.eulerAngles.x, foreArmBeziered.y + Player.transform.eulerAngles.y, foreArmBeziered.z + Player.transform.eulerAngles.z)));
            retVal.Add(Quaternion.Euler(new Vector3(upperArmBeziered.x + Player.transform.eulerAngles.x, upperArmBeziered.y + Player.transform.eulerAngles.y, upperArmBeziered.z + Player.transform.eulerAngles.z)));
        }
        else
        {
            retVal.Add(Quaternion.Euler(new Vector3((foreArmBeziered.x * -1) + Player.transform.eulerAngles.x, (((foreArmBeziered.y - 90) * -1) + 90) + Player.transform.eulerAngles.y, foreArmBeziered.z + Player.transform.eulerAngles.z)));
            retVal.Add(Quaternion.Euler(new Vector3(upperArmBeziered.x + Player.transform.eulerAngles.x, upperArmBeziered.y + Player.transform.eulerAngles.y, upperArmBeziered.z + Player.transform.eulerAngles.z)));
        }

        return retVal;
    }

    private void Animate(List<Vector3> foreArmCycle, List<Vector3> upperArmCycle, float animationDuration, List<List<Vector2>> bezier)
    {
        cycleOutOfRangeCheck(foreArmCycle);
        adjustedValues = AdjustQuaternion(foreArmCycle, upperArmCycle, animationDuration, bezier);
        cycleChange(foreArmCycle, adjustedValues[0]);

        BegOfArmContainer.transform.rotation = Quaternion.RotateTowards(BegOfArmContainer.transform.rotation, adjustedValues[0], step);
        EndOfArmContainer.transform.rotation = Quaternion.RotateTowards(EndOfArmContainer.transform.rotation, adjustedValues[1], step);

        //BegOfArmContainer.transform.rotation = Quaternion.Lerp(BegOfArmContainer.transform.rotation, adjustedValues[0], step);
        // EndOfArmContainer.transform.rotation = Quaternion.Lerp(EndOfArmContainer.transform.rotation, adjustedValues[1], step);
    }

    private Vector3 BezierGraph(Vector3 endState, Vector2 Bezier1, Vector2 Bezier2, float animationTime)
    {

        float timeTaken = time / animationTime;
        Vector2 startPos = new Vector2(0,0);
        Vector2 endPos = new Vector2(1, 1);


        Vector3 retVal = new Vector3(0, 0, 0);

        Vector2 point1 = CasteljauAlgorithm(timeTaken, startPos, Bezier1);
        Vector2 point2 = CasteljauAlgorithm(timeTaken, Bezier1, Bezier2);
        Vector2 point3 = CasteljauAlgorithm(timeTaken, Bezier2, endPos);


        Vector2 point4 = CasteljauAlgorithm(timeTaken, point1, point2);
        Vector2 point5 = CasteljauAlgorithm(timeTaken, point2, point3);

        Vector2 point6 = CasteljauAlgorithm(timeTaken, point4, point5);

        retVal = endState * point6.y;

        /* Vector3 point1 = CasteljauAlgorithm(timeTaken, currentState, Bezier1);
         Vector3 point2 = CasteljauAlgorithm(timeTaken, Bezier1, Bezier2);
         Vector3 point3 = CasteljauAlgorithm(timeTaken, Bezier2, endState);


         Vector3 point4 = CasteljauAlgorithm(timeTaken, point1, point2);
         Vector3 point5 = CasteljauAlgorithm(timeTaken, point2, point3);*/








        return retVal; 
    }
    private Vector3 CasteljauAlgorithm(float timeTaken, Vector2 point1, Vector2 point2)
    {
        Vector3 retVal = ((1 - timeTaken) * point2) -(timeTaken * point1);
        //de Casteljau's algorithm
        return retVal;
    }
}

