using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slippery : MonoBehaviour {
    GameObject player;
    public Rigidbody rb;
    public float speed;
    float angle;
    public float Slippyness = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        player = GameObject.FindGameObjectWithTag("player");
        rb = player.GetComponent<Rigidbody>();
        angle = this.transform.eulerAngles.x;
        //Debug.Log(angle);
	}
    public void OnCollisionEnter(Collision col)
    {
        speed = 1;

    }

    public void OnCollisionStay(Collision col)
    {
        if (col.transform.tag == "player")
        {
            rb.AddForce(Vector3.forward * speed, ForceMode.Force);
            speed += Time.deltaTime*(angle * Slippyness);

            rb.gameObject.GetComponent<PlayerMovement>().currentState = PlayerMovement.PlayerState.movingforward;
        }
        else
        {
            speed = 1;
        }
    }
}
