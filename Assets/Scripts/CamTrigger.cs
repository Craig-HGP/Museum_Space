using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class CamTrigger : MonoBehaviour
{
    public GameObject wideAngleCamera;
    public GameObject sidescrollAngleCamera;


    private void Start()
    {
        // Ensure that a virtual camera is assigned
        if (wideAngleCamera == null)
        {
            Debug.LogError("Room Camera is not assigned to the CameraPriorityChanger script!");
        }

        wideAngleCamera.SetActive(false);

        if (sidescrollAngleCamera == null)
        {
            Debug.LogError("Platform Camera is not assigned to the CameraPriorityChanger script!");
        }

        sidescrollAngleCamera.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (CompareTag("RoomTrigger") && other.CompareTag("Player"))
        {
            wideAngleCamera.SetActive(true);
        }
        else if (CompareTag("PlatformTrigger") && other.CompareTag("Player"))
        {
            wideAngleCamera.SetActive(false);
            sidescrollAngleCamera.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CompareTag("RoomTrigger") && other.CompareTag("Player"))
        {
            wideAngleCamera.SetActive(false);
        }
        else if (CompareTag("PlatformTrigger") && other.CompareTag("Player"))
        {
            sidescrollAngleCamera.SetActive(false);
            wideAngleCamera.SetActive(true);
        }
    }
}
