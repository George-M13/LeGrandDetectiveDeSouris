using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmContoller : MonoBehaviour
{
    public WallRunning wr;
    public bool isRight = true;

    public GameObject EndOfArmContainer;
    public GameObject BegOfArmContainer;

    public GameObject EndOfArmModel;
    public GameObject BegOfArmModel;

    public GameObject Player;

    public float Speed;

    private float endLength;
    private float begLength;

    private float hypotenuse;

    private float elbowAngle;
    private float forearmAngle;
    private float upperArmAngle;

    public List<Vector3> ForearmRun = new List<Vector3>();
    public List<Vector3> UpperArmRun = new List<Vector3>();

    public Vector3 ForearmRun2;
    public Vector3 UpperArmRun2;

    private int currentForearmCycle = 0;
    private int currentUpperArmCycle = 0;
    // Use this for initialization
    void Start()
    {
        Speed = 4;
        endLength = EndOfArmModel.GetComponent<MeshRenderer>().bounds.extents.x;
        begLength = BegOfArmModel.GetComponent<MeshRenderer>().bounds.extents.x;

        /*ForearmRun1 = new Vector3;
        ForearmRun1 = new Vector3(0, 120, 210);
        UpperArmRun1 = new Vector3(0, 95, -200);*/

        ForearmRun2 = new Vector3(0, 110, 190);
        UpperArmRun2 = new Vector3(0, 95, -185);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float step = (Speed * Time.deltaTime);
        /*
        hypotenuse = EndOfArmContainer.transform.position.x - BegOfArmContainer.transform.position.y;
       

        elbowAngle = cosineRule(endLength, begLength, hypotenuse);
        forearmAngle = cosineRule(begLength, hypotenuse, endLength);
        upperArmAngle = cosineRule(endLength, hypotenuse, begLength);

        rotateToAngle(EndOfArmContainer, upperArmAngle, step);
        rotateToAngle(BegOfArmContainer, forearmAngle, step);
        */
        //speed = 4; have this as something that changes, normal looking movements are non linear



        Quaternion ForeArmAdjusted;
        Quaternion UpperArmAdjusted;


        //Debug.Log(ForearmRun[currentForearmCycle]);
        if (isRight) {
            ForeArmAdjusted = Quaternion.Euler(new Vector3(ForearmRun[currentForearmCycle].x + Player.transform.eulerAngles.x, ForearmRun[currentForearmCycle].y + Player.transform.eulerAngles.y, ForearmRun[currentForearmCycle].z + Player.transform.eulerAngles.z));
            UpperArmAdjusted = Quaternion.Euler(new Vector3(UpperArmRun[currentForearmCycle].x + Player.transform.eulerAngles.x, UpperArmRun[currentForearmCycle].y + Player.transform.eulerAngles.y, UpperArmRun[currentForearmCycle].z + Player.transform.eulerAngles.z));
        }
        else
        {
            ForeArmAdjusted = Quaternion.Euler(new Vector3((ForearmRun[currentForearmCycle].x *-1) + Player.transform.eulerAngles.x, (((ForearmRun[currentForearmCycle].y - 90) *-1) + 90) + Player.transform.eulerAngles.y, ForearmRun[currentForearmCycle].z + Player.transform.eulerAngles.z));
            UpperArmAdjusted = Quaternion.Euler(new Vector3(UpperArmRun[currentForearmCycle].x + Player.transform.eulerAngles.x, UpperArmRun[currentForearmCycle].y + Player.transform.eulerAngles.y , UpperArmRun[currentForearmCycle].z + Player.transform.eulerAngles.z));
        }

        if (BegOfArmContainer.transform.rotation == ForeArmAdjusted)
        {
            currentForearmCycle++;
            if (currentForearmCycle == ForearmRun.Count) currentForearmCycle = 0;
        }
        else if(BegOfArmContainer.transform.rotation == new Quaternion(ForeArmAdjusted.x*-1, ForeArmAdjusted.y *-1, ForeArmAdjusted.z *-1, ForeArmAdjusted.w *-1))
        {
            currentForearmCycle++;
            if (currentForearmCycle == ForearmRun.Count) currentForearmCycle = 0;
        }
        Debug.Log(BegOfArmContainer.transform.rotation);

        Debug.Log(ForeArmAdjusted);
        Debug.Log(new Quaternion(ForeArmAdjusted.x * -1, ForeArmAdjusted.y * -1, ForeArmAdjusted.z * -1, ForeArmAdjusted.w * -1));

        BegOfArmContainer.transform.rotation = Quaternion.Lerp(BegOfArmContainer.transform.rotation, ForeArmAdjusted, step);
        EndOfArmContainer.transform.rotation = Quaternion.Lerp(EndOfArmContainer.transform.rotation, UpperArmAdjusted, step);

        /*
        else if (BegOfArmContainer.transform.rotation == Quaternion.Euler(ForearmRun2))
        {
            isRun1 = true;
        }

        if(isRun1)
        {
            BegOfArmContainer.transform.rotation = Quaternion.Lerp(BegOfArmContainer.transform.rotation, Quaternion.Euler(ForearmRun1), step);
            //EndOfArmContainer.transform.rotation = Quaternion.Lerp(EndOfArmContainer.transform.rotation, Quaternion.Euler(UpperArmRun1), step);
        }
        else
        {
            BegOfArmContainer.transform.rotation = Quaternion.Lerp(BegOfArmContainer.transform.rotation, Quaternion.Euler(ForearmRun2), step);
           // EndOfArmContainer.transform.rotation = Quaternion.Lerp(EndOfArmContainer.transform.rotation, Quaternion.Euler(UpperArmRun2), step);
        }

        /* if(wr.hit.collider.tag == "Wall")
         {

             BegOfArmContainer.transform.position = Vector3.MoveTowards(this.transform.position, wr.hit.transform.position, step);
             Debug.Log(wr.hit.collider.gameObject.name);
         }*/



        /*if (Physics.Raycast(transform.position, transform.right, out hit, distToWall + 0.1f) && hit.collider.gameObject.tag == "wall" && grounded == false)
        {

            /*if (lastWall != hit.collider.gameObject) //Check that the last wall that the player was running on isn't the current wall
            {
                lastWall = hit.collider.gameObject; //If it isn't then store is as the new last wall
                pm.CanWallJump = true; // the player can wall jump
            }*/
    }

    private float cosineRule(float a, float b, float c)
    {
        float retVal = 0;

        retVal = Mathf.Acos(((a * a) + (b * b) - (c * c)) / 2 * a * b); //this is just the cosine rule. Pretty simple.

        return retVal;
    }
    private void rotateToAngle(GameObject rotatingObject, float angle, float step)
    {
        if (rotatingObject.transform.rotation.z != angle)
        {
            Quaternion target = new Quaternion(rotatingObject.transform.rotation.x, rotatingObject.transform.rotation.y, angle, rotatingObject.transform.rotation.w);
            rotatingObject.transform.rotation = Quaternion.RotateTowards(rotatingObject.transform.rotation, target, step);
        }
    }
}

