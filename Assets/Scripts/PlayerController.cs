using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _defaultPlayerSpeed = 5f;
    private float _currentPlayerSpeed;
    [SerializeField] private float _playerSpeedModifier = 1.5f;
    private bool onGround;
    public CharacterController pc;
    private GameObject mainCamGO;
    private Camera mainCamera;
    private Vector3 velocity, startPosition;
    public float timeManagerIncrement;
    private void Awake()
    {
        GameManager.Instance.AttachPlayer();
    }
    void Start()
    {
        mainCamGO = GameObject.Find("Main Camera");
        mainCamera = mainCamGO.GetComponent<Camera>();
        pc = gameObject.GetComponent<CharacterController>();
        timeManagerIncrement = TimeManager.Increment;
        startPosition = transform.localPosition;
    }
    void Update()
    {
        HandlePlayerInput();
        TimeManager.Instance.SetIncrement(timeManagerIncrement);
    }
    private void HandlePlayerInput()
    {
        if (GameManager.Instance.gameState != GameState.NPCINTERACTION)
        {
            #region Player Movement
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _currentPlayerSpeed = _defaultPlayerSpeed * _playerSpeedModifier;
            }
            else
            {
                _currentPlayerSpeed = _defaultPlayerSpeed;
            }

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
            pc.Move(velocity * Time.deltaTime * _currentPlayerSpeed);
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