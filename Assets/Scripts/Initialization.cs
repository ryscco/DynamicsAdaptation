using UnityEngine;
using UnityEngine.SceneManagement;
public class Initialization : MonoBehaviour
{
    GameManager gm;
    private void Awake()
    {
        gm = GameManager.Instance;
        gm.SetGameState(GameState.MAINMENU);
        gm.SetCameraMode(CameraMode.FIXED);
    }
    void Start()
    {
        gm.SetGameState(GameState.PLAY);
        SceneManager.LoadScene("Scene1");
    }
}