using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("水星").transform.RotateAround(Vector3.zero, new Vector3(0, 1, 0), 50 * Time.deltaTime);
        GameObject.Find("金星").transform.RotateAround(Vector3.zero, new Vector3(1, 1, 0), 55 * Time.deltaTime);
        GameObject.Find("地球").transform.RotateAround(Vector3.zero, new Vector3(2, 0, 0), 60 * Time.deltaTime);
        GameObject.Find("月亮").transform.RotateAround(GameObject.Find("地球").transform.position, Vector3.up, 300 * Time.deltaTime);
        GameObject.Find("火星").transform.RotateAround(Vector3.zero, new Vector3(1, 2, 0), 65 * Time.deltaTime);
        GameObject.Find("木星").transform.RotateAround(Vector3.zero, new Vector3(0.2f, 1, 0), 70 * Time.deltaTime);
        GameObject.Find("土星").transform.RotateAround(Vector3.zero, new Vector3(0.3f, 1, 0), 75 * Time.deltaTime);
        GameObject.Find("天王星").transform.RotateAround(Vector3.zero, new Vector3(0.4f, 1, 0), 85 * Time.deltaTime);
        GameObject.Find("海王星").transform.RotateAround(Vector3.zero, new Vector3(-0.7f, 1, 0), 80 * Time.deltaTime);
    }
}
