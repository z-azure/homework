using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SceneController.cs
public class SceneController : MonoBehaviour, IUserAction
{
    public GameObject p;
    private int enemyCount = 6;
    private bool gameOver = false;
    private GameObject[] enemys;
    private factory myFactory;
    public GameDirector director;
    private void Awake()
    {
        director = GameDirector.getInstance();
        director.currentSceneController = this;
        enemys = new GameObject[enemyCount];
        gameOver = false;
        myFactory = Singleton<factory>.Instance;

    }

    void Start()
    {
        p = myFactory.getPlayer();
        for (int i = 0; i < enemyCount; i++)
        {
            enemys[i] = myFactory.getEnemys();
        }
        player.destroy_event += setGameOver;
    }

    // Update is called once per frame
    void Update()
    {
        //设置相机位置
        Camera.main.transform.position = new Vector3(p.transform.position.x, 18, p.transform.position.z);
    }

    //返回玩家坦克的位置
    public GameObject getPlayer()
    {
        return p;
    }

    //返回游戏状态
    public bool getGameOver()
    {
        return gameOver;
    }

    //设置游戏结束
    public void setGameOver()
    {
        gameOver = true;
    }

    //玩家控制坦克移动
    public void moveForward()
    {
        p.GetComponent<player>().moveup();
    }
    public void moveBackWard()
    {
        p.GetComponent<player>().movedown();
    }

    //通过水平轴上的增量，改变玩家坦克的欧拉角，从而实现坦克转向
    public void turn(float offsetX)
    {
        p.GetComponent<player>().turn(offsetX);
    }

    public void shoot()
    {
        p.GetComponent<player>().shoot(TankType.PLAYER);
    }
}

//IUserAction.cs
public interface IUserAction
{
    void moveForward();
    void moveBackWard();
    void turn(float offsetX);
    void shoot();
    bool getGameOver();
}
