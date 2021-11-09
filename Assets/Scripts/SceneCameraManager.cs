using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneCameraManager : MonoBehaviour
{
    private GameObject[] camLocObj;
    public Transform[] cameraLocations;
    public Transform currentCameraLocation;
    void Start()
    {
        camLocObj = GameObject.FindGameObjectsWithTag("Camera Locations");
        int i = 0;
        foreach (GameObject c in camLocObj)
        {
            cameraLocations[i] = c.transform;
            i++;
        }
        currentCameraLocation = cameraLocations[0];
    }
    void Update()
    {
        if (GameManager.Instance.player.transform.position.z <= 45f) currentCameraLocation = cameraLocations[0];
        else currentCameraLocation = cameraLocations[1];
        Camera.main.gameObject.transform.localPosition = currentCameraLocation.localPosition;
        Camera.main.gameObject.transform.localRotation = currentCameraLocation.localRotation;
    }
}