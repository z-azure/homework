using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AItank : tank
{
    public delegate void recycle(GameObject tank);
    public static event recycle recycle_event;
    Vector3 playerpo;
    //游戏是否结束
    private bool gameover;

    private void Start()
    {
        playerpo = GameDirector.getInstance().currentSceneController.getPlayer().transform.position;
        StartCoroutine(shoot());
    }

    void Update()
    {
        playerpo = GameDirector.getInstance().currentSceneController.getPlayer().transform.position;
        gameover = GameDirector.getInstance().currentSceneController.getGameOver();
        if (!gameover)
        {
            if (getblood() <= 0 && recycle_event != null)
            {
                recycle_event(this.gameObject);
            }
            else
            {
                // 自动向player移动
                NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
                agent.SetDestination(playerpo);
            }
        }
        else
        {
            //游戏结束，停止寻路
            NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
            agent.velocity = Vector3.zero;
            agent.ResetPath();
        }
    }

    IEnumerator shoot()
    {
        while (!gameover)
        {
            for (float i = 1; i > 0; i -= Time.deltaTime)
            {
                yield return 0;
            }
            if (Vector3.Distance(playerpo, gameObject.transform.position) < 14)
            {
                shoot(TankType.ENEMY);
            }
        }
    }
}