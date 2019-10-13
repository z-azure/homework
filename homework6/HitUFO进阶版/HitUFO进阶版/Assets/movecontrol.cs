using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface grativity
{
    void setGrativity(GameObject obj, Vector3 dir);
    void removeGrativity(GameObject obj);
}

public class adapter : MonoBehaviour, grativity {


    /*void setGrativity(GameObject obj)
    {
        obj.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.down * 9.8f);
    }*/

    public void setGrativity(GameObject obj, Vector3 dir)
    {
        //obj.gameObject.AddComponent<Rigidbody>();
        //obj.gameObject.GetComponent<Rigidbody>().AddForce(dir * 9.8f);
        obj.gameObject.AddComponent<Rigidbody>();
        obj.gameObject.GetComponent<Rigidbody>().AddForce(dir);
        obj.gameObject.GetComponent<Rigidbody>().useGravity = false;
        //throw new System.NotImplementedException();
    }
    public void removeGrativity(GameObject obj)
    {
        obj.AddComponent<Rigidbody>();
        //u.GetComponent<Rigidbody>().AddForce(-dir[num]);
        Destroy(obj.GetComponent<Rigidbody>());
    }

    public void setmoveable(GameObject obj)
    {
        if (obj.gameObject.GetComponent<Rigidbody>() != null)
        {
            Destroy(obj.gameObject.GetComponent<Rigidbody>());
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

