using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThisObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, 15f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
