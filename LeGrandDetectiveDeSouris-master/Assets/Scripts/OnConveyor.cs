using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnConveyor : MonoBehaviour {
    GameObject player;
    PlayerMovement pm;
    public Rigidbody rb;
    public Rigidbody cRb;
    public BoxCollider col;
    public float strength = 50;
    Vector3 move;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("player");
        //rb = player.transform.parent.GetComponent<Rigidbody>();
        col = this.GetComponent<BoxCollider>();
        pm = rb.gameObject.GetComponent<PlayerMovement>();
        
    }

    void Update()
    {
    }

    public void OnTriggerStay(Collider coll)
    {
        if (coll.transform.parent.gameObject.tag == "player" && (pm.currentState == PlayerMovement.PlayerState.idle || pm.currentState == PlayerMovement.PlayerState.movingbackward || pm.currentState == PlayerMovement.PlayerState.movingforward))
        {
            move = this.transform.forward;
            move.y = 0;
            rb.AddForce(move*strength , ForceMode.Force);

        }
        if (coll.transform.GetComponent<Rigidbody>() != null) coll.transform.GetComponent<Rigidbody>().AddForce(this.transform.forward * strength / 4, ForceMode.Force);
    }

    public void OnTriggerExit(Collider coll)
    {
        if (coll.transform.parent.gameObject.tag == "player")
        {
            move.y = 0;
            rb.velocity = Vector3.zero;
            rb.AddForce(move * strength/2, ForceMode.Force);
            //if (pm.currentState != PlayerMovement.PlayerState.movingforward) pm.z = 1;

        }
    }
}
