using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank : MonoBehaviour {
    float blood = 100f;
	// Use this for initialization
	void Start () {
        blood = 100f;
	}

    public float getblood()
    {
        return blood;
    }
    public void setblood(float b)
    {
        blood = b;
    }

    public void shoot(TankType type)
    {
        GameObject bullet = Singleton<factory>.Instance.getBullets(type);
        bullet.transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z) + transform.forward * 1.5f;
        bullet.transform.forward = transform.forward; //方向
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 20, ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
