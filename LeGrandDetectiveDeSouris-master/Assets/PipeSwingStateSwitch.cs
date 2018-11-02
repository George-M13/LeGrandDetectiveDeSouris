using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSwingStateSwitch : MonoBehaviour {

    public PlayerMovement pm;
    public PipeSwing ps;
    HingeJoint hj;

	// Use this for initialization
	void Start () {
        hj = this.GetComponent<HingeJoint>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerStay(Collider col)
    {
        if (col.transform.tag == "player")
        {
            pm.currentState = PlayerMovement.PlayerState.pipeswinging;
            ps.currentPipe = this.transform.parent.gameObject;
            hj.connectedBody = pm.gameObject.GetComponent<Rigidbody>();
        }
    }

}
