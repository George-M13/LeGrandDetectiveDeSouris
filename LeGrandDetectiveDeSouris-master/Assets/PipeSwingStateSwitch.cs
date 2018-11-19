using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSwingStateSwitch : MonoBehaviour {
    public GameObject player;
    public PlayerMovement pm;
    public PipeSwing ps;
    public HingeJoint hj;
    public bool dismounted;
    public GameObject swingPoint;
    public Transform startPos;

    // Use this for initialization
    void Start () {
        //hj = this.GetComponent<HingeJoint>();
        player = GameObject.FindGameObjectWithTag("player");
        pm = player.GetComponent<PlayerMovement>();
        ps = player.GetComponent<PipeSwing>();
        swingPoint = transform.parent.GetChild(0).gameObject;

    }
	
	// Update is called once per frame
	void Update () {
        if (dismounted)
        {
            this.GetComponent<BoxCollider>().enabled = false;
            
        }
        //if((ps.currentPipe != this.gameObject && ps.currentPipe != null )|| pm.IsGrounded) this.GetComponent<BoxCollider>().enabled = true;
        if (pm.IsGrounded) dismounted = false;

       
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "player" && !this.dismounted)
        {
            ps.playerPos = this.transform.gameObject;
            swingPoint.transform.position = col.transform.position;
            pm.currentState = PlayerMovement.PlayerState.pipeswinging;
            ps.currentPipe = this.transform.gameObject;
            ps.sp = swingPoint;
            ps.Swing();
            //hj.connectedAnchor = ps.currentPipe.transform.position;
            //hj.connectedBody = pm.gameObject.GetComponent<Rigidbody>();
        }
    }

    

}
