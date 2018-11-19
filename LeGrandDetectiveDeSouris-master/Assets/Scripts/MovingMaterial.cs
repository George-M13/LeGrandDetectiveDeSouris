using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMaterial : MonoBehaviour {

    public MeshRenderer rend;
    public float h;
    public float speed = 2;
    public Material mat;

    // Use this for initialization
    void Start () {
        rend = this.GetComponent<MeshRenderer>();
        mat = rend.material;
        h = 0;
	}
	
	// Update is called once per frame
	void Update () {

        h -= Time.deltaTime / speed;
        rend.material.mainTextureOffset = new Vector2(0,h);
	}
}
