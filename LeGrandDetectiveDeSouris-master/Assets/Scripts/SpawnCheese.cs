using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCheese : MonoBehaviour {
    public GameObject cheese;
    public Transform spawnPos;
    float timer;
    bool CanSpawn = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timer >= 2)
        {
            Instantiate(cheese, spawnPos.position, Quaternion.identity);
            timer = 0;
        }
        if (timer < 2) timer += Time.deltaTime;
    }

}
