using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

    GameObject a;
	// Use this for initialization
	void Start () {
		a= Instantiate(Resources.Load<GameObject>("ufo_u2"), new Vector3(0,0,0), Quaternion.identity);
        //a= GameObject.CreatePrimitive(PrimitiveType.Capsule);
    }
    int i = 0;
	// Update is called once per frame
	void Update () {
        //a.AddComponent<Rigidbody>();
        if (i == 0)
        {
            a.GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0));
            
            Destroy(a.gameObject.GetComponent<Rigidbody>());
            Debug.Log(a.GetComponent<Rigidbody>());
            i = 1;
        }
	}
}
