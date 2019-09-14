using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Engine;

public class ClickGUI : MonoBehaviour {

	// Use this for initialization
	UserAction action;
	MyCharacterController character;

	public void setController(MyCharacterController tem){
		character = tem;
	}
	void Start(){
		action = Director.get_Instance ().curren as UserAction;
	}
	void OnMouseDown(){
		if (gameObject.name == "boat") {
			action.moveboat ();
		} else {
			action.isClickChar (character);
		}
	}
}
