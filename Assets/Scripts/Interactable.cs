using System.Collections;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private bool _interactable;
    public bool isInteractable() => _interactable;
    public abstract void ShowNameplate();
    public abstract void HideNameplate();
    protected abstract void beginPlayerInteraction();
    protected abstract void exitPlayerInteraction();
    protected abstract void playerInteraction();
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            this._interactable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            this._interactable = false;
        }
    }
}