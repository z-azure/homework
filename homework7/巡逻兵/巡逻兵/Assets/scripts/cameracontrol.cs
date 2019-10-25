using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//相机一直拍摄主角的后背
public class cameracontrol : MonoBehaviour
{
    public Transform target=null;
    public float distanceUp = 5f;
    public float distanceAway = 2f;
    public float smooth = 60f;//位置平滑移动值
    public float camDepthSmooth = 5f;

    //public bool begin = false;

    bool setalready;
    // Use this for initialization
    void Start()
    {
        setalready = false;
        //target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (director.getinstance().getstate()==2 && setalready == false)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            setalready = true;
        }
        // 鼠标轴控制相机的远近
        if ((Input.mouseScrollDelta.y < 0 && Camera.main.fieldOfView >= 3) || Input.mouseScrollDelta.y > 0 && Camera.main.fieldOfView <= 80)
            {
                Camera.main.fieldOfView += Input.mouseScrollDelta.y * camDepthSmooth * Time.deltaTime;
            }
        //}
    }

    void LateUpdate()
    {
        if (director.getinstance().getstate() == 2 && setalready == false)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            setalready = true;
        }
        //相机的位置
        Vector3 disPos = target.position + Vector3.up * distanceUp - target.forward * distanceAway;
        transform.position = Vector3.Lerp(transform.position, disPos, Time.deltaTime * smooth);
        //相机的角度
        transform.LookAt(target.position);
    }
}
