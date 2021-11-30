using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using Ink.Runtime;
public enum NPCJob { Fighter, Merchant, Shopkeeper }
public enum NPCPersonality { Friendly, Rude, Reserved }
public class NonPlayerCharacter : Interactable
{
    [System.Serializable]
    public struct ScheduleNode
    {
        public Vector3 worldPosition;
        public int hourToLeave;
    }
    [System.Serializable]
    public struct ConnectionLogic
    {
        public NPCJob job;
        [Range(0f, 1f)] public float wealth;
        public NPCPersonality personality;
    }
    [System.Serializable]
    public struct GiftsAcceptedOrItemsWanted
    {
        public ItemSO giftOrItem;
        public NonPlayerCharacter relationshipToAffect;
        public float relationshipChangeValue;
    }
    [System.Serializable]
    public struct NPC_Story
    {
        [Header("Set this integer to -1 if no items are requisite for this story.")]
        public int giftOrItemTriggerIndex;
        public TextAsset story;
    }
    private GameManager _gameManager;
    private TimeManager _timeManager;
    private RelationshipManager _relationshipManager;
    public string characterName;
    public Family family;
    [SerializeField] public ScheduleNode[] scheduleNodes;
    public Queue<ScheduleNode> schedule;
    [SerializeField] public ConnectionLogic connectionLogic;
    [SerializeField] public GiftsAcceptedOrItemsWanted[] giftsAcceptedOrItemsWanted;
    public List<GiftsAcceptedOrItemsWanted> wanteds;
    //private List<GiftsAcceptedOrItemsWanted> wanteds;
    private BasicInkExample storyController;
    [SerializeField] public NPC_Story[] story;
    public NPCState npcState, previousNpcState;
    [SerializeField] private TextMeshPro _textName;
    private NavMeshAgent _agent;
    [SerializeField] private Animator _anim;
    public bool WantsItem { get; private set; }
    public Item _haveWantedItem = null;
    //private Item _haveWantedItem = null;
    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _timeManager = TimeManager.Instance;
        _agent = this.GetComponent<NavMeshAgent>();
        storyController = transform.Find("StoryController").GetComponent<BasicInkExample>();
        wanteds = giftsAcceptedOrItemsWanted.ToList();
    }
    void Start()
    {
        WantsItem = (giftsAcceptedOrItemsWanted.Count<GiftsAcceptedOrItemsWanted>() > 0);
        _anim.SetBool("isWalking", false);
        _relationshipManager = GameObject.Find("RelationshipManager").GetComponent<RelationshipManager>();
        npcState = NPCState.IDLE;
        _textName.text = characterName;
        schedule = makeSchedule();
        advanceSchedule();
        npcState = NPCState.IDLE;
        SetStoryFile();
    }
    void Update()
    {
        _haveWantedItem = ItemOrGiftCheck();
        _textName.gameObject.transform.rotation = Quaternion.LookRotation((Camera.main.transform.forward).normalized);
        if (npcState == NPCState.PLAYERINTERACT)
        {
            playerInteraction();
        }
        if (_agent.velocity.magnitude >= 0.05f && npcState != NPCState.PLAYERINTERACT)
        {
            npcState = NPCState.MOVINGTONODE;
        }
        if (_agent.remainingDistance <= float.Epsilon && npcState == NPCState.MOVINGTONODE)
        {
            npcState = NPCState.IDLE;
        }
        if (this.isInteractable())
        {
            ShowNameplate();
            if (npcState != NPCState.PLAYERINTERACT && Input.GetKeyDown(KeyCode.Space))
            {
                beginPlayerInteraction();
            }
        }
        else HideNameplate();
        handleSchedule();
        AnimationState();
    }
    override public void ShowNameplate()
    {
        if (!_textName.gameObject.activeSelf)
        {
            _textName.gameObject.SetActive(true);
        }
    }
    public override void HideNameplate()
    {
        if (_textName.gameObject.activeSelf)
        {
            _textName.gameObject.SetActive(false);
        }
    }
    #region PLAYER INTERACTION
    protected override void beginPlayerInteraction()
    {
        previousNpcState = npcState;
        npcState = NPCState.PLAYERINTERACT;
        _gameManager.gameState = GameState.NPCINTERACTION;
        _agent.isStopped = true;
        this.transform.LookAt(GameManager.Instance.player.transform);
        GameManager.Instance.player.transform.LookAt(this.transform);
        GameManager.Instance.player.GetComponent<PlayerController>().EnableOverShoulderView();
        _relationshipManager.PrintRelationships(this.gameObject);
        SetStoryFile();
        storyController.gameObject.SetActive(true);
        storyController.StartStory();
        Debug.Log((_haveWantedItem != null) ? "Player has wanted item." : "Player doesn't have the wanted item.");
    }
    protected override void playerInteraction()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitPlayerInteraction();
        }
    }
    protected override void exitPlayerInteraction()
    {
        npcState = previousNpcState;
        previousNpcState = NPCState.IDLE;
        _agent.isStopped = false;
        _gameManager.gameState = GameState.PLAY;
        GameManager.Instance.player.GetComponent<PlayerController>().DisableOverShoulderView();
    }
    #endregion PLAYER INTERACTION
    #region SCHEDULE
    private Queue<ScheduleNode> makeSchedule()
    {
        Queue<ScheduleNode> q = new Queue<ScheduleNode>();
        foreach (ScheduleNode s in scheduleNodes)
        {
            q.Enqueue(s);
        }
        return q;
    }
    private void handleSchedule()
    {
        if (_timeManager.TimeHour == schedule.Peek().hourToLeave)
        {
            advanceSchedule();
        }
    }
    private void advanceSchedule()
    {
        ScheduleNode s = schedule.Dequeue();
        schedule.Enqueue(s);
        _agent.SetDestination(s.worldPosition);
        npcState = NPCState.MOVINGTONODE;
    }
    #endregion SCHEDULE
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        int i = 0;
        foreach (ScheduleNode s in scheduleNodes)
        {
            Gizmos.DrawWireSphere(s.worldPosition, 1f);
            Handles.Label(new Vector3(s.worldPosition.x, s.worldPosition.y, s.worldPosition.z), "Node " + i + " leave @ hour " + s.hourToLeave);
            i++;
        }
    }
    private void OnDrawGizmos()
    {
        Handles.Label(this.transform.position, characterName);
        Handles.DrawLine(this.transform.localPosition, this.transform.localPosition + this.transform.forward, 5f);
    }
