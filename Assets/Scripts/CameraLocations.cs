using UnityEngine;
public class CameraLocations : MonoBehaviour
{
    [SerializeField] private Transform xform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(GameManager.Instance.player))
        {
            GameManager.Instance.SetCameraTransform(xform);
        }
    }
}