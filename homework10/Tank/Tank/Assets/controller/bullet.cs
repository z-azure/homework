using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {
    public float explosionRadius = 3.0f;
    private TankType tankType;

    public void setTankType(TankType type)
    {
        tankType = type;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "AItank" && this.tankType == TankType.ENEMY ||
            collision.transform.gameObject.tag == "Player" && this.tankType == TankType.PLAYER)
        {
            return;
        }
        factory f = Singleton<factory>.Instance;
        ParticleSystem explosion = f.getParticleSystem();
        explosion.transform.position = gameObject.transform.position;
        //获取爆炸范围内的所有碰撞体
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, explosionRadius);

        foreach (var collider in colliders)
        {
            //被击中坦克与爆炸中心的距离
            float distance = Vector3.Distance(collider.transform.position, gameObject.transform.position);
            float hurt;
            // 如果是玩家发出的子弹伤害高一点
            if (collider.tag == "AItank" && this.tankType == TankType.PLAYER)
            {
                hurt = 300.0f / distance;
                collider.GetComponent<tank>().setblood(collider.GetComponent<tank>().getblood() - hurt);
            }
            else if (collider.tag == "Player" && this.tankType == TankType.ENEMY)
            {
                hurt = 100.0f / distance;
                collider.GetComponent<tank>().setblood(collider.GetComponent<tank>().getblood() - hurt);
            }
            explosion.Play();
        }

        if (gameObject.activeSelf)
        {
            f.recycleBullet(gameObject);
        }
    }
}
