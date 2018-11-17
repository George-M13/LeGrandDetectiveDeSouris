using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookController : MonoBehaviour {

    float rotX;
    float rotY;
    float mouseSens = 200;

    public GameObject player;
    public PlayerMovement pm;
	
	void Start () {
        pm = player.GetComponent<PlayerMovement>();
        Vector3 rot = transform.localRotation.eulerAngles;//Store rotation to modify it
        rotX = rot.x;
        rotY = rot.y;

	}
	
	
	void LateUpdate () {
        float mouseX = Input.GetAxis("Mouse X"); //Store the mouse input
        float mouseY = -Input.GetAxis("Mouse Y");

        rotX += mouseX * mouseSens * Time.deltaTime; //Add the mouse input to the camera's rotation
        rotY += mouseY * mouseSens * Time.deltaTime;
        rotY = Mathf.Clamp(rotY, -80f, 80f);

        if (!pm.IsWallRunning)
        {
            Quaternion playerRot = Quaternion.Euler(0.0f, rotX, 0.0f);//Store just the x rotation
            player.transform.rotation = playerRot;//Rotate the player by the x axis with the camera
        }                  //so that the player's direction changes with the camera movement

        Quaternion localRot = Quaternion.Euler(rotY, rotX, 0.0f); //Combine the seperate rotations store them as rotations
        this.transform.rotation = localRot; //Rotate the camera

        



    }
}
