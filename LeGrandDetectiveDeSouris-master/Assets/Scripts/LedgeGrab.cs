using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrab : MonoBehaviour {
    PlayerMovement pm;


	// Use this for initialization
	void Start () {
        pm = this.GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit midHit;
        RaycastHit upHit;
        Debug.DrawRay(this.transform.position, this.transform.forward, Color.red);
        Debug.DrawRay(new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z), this.transform.forward, Color.red);
        if(Physics.Raycast(this.transform.position, this.transform.forward,out midHit,1f) && (pm.currentState == PlayerMovement.PlayerState.falling|| pm.currentState == PlayerMovement.PlayerState.jumping || pm.currentState == PlayerMovement.PlayerState.walljumping))
        {
            if(!Physics.Raycast(new Vector3(this.transform.position.x,this.transform.position.y + 1,this.transform.position.z),transform.forward,out upHit, 1f))
            {
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAITSMSJSJD}");
                pm.currentState = PlayerMovement.PlayerState.gettingup;
            }
        }
	}
}
