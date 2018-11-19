using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingSwitchState : MonoBehaviour {
    public GameObject player;
    Rigidbody rb;
    PlayerMovement pm;
    

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        rb = player.GetComponent<Rigidbody>();
        pm = player.GetComponent<PlayerMovement>();

    }

    public void OnCollisionStay(Collision col)
    {
        Debug.Log(col.transform.tag);
        if (col.transform.tag == "player")
        {
            /*RaycastHit hit;
            float top = this.GetComponent<MeshCollider>().bounds.extents.y;
            if (Physics.SphereCast(transform.position, this.transform.localScale.x/2, transform.up, out hit, top + 0.1f))
            {
                Debug.Log("hit");
                if (hit.transform.tag == "player" && col.transform.position.y >= this.transform.position.y)
                {
                    Debug.Log("player");
                    
                }
            }
            else
            {
                pm.currentState = PlayerMovement.PlayerState.pipeclimbing;
                rb.velocity = Vector3.zero;
            }*/
            pm.currentState = PlayerMovement.PlayerState.pipeclimbing;
            rb.velocity = Vector3.zero;
            pm.Climb(this.gameObject);


        }
        
    }

    public void OnCollisionExit(Collision col)
    {
        if (col.transform.tag == "player")
        {
            //pm.currentState = PlayerMovement.PlayerState.idle;
        }
    }
}
