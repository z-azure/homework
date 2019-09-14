using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Com.Engine;

public class UserGUI : MonoBehaviour {//用户可操作的行为事件
	private UserAction action;
	private GUIStyle MyStyle;//对界面（字体等）的控制
	private GUIStyle MyButtonStyle;
	public int if_win_or_not;

    private int t = 60;
    private int now, init;
    public Text lab;
    private void Update()
    {
        /*int temptime = (int)Time.time;
        now = init - temptime;
        lab.text = ctt(now);
        tt += Time.deltaTime;
        if (tt > 1)
        {
            t--;
            GUI.Label(new Rect(10, 10, Screen.width / 8, 30), "" + t);
        }*/
    }
    string ctt(int s)
    {
        int h = s / 3600;
        int m = (s - h * 300) / 60;
        int se = s % 60;
        return string.Format("{0:D2}", se);
    }
    void Start(){
		action = Director.get_Instance ().curren as UserAction;

		MyStyle = new GUIStyle ();
		MyStyle.fontSize = 40;
		MyStyle.normal.textColor = new Color (255f, 0, 0);
		MyStyle.alignment = TextAnchor.MiddleCenter;

		MyButtonStyle = new GUIStyle ("button");
		MyButtonStyle.fontSize = 30;

        init = 60;
        now = 60;
        //t = 60;
        //ctt(now);
	}
	void reStart(){
		if (GUI.Button (new Rect (Screen.width/2-Screen.width/8, Screen.height/2+100, 150, 50), "Restart", MyButtonStyle)) {
			if_win_or_not = 0;
			action.restart ();
			moveable.cn_move = 0;
		}
        //t = 60;
	}
	void IsPause(){//创建暂停与继续键，实际上是没有必要的
		/*if (GUI.Button (new Rect (Screen.width / 2 - 350, Screen.height / 2 + 100, 150, 50), "Pause", MyButtonStyle)) {
			if (moveable.cn_move == 0) {
				action.pause ();
				moveable.cn_move = 1;
			} 
		} else if (GUI.Button (new Rect (Screen.width-Screen.width/2, Screen.height / 2 + 100, 150, 50), "Continue", MyButtonStyle)) {
			if (moveable.cn_move == 1) {
				action.Coninu();
				moveable.cn_move = 0;
			}
		}*/
	}
    private float tt = 0;
    void OnGUI(){
		IsPause ();
		reStart ();
        
		if(moveable.cn_move == 1)
			GUI.Label (new Rect (Screen.width/2-Screen.width/8, 50, 100, 50), "Pausing", MyStyle);
		if (if_win_or_not == -1) {
			GUI.Label (new Rect (Screen.width/2-Screen.width/8, 50, 100, 50), "Game Over!!!", MyStyle);
			IsPause ();
			reStart ();
		} else if (if_win_or_not == 1) {
			GUI.Label (new Rect (Screen.width/2-Screen.width/8, 50, 100, 50), "You Win!!!", MyStyle);
			IsPause ();
			reStart ();
		}
	}
}
