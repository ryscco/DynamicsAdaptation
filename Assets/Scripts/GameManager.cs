using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MAINMENU, PAUSEMENU, PLAY }
public enum CameraMode { FIXED, MOBILE, CINEMATIC, DIALOGUE, FOLLOW_PLAYER, FOLLOW_OTHER }
public enum Faction { REDTEAM, BLUETEAM, GREENTEAM }

public class GameManager
{
    protected GameManager() { }
    private static GameManager _instance = null;
    public GameState gameState;
    public CameraMode camMode;
    public GameObject player;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameManager._instance = new GameManager();
            }
            return GameManager._instance;
        }
    }
    public void SetGameState(GameState s)
    {
        gameState = s;
    }
    public void SetCameraMode(CameraMode c)
    {
        camMode = c;
    }
    public void AttachPlayer()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }
    public bool ProximityCheck(Transform t1, Transform t2, float d)
    {
        return ((t1.localPosition - t2.localPosition).magnitude <= d);
    }
    public void OnApplicationQuit()
    {
        GameManager._instance = null;
    }
}