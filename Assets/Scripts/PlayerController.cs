using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 5f;
    private bool onGround;
    public CharacterController pc;
    private GameObject mainCamGO;
    private Camera mainCamera;
    private Vector3 velocity;
    private void Awake()
    {
        GameManager.Instance.AttachPlayer();
    }
    void Start()
    {
        mainCamGO = GameObject.Find("Main Camera");
        mainCamera = mainCamGO.GetComponent<Camera>();
        pc = gameObject.GetComponent<CharacterController>();
    }
    void Update()
    {
        HandlePlayerInput();
    }
    private void HandlePlayerInput()
    {
        if (GameManager.Instance.gameState != GameState.NPCINTERACTION)
        {
            #region Player Movement
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
            if (GameManager.Instance.camMode == CameraMode.FIXED)
            {
                Vector3 camFwd = mainCamera.transform.forward;
                Vector3 camRight = mainCamera.transform.right;
                camFwd.y = camRight.y = 0f;
                camFwd = camFwd.normalized;
                camRight = camRight.normalized;
                velocity = (camFwd * z + camRight * x).normalized;
            }
            else
            {
                velocity = new Vector3(x, 0f, z).normalized;
            }
            velocity.y = Physics.gravity.y;
            if (pc.isGrounded)
            {
                velocity.y = 0f;
            }
            pc.Move(velocity * Time.deltaTime * _playerSpeed);
            if (velocity.x != 0f || velocity.z != 0f)
            {
                Vector3 facing = velocity.normalized;
                facing.y = 0f;
                gameObject.transform.forward = facing;
            }
            #endregion Player Movement
        }
    }
}