using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private int[,] map = new int[3, 3];//创建棋盘
    private int turn = 0;
    private int num = 0;
    private int size = 50;

    // Start is called before the first frame update
    void Start()
    {
        num = 0;//下棋数
        turn = 1 - turn;//轮流下第一个
        for(int i=0;i<3;i++)
        {
            for(int j=0;j<3;j++)
            {
                map[i,j] = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("update\n");
    }

    private int getwin()
    {
        for(int i=0;i<3;i++)
        {
            if (map[i, 0] == map[i, 1] && map[i, 1] == map[i, 2]) return map[i, 1];
            if (map[0, i] == map[1, i] && map[1, i] == map[2, i]) return map[1, i];
        }
        if (map[1, 1] == map[0, 0] && map[1, 1] == map[2, 2]) return map[1, 1];
        if (map[1, 1] == map[0, 2] && map[1, 1] == map[2, 0]) return map[1, 1];
        if (num == 9) return 3;
        return 0;
    }

    private void OnGUI()
    {
        GUI.skin.button.fontSize = 20;
        GUI.skin.label.fontSize = 20;
        if (GUI.Button(new Rect(300, 250, 150, 50), "Reset"))
        {
            num = 0;
            turn = 1 - turn;//轮流下第一个
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    map[i, j] = 0;
                }
            }
        }

        int win = getwin();
        if(win==1) GUI.Label(new Rect(350, 50, 100, 50), "O wins");
        else if(win==2) GUI.Label(new Rect(350, 50, 100, 50), "X wins");
        else if(win==3) GUI.Label(new Rect(350, 50, 100, 50), "Draw");

        for (int i=0;i<3;i++)
        {
            for(int j=0;j<3;j++)
            {
                if(map[i,j]==1)
                {
                    GUI.Button(new Rect(i * size + 300, j * size + 100, size, size), "O");
                }
                else if(map[i,j]==2)
                {
                    GUI.Button(new Rect(i * size + 300, j * size + 100, size, size), "X");
                }
                else if(GUI.Button(new Rect(i * size + 300, j * size + 100, size, size), ""))
                {
                    if (win == 0)
                    {
                        if (turn == 0)
                        {
                            map[i, j] = 1;
                        }
                        else map[i, j] = 2;
                        turn = 1 - turn;
                        num++;
                    }
                }
            }
        }
    }

}
