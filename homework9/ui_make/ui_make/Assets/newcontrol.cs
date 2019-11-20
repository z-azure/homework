using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class newcontrol : MonoBehaviour {

	public Slider bl;
	public float blood;
	
	//float curb;
	public float p = 150f;

	// Use this for initialization
	void Start()
	{

	}

	private void OnGUI()
	{

		if (GUI.Button(new Rect(p + 100, 100, 100, 100), "加血"))
		{
			if (blood < 1)
			{
				blood += 0.1f;
			}
		}
		if (GUI.Button(new Rect(p + 500, 100, 100, 100), "减血"))
		{
			if (blood > 0.1f)
			{
				blood -= 0.1f;
			}
		}
		//GUI.HorizontalScrollbar(new Rect(p+300,300,200,50), 0f, blood, 0f, 1f);
		bl.value = blood;
	}
}
