using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class factory : MonoBehaviour {

    public List<ufo> use = new List<ufo>();
    public List<ufo> rest = new List<ufo>();
    private adapter a = new adapter();

    Vector3 bottom = new Vector3(-0.12f, -1.99f, -5.67f);
    int num = 2;
    int start = 7;
    int type1 = 3;
    int type2 = 5;
    int type3 = 7;

    int type = 0;

    public GameObject getufo(int round)
    {
        ufo s = new ufo();
        GameObject ufo_ = null;
        string type_ = getufotype(round);

        for(int i = 0; i < rest.Count; i++)
        {
            if (rest[i]._ufo.gameObject.tag == type_)
            {
                ufo_ = rest[i]._ufo;
                rest.Remove(rest[i]);
                ufo_.gameObject.SetActive(true);
                break;
            }
        }

        if (ufo_ == null)
        {
            if (type_ == "u1")
            {
                ufo_ = Instantiate(Resources.Load<GameObject>("ufo_u1"), bottom, Quaternion.identity);
            }
            else if (type_ == "u2")
            {
                ufo_ = Instantiate(Resources.Load<GameObject>("ufo_u2"), bottom, Quaternion.identity);
            }
            else
            {
                ufo_ = Instantiate(Resources.Load<GameObject>("ufo_u3"), bottom, Quaternion.identity);
            }
            //ufo_.GetComponent<ufo>().direction = new Vector3(1, 1, 0);
            
            ufo_.tag = type_;
            if (type == 0)
            {
                float x = Random.Range(-2f, 2f);
                float y = Random.Range(0f, 1f);
                float z = Random.Range(1f, 2f);
                //int r = Random.Range(0, 1);
                //s.direction = new Vector3(0, r, 0);
                //a.setGrativity(ufo_, new Vector3(x, y, z));
            }
            s._ufo = ufo_;
        }
        ufo_.transform.position = bottom;
        s._ufo = ufo_;
        use.Add(s);
        //Debug.Log("in have " + use.Count);
        return ufo_;
    }

    public void free(GameObject ufo_)
    {
        for(int i = 0; i < use.Count; i++)
        {
            if (ufo_.GetInstanceID() == use[i]._ufo.gameObject.GetInstanceID())
            {
                use[i]._ufo.gameObject.SetActive(false);
                //Destroy(use[i]._ufo.gameObject.GetComponent<Rigidbody>());
                rest.Add(use[i]);
                use.Remove(use[i]);
                break;
            }
        }
    }

    public int getcount()
    {
        return use.Count;
    }

    string getufotype(int round)
    {
        int type_;
        string t;
        if (round <= 1)
        {
            type_ = Random.Range(0, type1);
        }
        else if (round <= 5)
        {
            type_ = Random.Range(0, type2);
        }
        else
        {
            type_ = Random.Range(0, type3);
        }
        if (type_ <= type1)
        {
            t = "u1";
        }
        else if (type_ <= type2)
        {
            t = "u2";
        }
        else
        {
            t = "u3";
        }
        return t;
    }

	// Use this for initialization
	void Start () {
        //getufo(1);
        type = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
