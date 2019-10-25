using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rule {

	int score;
    

	private static rule _instance;

	public static rule getinstance()
	{
		if (_instance == null)
		{
			_instance = new rule();
		}
		return _instance;
	}

	void Start()
	{
		init();
	}

    public void init()
	{
		score = 0;
	}

	public void addscore()
	{
		score++;
	}

    public int getscore()
	{
		return score;
	}
}
