using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookcamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(Camera.main.transform.position);
    }
}
