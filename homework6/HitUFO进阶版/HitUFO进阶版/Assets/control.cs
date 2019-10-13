using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class control : MonoBehaviour {

    private factory fac;
    private ruler r = new ruler();
    private List<GameObject> flyufo = new List<GameObject>();
    private List<Vector3> dir = new List<Vector3>();
    private adapter ad = new adapter();

    public bool mod = true;//为true时表示物理学运动，false为运动学运动

    private TextMesh textmesh;

    //private List<ufo> flyufo = new List<ufo>();
    int number = 10;
    //private action move;

    private void Awake()
    {
        this.textmesh = this.GetComponentInParent<TextMesh>();
    }

    // Use this for initialization
    void Start () {
        fac = Singleton<factory>.Instance;
        cam = Camera.main;
        r.init();
        this.textmesh.text = "";
        textmesh.transform.position = new Vector3(-8, 5, 0);

    }

    float interval = 2f;
    float trial_t = 0;
    int shownum = 1;
    float ti = 0;
    int create = 0;
    int numofround = 0;
    int numofnow = 0;

    public Camera cam;

    public void restart()
    {
        r.restart();
        //textmesh.text = "round " + r.getround() + " score: " + r.getScore();
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(5, 5, 60, 40), "start"))
        {
            if (r.getStart() == false)
            {
                r.setstart();
            }
        }
        /*if (GUI.Button(new Rect(70, 5, 60, 40), "restart"))
        {
            restart();
            while (flyufo.Count > 0)
            {
                remove(flyufo[0]);
            }
        }*/
    }

    // Update is called once per frame
    void Update () {
        
        //Debug.Log(r.finish());
        if (r.finish() == true)
        {
            textmesh.text = "finish and your score is: " + r.getScore();
            restart();
            while (flyufo.Count > 0)
            {
                remove(flyufo[0]);
            }
        }
        if (r.getStart() == true && r.finish() == false)
        {
            textmesh.text = "round " + r.getround() + " score: " + r.getScore();
            if (create == 0)
            {
                interval += Time.deltaTime;
            }
            if (interval > 2f)
            {
                create = 1;
                numofround = r.getNumberOfufo();
                //Debug.Log("get " + numofround + " and set create: " + create);
                interval = 0;
            }
            //Debug.Log("create: " + create);
            if (create == 1)
            {
                trial_t += Time.deltaTime;
                //Debug.Log(trial_t);
                if (trial_t > 0.2f)
                {
                    if (numofnow == numofround)
                    {
                        trial_t = 0;
                        create = 0;
                        r.nextTrial();
                        numofnow = 0;
                    }
                    else
                    {
                        float x = Random.Range(-2f, 2f);
                        float y = Random.Range(0f, 1f);
                        float z = Random.Range(1f, 2f);
                        //Debug.Log("" + x + "," + y + "," + z);
                        dir.Add(new Vector3(x, y, z));
                        flyufo.Add(fac.getufo(r.getround()));
                        numofnow++;
                        trial_t = 0;
                        //Debug.Log("create");
                    }
                }
            }
            //Debug.Log("out have " + fac.use.Count);
            if (mod == false)
            {
                //ti += Time.deltaTime;
                //if (ti > 2f)
                //{
                    for (int i = 0; i < flyufo.Count; i++)
                    {
                        move2(flyufo[i], dir[i]);
                        
                    }
                    ti = 0;
                //}
            }
            else
            {
                move();
            }
            if (Input.GetButtonDown("Fire1"))
            {
                Vector3 mousePosition = Input.mousePosition;
                Ray ray = cam.ScreenPointToRay(mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    r.addscore(hit.transform.gameObject.tag);
                    remove(hit.transform.gameObject);
                }
            }
            //move();
            outscreen();
            
            //Debug.Log("now round " + r.getround() + " trial " + r.gettrial() + " score " + r.getScore());
        }
    }

    public void remove(GameObject u)
    {
        fac.free(u);
        int num = 0;
        for (int i = 0; i < flyufo.Count; i++)
        {
            if (flyufo[i].GetInstanceID() == u.GetInstanceID())
            {
                num = i;
                break;
            }
        }
        //Debug.Log(flyufo[num].gameObject.GetComponent<Rigidbody>());
        if (mod == true)
        {
            //u.AddComponent<Rigidbody>();
            //u.GetComponent<Rigidbody>().AddForce(-dir[num]);
            //Destroy(u.GetComponent<Rigidbody>());
            ad.removeGrativity(u);
        }
        //flyufo[num].gameObject.GetComponent<Rigidbody>().AddForce(-dir[num]);
        flyufo.Remove(u);
        dir.Remove(dir[num]);
    }

    public void outscreen()
    {
        for(int i = 0; i < flyufo.Count; i++)
        {
            if (flyufo[i].transform.position.z > 20)
            {
                remove(flyufo[i]);
            }
        }
        
    }

    public void move()
    {
        for(int i = 0; i < flyufo.Count; i++)
        {
            //ad.setGrativity(flyufo[i], dir[i]);
            //flyufo[i].transform.position += Time.deltaTime * dir[i];
            if (mod == true)
            {
                //flyufo[i].gameObject.AddComponent<Rigidbody>();
                //flyufo[i].gameObject.GetComponent<Rigidbody>().AddForce(dir[i]);
                //flyufo[i].gameObject.GetComponent<Rigidbody>().useGravity = false;
                ad.setGrativity(flyufo[i], dir[i]);
            }
        }
        //Debug.Log("move");
        //Vector3 dir = new Vector3(1, 0, 0);
        //ufo_.transform.position += d * Time.deltaTime * r.getSpeed(ufo_.gameObject.tag);
    }
    public void move2(GameObject ufo_,Vector3 d)
    {
        //ufo_.AddComponent<Rigidbody>();
        //Destroy(ufo_.GetComponent<Rigidbody>());
        ufo_.transform.position += d * Time.deltaTime * r.getSpeed(ufo_.gameObject.tag);
    }
}