using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnConveyor : MonoBehaviour {

    PlayerMovement pm;
    public Rigidbody rb;
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
        if(coll.transform.gameObject.tag == "player")
        {
            rb.AddForce(transform.forward * strength, ForceMode.Force);
        }
    }
}
