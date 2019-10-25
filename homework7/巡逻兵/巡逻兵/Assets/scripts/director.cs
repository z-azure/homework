using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class director {

	private static director _instance;

	public int gamestate;//0 for stop   1 for ready   2 for run   3 for over

    public void init()
	{
		gamestate = 0;
        
	}

    public void setbegin()
	{
		gamestate = 1;
	}

    public void setrun()
	{
		gamestate = 2;
	}

    public void setover()
	{
		gamestate = 3;
	}

    public bool isover()
    {
        if (gamestate == 3)
        {
            return true;
        }
        return false;
    }

    public int getstate()
	{
		return gamestate;
	}

    public static director getinstance()
	{
		if (_instance == null)
		{
			_instance = new director();
		}
		return _instance;
	}
}
