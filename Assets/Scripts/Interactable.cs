using System.Collections;
using UnityEngine;
public abstract class Interactable : MonoBehaviour
{
    protected Quaternion _lookAtCamera;
    protected float _distanceFromCamera;
    private bool _interactable;
    public bool isInteractable() => _interactable;
    protected Quaternion LookAtCamera { get => _lookAtCamera; }
    protected float DistanceFromCamera { get => _distanceFromCamera; }
    private void Start()
    {
        _lookAtCamera = Quaternion.LookRotation((Camera.main.transform.forward).normalized);
    }
    private void Update()
    {
        _lookAtCamera = Quaternion.LookRotation((Camera.main.transform.forward).normalized);
    }
    public abstract void ShowNameplate();
    public abstract void HideNameplate();
    protected abstract void beginPlayerInteraction();
    protected abstract void exitPlayerInteraction();
    protected abstract void playerInteraction();
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