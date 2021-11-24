using System.Collections;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    protected Quaternion _lookAtCamera;
    private bool _interactable;
    public bool isInteractable() => _interactable;
    private void Awake()
    {
        _lookAtCamera = Quaternion.LookRotation((Camera.main.transform.forward).normalized);
    }
    public abstract void ShowNameplate();
    public abstract void HideNameplate();
    protected abstract void beginPlayerInteraction();
    protected abstract void exitPlayerInteraction();
    protected abstract void playerInteraction();
    protected Quaternion LookAtCamera()
    {
        return _lookAtCamera;
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            this._interactable = true;
        }
    }
    protected void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            this._interactable = false;
        }
    }
}