using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.Engine;

public class MySceneController : MonoBehaviour, SceneController, UserAction{//对界面的控制
	readonly Vector3 water_pos = new Vector3 (0, 0.5f, 0);//初始化水的位置
	UserGUI user;//定义用户操作
    //在这个类里面需要放上需要的所有游戏对象，这样才能集中在一起管理
	public CoastController fromCoast;
	public CoastController toCoast;
	public BoatController boat;
	//private MyCharacterController[] characters;
	private List<MyCharacterController> team;
    void Update()
    {
        //Debug.Log("asd");
    }
	void Awake(){
		Director director = Director.get_Instance ();//实例化导演
		director.curren = this;
		user = gameObject.AddComponent<UserGUI> () as UserGUI;//添加GUI控制
		//characters = new MyCharacterController[6];
		team = new List<MyCharacterController>();
		loadResources ();
	}
	public void loadResources(){//资源的初始化加载
		GameObject water = Instantiate (Resources.Load ("Prefabs/water", typeof(GameObject)), water_pos, Quaternion.identity, null) as GameObject;
		water.name = "water";

		fromCoast = new CoastController ("from");
		toCoast = new CoastController ("to");
		boat = new BoatController ();

		for (int i = 0; i < 3; i++) {//对人物的加载
			MyCharacterController tem = new MyCharacterController ("priest");
			tem.setName ("priest" + i);
			tem.setPosition (fromCoast.getEmptyPosition ());
			tem.getOnCoast (fromCoast);
			fromCoast.getOnCoast (tem);
			team.Add (tem);
		}
		for (int i = 0; i < 3; i++) {
			MyCharacterController tem = new MyCharacterController ("devil");
			tem.setName ("devil" + i);
			tem.setPosition (fromCoast.getEmptyPosition ());
			tem.getOnCoast (fromCoast);
			fromCoast.getOnCoast (tem);
			team.Add (tem);
		}
	}
	public void moveboat(){
		if (boat.IfEmpty ())
			return;
		boat.boatMove ();
		//check whether game over
		user.if_win_or_not = checkGameOver();
	}
	public void isClickChar (MyCharacterController tem_char){
		if (moveable.cn_move == 1)
			return;
		if (tem_char._isOnBoat ()) {
			CoastController tem_coast;
			if (boat.getTFflag () == -1) {
				tem_coast = toCoast;
			} else {
				tem_coast = fromCoast;
			}
			boat.getOffBoat (tem_char.getName ());
			tem_char.moveToPosition (tem_coast.getEmptyPosition ());
			tem_char.getOnCoast (tem_coast);
			tem_coast.getOnCoast (tem_char);
		} else {
			CoastController tem_coast2 = tem_char.getCoastController ();
			if (boat.getEmptyIndex () == -1)
				return;
			if (boat.getTFflag () != tem_coast2.getTFflag ())
				return;
			tem_coast2.getOffCoast (tem_char.getName());
			tem_char.moveToPosition (boat.getEmptyPosition ());
			tem_char.getOnBoat (boat);
			boat.getOnBoat (tem_char);
		}
		//check whether game over;
		user.if_win_or_not = checkGameOver();
	}
	public void restart(){
		boat.reset ();
		fromCoast.reset ();
		toCoast.reset ();
		foreach (MyCharacterController i in team) {
			i.reset ();
		}
		moveable.cn_move = 0;
	}
	public void pause(){
		boat.Mypause ();
		foreach (MyCharacterController i in team) {
			i.Mypause();
		}
	}
	public void Coninu (){
		boat.MyConti ();
		foreach (MyCharacterController i in team) {
			i.MyConti();
		}
	}
	private int checkGameOver(){
		if (moveable.cn_move == 1)
			return 0;
		int from_priest = 0;
		int from_devil = 0;
		int to_priest = 0;
		int to_devil = 0;

		int[] from_count = fromCoast.getCharacterNum ();
		from_priest = from_count [0];
		from_devil = from_count [1];

		int[] to_count = toCoast.getCharacterNum ();
		to_priest = to_count [0];
		to_devil = to_count [1];

		if (to_devil + to_priest == 6)
			return 1;//you win
		int[] boat_count = boat.getCharacterNum();
		if (boat.getTFflag () == 1) {
			from_priest += boat_count [0];
			from_devil += boat_count [1];
		} else {
			to_priest += boat_count [0];
			to_devil += boat_count [1];
		}
		if (from_priest < from_devil && from_priest > 0)
			return -1;//you lose
		if(to_priest < to_devil && to_priest > 0)
			return -1;//you lose
		return 0;//not yet finish
	}

}
