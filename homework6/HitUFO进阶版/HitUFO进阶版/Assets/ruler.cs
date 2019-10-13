using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ruler : MonoBehaviour {

    int round;
    int trial;
    int score;
    bool start;
    int[] numOfatrial = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    int[] scoreOfufo = { 1, 5, 100 };
    float[] speed = { 2f, 5f, 10f };
    public void init()
    {
        round = 1;
        trial = 1;
        score = 0;
        start = false;
    }

    public void setstart()
    {
        start = true;
    }

    public bool getStart()
    {
        return start;
    }

    public float getSpeed(string type_)
    {
        float s = 0f;
        if (type_ == "u1")
        {
            s = speed[0];
        }
        else if (type_ == "u2")
        {
            s = speed[1];
        }
        else
        {
            s = speed[2];
        }
        return s;
    }

    public void addscore(string type_)
    {
        if (type_ == "u1")
        {
            score += scoreOfufo[0];
        }
        else if (type_ == "u2")
        {
            score += scoreOfufo[1];
        }
        else
        {
            score += scoreOfufo[2];
        }
    }

    public int getScore()
    {
        return score;
    }

    public int getNumberOfufo()
    {
        return numOfatrial[round];
    }

    public void restart()
    {
        init();
    }

    public bool nextTrial()
    {
        trial++;
        if (trial > 10)
        {
            round++;
            trial = 1;
            return true;
        }
        return false;
    }

    public bool finish()
    {
        if (round > 10)
        {
            return true;
        }
        return false;
    }

    public int getround()
    {
        return round;
    }

    public int gettrial()
    {
        return trial;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
