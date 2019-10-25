using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardaction : MonoBehaviour {

    float t;
    Vector3 dir;
    float[] x = new float[5];
    float[] z = new float[5];
    int order;

    float localx;//每个区域的偏移量
    float localz;

    float delta = 0.1f;

    float deltaplayer = 1f;

    float tackrange = 10f;

    private int mod;//1 for go around;  2 for chase player;     3 for attack player

    float playerx, playerz;

	// Use this for initialization
	void Start () {
        mod = 1;
        localx = this.transform.position.x;
        localz = this.transform.position.z;
        //Debug.Log(localx + " " + localz);
        t = 2f;
        //dir = new Vector3(1, 0, 0);
        order = 0;
        float a = Random.Range(2.5f, 22.5f);
        float b = Random.Range(2.5f, 22.5f);
        float c = Random.Range(2.5f, 22.5f);
        float d = Random.Range(2.5f, 22.5f);
        x[0] = 2.5f;
        z[0] = a;
        x[1] = b;
        z[1] = 22.5f;
        x[2] = 22.5f;
        z[2] = c;
        x[3] = d;
        z[3] = 2.5f;
        x[4] = 2.5f;
        z[4] = 2.5f;
        setlocalpo();
        dir = new Vector3(x[order] - localx, 0, z[order] - localz);
        //Debug.Log(x[0]+" "+z[0]);
    }

    void setlocalpo()
    {
        for(int i = 0; i < 5; i++)
        {
            x[i] += localx - 2.5f;
            z[i] += localz - 2.5f;
            //Debug.Log(x[i] + " " + z[i]);
        }
    }

    private void FixedUpdate()
    {
        if (mod == 1)
        {
            freearound();
        }
        else if (mod == 2) 
        {
            chaseplayer();
        }
        else if (mod == 3)
        {
            //Debug.Log("attack");
            //this.transform.GetComponent<Animator>().SetTrigger("Attack2");
            director.getinstance().setover();
            mod = 2;
        }
        if (outrange() == true)
        {
            //Debug.Log("outofrange");
            mod = 1;
            setdir(x[order], z[order]);
        }
    }

    void setdir(float px,float pz)
    {
        float lx = this.transform.position.x;
        float lz = this.transform.position.z;
        dir = new Vector3(px - lx, 0, pz - lz);
        this.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
    }

    bool outrange()
    {
        float lx = this.transform.position.x - localx + 2.5f;
        float lz = this.transform.position.z - localz + 2.5f;
        if (lx < 2.0f || lx > 23.0f || lz < 2.0f || lz > 23.0f)
        {
            return true;
        }
        return false;
    }

    void freearound()
    {
        runtopoint(x[order], z[order]);
    }

    void runtopoint(float px,float pz)
    {
        float lx = this.transform.position.x;
        float lz = this.transform.position.z;
        if (System.Math.Abs(lx - px) < delta && System.Math.Abs(lz - pz) < delta)
        {
            order++;
            order %= 5;
            //Debug.Log("change dir");
            dir = new Vector3(x[order] - lx, 0, z[order] - lz);
            this.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }
        else
        {
            //Debug.Log("run");
            this.transform.position += dir * Time.fixedDeltaTime * 0.5f;
        }
    }
    void runtoplayer(float px,float pz)
    {
        
        this.transform.position += dir * Time.fixedDeltaTime * 0.5f;
    }

    bool isclose(float lx,float lz)
    {
        if(System.Math.Abs(this.transform.position.x - lx) < delta && System.Math.Abs(this.transform.position.z - lz) < delta)
        {
            return true;
        }
        return false;
    }

    void chaseplayer()
    {
        //Debug.Log(System.Math.Abs(this.transform.position.x - playerx));
        if (System.Math.Abs(this.transform.position.x - playerx) < deltaplayer && System.Math.Abs(this.transform.position.z - playerz) < deltaplayer)
        {
            //Debug.Log("change mod");
            mod = 3;
        }
        else
        {
            runtoplayer(playerx, playerz);
        }
    }

    public void setmod(int m)
    {
        mod = m;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("run to player");
            mod = 2;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //Debug.Log("out of player");
            mod = 1;
            setdir(x[order], z[order]);
            rule.getinstance().addscore();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (t < 1f)
            {
                t += Time.deltaTime;
            }
            else
            {
                playerx = other.transform.position.x;
                playerz = other.transform.position.z;
                setdir(playerx, playerz);
                t = 0f;
            }
        }
    }
}
