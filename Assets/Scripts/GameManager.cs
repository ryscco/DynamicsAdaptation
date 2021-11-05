using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState { MAINMENU, PAUSEMENU, PLAY, NPCINTERACTION }
public enum CameraMode { FIXED, MOBILE, CINEMATIC, DIALOGUE, FOLLOW_PLAYER, FOLLOW_OTHER }
public enum NPCState { IDLE, PLAYERINTERACT, MOVINGTONODE }
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
        return ((t1.position - t2.position).magnitude <= d);
    }
    //public bool FacingCheck(Transform t1, Transform t2)
    //{
    //    Quaternion q1 = t1.localToWorldMatrix.rotation;
    //    Quaternion q2 = t2.localToWorldMatrix.rotation;
    //    float q3 = Quaternion.Angle(q1, q2);
    //    Debug.Log("Angle between: " + q3);
    //    return (Vector3.Dot(t1.forward, t2.forward) > 0f);
    //    return NiftyMath.AngleInRange360(t1, t2, Vector3.up, 10f, 170f);
    //}
    public void OnApplicationQuit()
    {
        GameManager._instance = null;
    }
}