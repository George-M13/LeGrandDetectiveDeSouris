using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowawayMove : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        this.transform.Translate(Vector3.forward);
        Destroy(this.gameObject, 2);
	}
}
