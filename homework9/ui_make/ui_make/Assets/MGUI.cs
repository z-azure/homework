using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class MGUI : MonoBehaviour {

    float blood;//血量
    //public Slider bl;
    //float curb;
    float p = 150f;//偏移位置

	// Use this for initialization
	void Start () {
		
	}

    private void OnGUI()
    {

        if(GUI.Button(new Rect(p+100,100,100,100),"加血"))//创建加血按钮
        {
            if (blood < 1)
            {
                blood += 0.1f;
            }
        }
        if(GUI.Button(new Rect(p+500, 100, 100, 100), "减血"))//创建扣血按钮
        {
            if (blood > 0.1f)
            {
                blood -= 0.1f;
            }
        }
        GUI.HorizontalScrollbar(new Rect(p+300,300,200,50), 0f, blood, 0f, 1f);//滚动条表示血量
        //bl.value = blood;
    }
}
