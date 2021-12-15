using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _defaultPlayerSpeed = 5f;
    private float _currentPlayerSpeed;
    [SerializeField] private float _playerSpeedModifier = 1.5f;
    [SerializeField] private Animator _anim;
    [SerializeField] private NavMeshAgent _agent;
    private bool onGround;
    public CharacterController pc;
    private GameObject _mainCamGO;
    private Camera _mainCam;
    private Vector3 velocity, startPosition;
    public float timeManagerIncrement;
    static public Inventory _inventory;
    [SerializeField] private UI_Inventory _uiInventory;
    private Transform _overShoulderView;
    private Vector3 _storedCamViewPos;
    private Quaternion _storedCamViewRot;
    private void Awake()
    {
        GameManager.Instance.AttachPlayer();
        _inventory = new Inventory();
    }
    void Start()
    {
        _mainCamGO = GameObject.Find("Main Camera");
        _mainCam = _mainCamGO.GetComponent<Camera>();
        pc = gameObject.GetComponent<CharacterController>();
        timeManagerIncrement = TimeManager.Instance.Increment;
        startPosition = transform.localPosition;
        _uiInventory.SetInventory(_inventory);
        _overShoulderView = transform.Find("PlayerOverShoulderCamView");
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
            if (!_agent.enabled) _agent.enabled = true;
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
                Vector3 camFwd = _mainCam.transform.forward;
                Vector3 camRight = _mainCam.transform.right;
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
                _anim.SetBool("isWalking", true);
            }
            else
            {
                _anim.SetBool("isWalking", false);
            }
            if (transform.position.y < 1f)
            {
                transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
            }
        }
        #endregion Player Movement
        if (GameManager.Instance.gameState == GameState.NPCINTERACTION)
        {
            if (_agent.enabled) _agent.enabled = false;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.gameState = GameState.PLAY;
            }
        }
    }
    internal void EnableOverShoulderView()
    {
        _storedCamViewPos = _mainCamGO.transform.position;
        _storedCamViewRot = _mainCamGO.transform.rotation;
        GameManager.Instance.SetCameraTransform(_overShoulderView);
    }
    internal void DisableOverShoulderView()
    {
        GameManager.Instance.SetCameraTransform(_storedCamViewPos, _storedCamViewRot);
        _storedCamViewPos = Vector3.zero;
        _storedCamViewRot = Quaternion.identity;
    }
}