#endif
    private void AnimationState()
    {
        switch (npcState)
        {
            default:
            case NPCState.IDLE:
                _anim.SetBool("isWalking", false);
                break;
            case NPCState.MOVINGTONODE:
                _anim.SetBool("isWalking", true);
                break;
        }
    }
    private Item ItemOrGiftCheck()
    {
        if (!WantsItem) return null;
        Item haveWantedItem = null;
        foreach (Item i in Inventory.Instance.GetItemList())
        {
            for (int x = 0; x < giftsAcceptedOrItemsWanted.Count(); x++)
            {
                if (giftsAcceptedOrItemsWanted[x].giftOrItem.itemName == i.itemName)
                {
                    haveWantedItem = i;
                }
            }
        }
        return haveWantedItem;
    }
    private void ReceiveItem(Item item)
    {
        for (int i = 0; i < wanteds.Count; i++)
        {
            if (wanteds[i].giftOrItem.itemName == item.itemName)
            {
                Inventory.RemoveItem(item);
                wanteds.Remove(wanteds[i]);
            }
        }
    }
    private void ReceiveItem(string i)
    {
        if (_haveWantedItem.itemName == i)
        {
            Debug.Log($"Player has {i}");
            if (wanteds.Count > 0)
            {
                Debug.Log($"'wanteds' list contains: ");
                foreach (GiftsAcceptedOrItemsWanted g in wanteds)
                {
                    Debug.Log(g.giftOrItem.itemName + ", ");
                }
            }
            for (int j = 0; j < wanteds.Count; j++)
            {
                if (wanteds[j].giftOrItem.itemName.Equals(i))
                {
                    Debug.Log($"Found in 'wanteds' list");
                    if (wanteds.Remove(wanteds.First(it => it.giftOrItem.itemName == i))) Debug.Log($"Removed {i} from 'wanteds' list. \nProof:");
                    Inventory.RemoveItem(i);
                    //if (wanteds.Count > 0)
                    //{
                    //    foreach (GiftsAcceptedOrItemsWanted gg in wanteds)
                    //    {
                    //        Debug.Log(gg.giftOrItem.itemName);
                    //    }
                    //    continue;
                    //}
                    //else if (wanteds.Count == 0)
                    //{
                    //    Debug.Log("'wanteds' list is empty.");
                    //    continue;
                    //}
                }
                //if (wanteds.Count == 0) continue;
            }
        }
        else
        {
            Debug.Log($"Player does not have {i}");
        }
    }
    private void SetStoryFile()
    {
        foreach (NPC_Story n in story)
        {
            if (WantsItem && n.giftOrItemTriggerIndex > -1 && _haveWantedItem != null)
            {
                storyController.SetStoryFile(n.story);
                if (wanteds.Count > 0)
                {
                    storyController.story.BindExternalFunction("receiveItem", (string iN) =>
                    {
                        ReceiveItem(iN);
                        Debug.Log("ReceiveItem called");
                    });
                    return;
                }
                else return;
            }
            else if (n.giftOrItemTriggerIndex == -1)
            {
                storyController.SetStoryFile(n.story);
                return;
            }
        }
    }
}