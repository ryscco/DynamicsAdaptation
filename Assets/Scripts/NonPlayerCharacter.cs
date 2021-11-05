using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
public class NonPlayerCharacter : Interactable
{
    private GameManager gameManager;
    public NPC npc;
    public Queue<Vector4> schedule;
    public NPCState npcState = NPCState.IDLE;
    private NPCState _previousState;
    [SerializeField] private TextMeshPro _textName;
    private NavMeshAgent _agent;
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    void Start()
    {
        _textName.text = npc.npcName;
        _agent = this.GetComponent<NavMeshAgent>();
        schedule = npc.GetSchedule();
        npc.ShowSchedule(schedule);
        advanceSchedule();
    }
    void Update()
    {
        Debug.Log("Current Hour: " + TimeManager.TimeHour);
        if (_agent.velocity.magnitude >= 0.05f)
        {
            npcState = NPCState.MOVINGTONODE;
        }
        if (_agent.remainingDistance >= 0f && npcState == NPCState.MOVINGTONODE)
        {
            npcState = NPCState.IDLE;
        }
        if (this.isInteractable() /*&& this.isFacingAndNearby()*/)
        {
            showNameplate();
            if (npcState != NPCState.PLAYERINTERACT && Input.GetKeyDown(KeyCode.Space))
            {
                beginPlayerInteraction();
            }
        }
        else hideNameplate();
        if (npcState == NPCState.PLAYERINTERACT)
        {
            playerInteraction();
        }
        handleSchedule();
    }
    override public void showNameplate()
    {
        if (!_textName.gameObject.activeSelf)
        {
            _textName.gameObject.SetActive(true);
        }
    }
    public override void hideNameplate()
    {
        if (_textName.gameObject.activeSelf)
        {
            _textName.gameObject.SetActive(false);
        }
    }
    protected override void beginPlayerInteraction()
    {
        _previousState = npcState;
        npcState = NPCState.PLAYERINTERACT;
        gameManager.gameState = GameState.NPCINTERACTION;
    }
    protected override void exitPlayerInteraction()
    {
        npcState = _previousState;
        gameManager.gameState = GameState.PLAY;
    }
    protected override void playerInteraction()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPlayerInteraction();
            Debug.Log("Game State: " + gameManager.gameState);
        }
    }
    private void handleSchedule()
    {
        Debug.Log("Hour To Leave: " + schedule.Peek().w);
        if (TimeManager.TimeHour == schedule.Peek().w)
        {
            advanceSchedule();
        }
    }
    private void advanceSchedule()
    {
        Vector4 v = schedule.Dequeue();
        schedule.Enqueue(v);
        _agent.SetDestination(v);
    }
}