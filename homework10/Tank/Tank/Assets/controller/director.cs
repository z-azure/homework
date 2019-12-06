using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameDirector.cs
public class GameDirector : System.Object
{
    private static GameDirector _instance;
    public SceneController currentSceneController { get; set; }

    private GameDirector() { }
    public static GameDirector getInstance()
    {
        if (_instance == null)
        {
            _instance = new GameDirector();
        }
        return _instance;
    }
}
