using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class control : MonoBehaviour, IVirtualButtonEventHandler
{
    //public GameObject vb;
    public Animator ani;
    public VirtualButtonBehaviour[] vbs;
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        if (vb.VirtualButtonName == "vb_2")
        {
            ani.gameObject.transform.position += new Vector3(1, 0, 0);
        }
        else
        {
            ani.gameObject.transform.position += new Vector3(-1, 0, 0);
        }
        
        Debug.Log(vb.VirtualButtonName);
        //throw new System.NotImplementedException();
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        //Debug.Log("button2");
        //throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        //VirtualButtonBehaviour vbb = vb.GetComponent<VirtualButtonBehaviour>();
        for(int i = 0; i < vbs.Length; i++)
        {
            vbs[i].RegisterEventHandler(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
