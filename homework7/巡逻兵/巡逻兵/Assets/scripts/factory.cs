using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class factory : MonoBehaviour {
    private static factory _instance;
    private List<GameObject> guard = new List<GameObject>();
    private GameObject cur_guard;

    public static factory getinstance()
    {
        if (_instance == null)
        {
            _instance = new factory();
        }
        return _instance;
    }

    public GameObject getguard(Vector3 po)
    {
        GameObject cur = Instantiate(Resources.Load<GameObject>("guard"), po, Quaternion.identity);
        return cur;
    }
	
}
