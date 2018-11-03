﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnConveyor : MonoBehaviour {

    PlayerMovement pm;
    public Rigidbody rb;
    public Rigidbody cRb;
    public BoxCollider col;
    public float strength = 50;

    void Start()
    {
        col = this.GetComponent<BoxCollider>();
        pm = rb.gameObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {

    }

    public void OnTriggerStay(Collider coll)
    {
        if (coll.transform.gameObject.tag == "player")
        {
            rb.AddForce(this.transform.forward * (strength), ForceMode.Force);
            
        }
        if (coll.transform.GetComponent<Rigidbody>() != null) coll.transform.GetComponent<Rigidbody>().AddForce(this.transform.forward * strength / 4, ForceMode.Force);
    }

    public void OnTriggerExit(Collider coll)
    {
        if (coll.transform.gameObject.tag == "player")
        {
            if (pm.currentState != PlayerMovement.PlayerState.moving) pm.z = 1;

        }
    }
}