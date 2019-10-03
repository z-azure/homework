using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class control : MonoBehaviour {

    private factory fac;
    private ruler r = new ruler();
    private List<GameObject> flyufo = new List<GameObject>();
    private List<Vector3> dir = new List<Vector3>();

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

    float interval = 0;
    int shownum = 1;
    float ti = 0;
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
        Debug.Log(r.finish());
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
            interval += Time.deltaTime;
            if (interval > 2f)
            {
                r.nextTrial();
                //int left = number - flyufo.Count;
                int left = r.getNumberOfufo();
                //Debug.Log(fac.use.Count);
                for (int i = 0; i < left; i++)
                {
                    //display.Add(fac.getufo(1));
                    flyufo.Add(fac.getufo(r.getround()));
                    float x = Random.Range(-2f, 2f);
                    float y = Random.Range(0f, 1f);
                    float z = Random.Range(1f, 2f);
                    //Debug.Log("" + x);
                    dir.Add(new Vector3(x, y, z));
                }
                interval = 0;
            }
            //Debug.Log("out have " + fac.use.Count);
            ti += Time.deltaTime;
            for (int i = 0; i < flyufo.Count; i++)
            {
                if (i <= shownum)
                {
                    move(flyufo[i], dir[i]);
                }
                if (ti > 2f)
                {
                    shownum++;
                    ti = 0;
                }
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

    public void move(GameObject ufo_,Vector3 d)
    {
        //Debug.Log("move");
        //Vector3 dir = new Vector3(1, 0, 0);
        ufo_.transform.position += d * Time.deltaTime * r.getSpeed(ufo_.gameObject.tag);
    }
}