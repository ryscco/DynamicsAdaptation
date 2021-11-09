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
    public Faction[] allFactions;
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
    public void SetGameState(GameState s) => gameState = s;
    public void SetCameraMode(CameraMode c) => camMode = c;
    public void AttachPlayer()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }
    public bool ProximityCheck(Transform t1, Transform t2, float d) => ((t1.position - t2.position).magnitude <= d);
    public Vector3 CameraPosition() => Camera.main.transform.localPosition;
    public void PopulateFactions()
    {
        allFactions = GameObject.Find("RelationshipManager").GetComponent<RelationshipManager>().GetAllFactions();
    }
    public void DisplayRelationships()
    {
        GameObject.Find("RelationshipManager").GetComponent<RelationshipManager>().SetBaselineRelationships();
        GameObject.Find("RelationshipManager").GetComponent<RelationshipManager>().DisplayRelationships();
    }
    public void OnApplicationQuit()
    {
        GameManager._instance = null;
    }
}