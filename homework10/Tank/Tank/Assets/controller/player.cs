using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : tank {
    public delegate void destroy_player();
    public static event destroy_player destroy_event;
	// Use this for initialization
	void Start () {
        setblood(100f);
	}
	
	// Update is called once per frame
	void Update () {
        if (getblood() <= 0)
        {
            gameObject.SetActive(false);
            destroy_event();
        }
	}

    public void moveup()
    {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 10;
    }

    public void movedown()
    {
        gameObject.GetComponent<Rigidbody>().velocity = -gameObject.transform.forward * 10;
    }

    public void turn(float jiao)
    {
        //根据主轴旋转
        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y + jiao * 2, 0);
    }
}
