using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Engine
{
	public class Director: System.Object
	{
		private static Director _instance;//实例化唯一的一个导演来控制
		public SceneController curren{ get; set;}
		public static Director get_Instance(){
			if (_instance == null)
			{
				_instance = new Director();
			}
			return _instance;
		}
	}
	public interface SceneController
	{
		void loadResources();
	}
	public interface UserAction{//用户行为接口
		void moveboat();
		void isClickChar (MyCharacterController tem_char);
		void restart();
		void pause();
		void Coninu ();
	}
	//------------moveable---------------------------
	public class moveable: MonoBehaviour//对移动这一事件的控制
	{
		readonly float move_speed = 20;
		private int move_to_where;//0->not move, 1->to middle, 2->to destination
		private Vector3 dest;
		private Vector3 middle;
		public static int cn_move = 0;//0->can move, 1->cant move

		void Update(){
			if (cn_move == 1)
				return;
			else{
				if(move_to_where == 1){
					transform.position = Vector3.MoveTowards(transform.position, middle, move_speed*Time.deltaTime);
					if (transform.position == middle)
						move_to_where = 2;
				}
				else if(move_to_where == 2){
					transform.position = Vector3.MoveTowards(transform.position, dest, move_speed*Time.deltaTime);
					if (transform.position == dest)
						move_to_where = 0;
				}
			}
		}

		public void SetDestination(Vector3 _dest){
			if (cn_move == 1)
				return;
			else{
				middle = _dest;
				dest = _dest;
				if (_dest.y < transform.position.y) {
					middle.y = transform.position.y;
				} else {
					middle.x = transform.position.x;
				}
				move_to_where = 1;
			}
		}
		public void reset(){
			if (cn_move == 1)
				return;
			else{
				move_to_where = 0;
			}
		}
	}
	//-----------CoastController---------------------
	public class CoastController{//对河岸的控制
		readonly GameObject coast;
		readonly Vector3 from_pos = new Vector3(9,1,0);//右岸的位置
		readonly Vector3 to_pos = new Vector3(-9,1,0);//左岸的位置
		readonly Vector3[] postion;//岸上的人物站的位置
		readonly int TFflag;//-1->to, 1->from

		private MyCharacterController[] passengerPlaner;//河岸上的魔鬼与牧师

		public CoastController(string to_or_from){
			postion = new Vector3[] {//初始化人物可以站的位置
				new Vector3 (6.5f, 2.25f, 0),
				new Vector3 (7.5f, 2.25f, 0),
				new Vector3 (8.5f, 2.25f, 0),
				new Vector3 (9.5f, 2.25f, 0),
				new Vector3 (10.5f, 2.25f, 0),
				new Vector3 (11.5f, 2.25f, 0)
			};
			passengerPlaner = new MyCharacterController[6];//一个河岸最多6个人物
			if(to_or_from == "from"){//创建河岸
				coast = Object.Instantiate(Resources.Load("Prefabs/Mycoast", typeof(GameObject)), from_pos, Quaternion.identity, null) as GameObject;
				coast.name = "from";
				TFflag = 1;
			}
			else{
				coast = Object.Instantiate(Resources.Load("Prefabs/Mycoast", typeof(GameObject)), to_pos, Quaternion.identity, null) as GameObject;
				coast.name = "to";
				TFflag = -1;
			}
		}
		public int getTFflag(){
			return TFflag;
		}
		public MyCharacterController getOffCoast(string object_name){
			for(int i=0; i<passengerPlaner.Length; i++){
				if(passengerPlaner[i] != null && passengerPlaner[i].getName() == object_name){
					MyCharacterController myCharacter = passengerPlaner[i];
					passengerPlaner[i] = null;
					return myCharacter;
				}
			}
			return null;
		}
		public int getEmptyIndex(){
			for(int i=0; i<passengerPlaner.Length; i++){
				if(passengerPlaner[i] == null){
					return i;
				}
			}
			return -1;
		}
		public Vector3 getEmptyPosition(){
			int index = getEmptyIndex();
			Vector3 pos = postion[index];
			pos.x *= TFflag;
			return pos;
		}
		public void getOnCoast(MyCharacterController myCharacter){
			int index = getEmptyIndex();
			passengerPlaner[index] = myCharacter;
		}
		public int[] getCharacterNum(){
			int[] count = {0,0};
			for(int i=0; i<passengerPlaner.Length; i++){
				if(passengerPlaner[i] == null) continue;
				if(passengerPlaner[i].getType() == 0) count[0]++;
				else count[1]++;
			}
			return count;
		}
		public void reset(){
			passengerPlaner = new MyCharacterController[6];
		}
	}
	//-----------CharacterController-----------------
	public class MyCharacterController{//对人物的定义，包括魔鬼与牧师
		readonly GameObject character;
		readonly moveable Cmove;
		readonly ClickGUI clickgui;
		readonly int Ctype;//0->priset, 1->devil
		private bool isOnboat;
		private CoastController coastcontroller;

		public MyCharacterController(string Myname){
			if(Myname == "priest"){
				character = Object.Instantiate(Resources.Load("Prefabs/Priest", typeof(GameObject)), Vector3.zero, Quaternion.identity,null) as GameObject;
				Ctype = 0;
			}
			else{
				character = Object.Instantiate(Resources.Load("Prefabs/Devil", typeof(GameObject)), Vector3.zero, Quaternion.identity,null) as GameObject;
				Ctype = 1;
			}
			Cmove = character.AddComponent(typeof(moveable)) as moveable;
			clickgui = character.AddComponent(typeof(ClickGUI)) as ClickGUI;
			clickgui.setController(this);
		}
		public int getType(){
			return Ctype;
		}
		public void setName(string name){
			character.name = name;
		}
		public string getName(){
			return character.name;
		}
		public void setPosition(Vector3 postion){
			character.transform.position = postion;
		}
		public void moveToPosition(Vector3 _dest){
			Cmove.SetDestination (_dest);
		}
		public void getOnBoat(BoatController tem_boat){
			coastcontroller = null;
			character.transform.parent = tem_boat.getGameObject ().transform;
			isOnboat = true;
		}
		public void getOnCoast(CoastController coastCon){
			coastcontroller = coastCon;
			character.transform.parent = null;
			isOnboat = false;
		}
		public bool _isOnBoat(){
			return isOnboat;
		}
		public CoastController getCoastController(){
			return coastcontroller;
		}
		public void reset(){
			Cmove.reset ();
			coastcontroller = (Director.get_Instance ().curren as MySceneController).fromCoast;
			getOnCoast(coastcontroller);
			setPosition (coastcontroller.getEmptyPosition ());
			coastcontroller.getOnCoast (this);
		}
		public void Mypause(){
			moveable.cn_move = 1;
		}
		public void MyConti(){
			moveable.cn_move = 0;
		}
	}
	//------------------------------BoatController--------------------------------------
	public class BoatController{//对船的行为等的定义
		readonly GameObject boat;
		readonly moveable Cmove;
		readonly Vector3 fromPos = new Vector3 (5, 1, 0);//船靠岸时在左岸与右岸的位置
		readonly Vector3 toPos = new Vector3 (-5, 1, 0);
		readonly Vector3[] from_pos;//船上的位置
		readonly Vector3[] to_pos;
		private int TFflag;//-1->to, 1->from
		private MyCharacterController[] passenger = new MyCharacterController[2];//船上同时最多2人

		public BoatController(){
			TFflag = 1;
			from_pos = new Vector3[]{ new Vector3 (4.5f, 1.5f, 0), new Vector3 (5.5f, 1.5f, 0) };//初始化船在两岸时人物可站的位置
			to_pos = new Vector3[]{ new Vector3 (-5.5f, 1.5f, 0), new Vector3 (-4.5f, 1.5f, 0) };
			boat = Object.Instantiate (Resources.Load ("Prefabs/Boat", typeof(GameObject)), fromPos, Quaternion.identity, null) as GameObject;
			boat.name = "boat";
			Cmove = boat.AddComponent (typeof(moveable)) as moveable;
			boat.AddComponent (typeof(ClickGUI));
		}
		public void boatMove(){
			if (TFflag == 1) {
				Cmove.SetDestination (toPos);
				TFflag = -1;
			} else {
				Cmove.SetDestination (fromPos);
				TFflag = 1;
			}
		}
		public void getOnBoat(MyCharacterController tem_cha){
			int index = getEmptyIndex ();
			passenger [index] = tem_cha;
		}
		public MyCharacterController getOffBoat(string object_name){
			for (int i = 0; i < passenger.Length; i++) {
				if (passenger [i] != null && passenger [i].getName () == object_name) {
					MyCharacterController temp_character = passenger [i];
					passenger [i] = null;
					return temp_character;
				}
			}
			return null;
		}
		public int getEmptyIndex(){
			for (int i = 0; i < passenger.Length; i++) {
				if (passenger [i] == null)
					return i;
			}
			return -1;
		}
		public bool IfEmpty(){
			for (int i = 0; i < passenger.Length; i++) {
				if (passenger [i] != null)
					return false;
			}
			return true;
		}
		public Vector3 getEmptyPosition(){
			Vector3 pos;
			int index = getEmptyIndex ();
			if (TFflag == 1) {
				pos = from_pos [index];
			} else {
				pos = to_pos [index];
			}
			return pos;
		}
		public GameObject getGameObject(){
			return boat;
		}
		public int getTFflag(){
			return TFflag;
		}
		public int[] getCharacterNum(){
			int[] count = { 0, 0 };
			for (int i = 0; i < passenger.Length; i++) {
				if (passenger [i] == null)
					continue;
				if (passenger [i].getType () == 0) {
					count [0]++;
				} else {
					count [1]++;
				}
			}
			return count;
		}
		public void reset(){
			Cmove.reset ();
			if (TFflag == -1) {
				boatMove ();
			}
			passenger = new MyCharacterController[2];
		}
		public void Mypause(){
			moveable.cn_move = 1;
		}
		public void MyConti(){
			moveable.cn_move = 0;
		}
	}
}
