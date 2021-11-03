using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private bool _interactable;
    [SerializeField] private float _interactDistance;
    public bool isInteractable()
    {
        _interactable = GameManager.Instance.ProximityCheck(GameManager.Instance.player.transform, this.transform, _interactDistance);
        return _interactable;
    }
    public bool isFacingAndNearby()
    {
        return GameManager.Instance.FacingCheck(GameManager.Instance.player.transform, this.transform);
    }
    public abstract void showNameplate();
    public abstract void hideNameplate();
    protected abstract void beginPlayerInteraction();
    protected abstract void exitPlayerInteraction();
    protected abstract void playerInteraction();
}