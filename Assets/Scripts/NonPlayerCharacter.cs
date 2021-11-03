using UnityEngine;
using TMPro;
public class NonPlayerCharacter : Interactable
{
    private GameManager gameManager;
    public string npcName;
    public Faction faction = Faction.REDTEAM;
    public NPCState npcState = NPCState.IDLE;
    private NPCState _previousState;
    [SerializeField] private TextMeshPro _textName;
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    void Start()
    {
        _textName.text = npcName;
    }
    void Update()
    {
        if (this.isInteractable() && this.isFacingAndNearby())
        {
            showNameplate();
            if (npcState != NPCState.PLAYERINTERACT && Input.GetKeyDown(KeyCode.Space))
            {
                beginPlayerInteraction();
                Debug.Log("Game State: " + gameManager.gameState);
            }
        }
        else hideNameplate();
        if (npcState == NPCState.PLAYERINTERACT)
        {
            playerInteraction();
        }
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
}