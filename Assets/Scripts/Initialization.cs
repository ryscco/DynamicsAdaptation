using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour
{
    GameManager gm;
    TimeManager tm;
    private void Awake()
    {
        gm = GameManager.Instance;
        gm.SetGameState(GameState.MAINMENU);
        gm.SetCameraMode(CameraMode.FIXED);
        tm = TimeManager.Instance;
    }
    void Start()
    {
        gm.SetGameState(GameState.PLAY);
        SceneManager.LoadScene("Scene1");
    }
}
