using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class control : MonoBehaviour {
    GameObject[] guard = new GameObject[3];
    GameObject player;

    float delta = 5f;

    string showlabel;

    // Use this for initialization
    void Start () {
        showlabel = "score: " + rule.getinstance().getscore();

        player = Instantiate(Resources.Load<GameObject>("player"), new Vector3(2.5f, 0, -15.5f), Quaternion.identity);
        player.SetActive(false);
        init();
    }

    void init()
    {
        buwidth = 100;
        buheight = 50;
        bux = Screen.width / 2 - buwidth / 2;
        buy = Screen.height / 2 - buheight / 2;

        labelx = 50;
        labely = 10;
        labelwid = 100;
        labelhe = 50;
    }

	// Update is called once per frame
	void Update () {
		if (director.getinstance().getstate() == 1)
		{
			//Debug.Log("create");
			guard[0] = factory.getinstance().getguard(new Vector3(2.5f, 0f, 2.5f));
			guard[1] = factory.getinstance().getguard(new Vector3(-22.5f, 0f, -22.5f));
			guard[2] = factory.getinstance().getguard(new Vector3(-22.5f, 0f, 2.5f));
			director.getinstance().setrun();
            player.SetActive(true);
            
		}
        else if (director.getinstance().isover() == true)
        {
            player.SetActive(false);
            labelx = Screen.width / 2 - labelx / 2;
            labely = Screen.height / 2;
            showlabel = "you lose and the score is " + rule.getinstance().getscore();
        }
        else
        {
            showlabel = "score: " + rule.getinstance().getscore();
        }
	}

    float bux, buy, labelx, labely;
    float buwidth, buheight, labelwid, labelhe;

    private void OnGUI()
	{
		if (GUI.Button(new Rect(bux,buy,buwidth,buheight), "begin"))
		{
			director.getinstance().setbegin();
            bux = 0;
            buy = 0;
            buheight = 0;
            buwidth = 0;
			//director.getinstance().setrun();
			//Debug.Log(director.getinstance().getstate());
			
		}
		GUI.color = Color.black;
		GUI.Label(new Rect(labelx, labely, labelwid, labelhe), showlabel);
		
	}
}
