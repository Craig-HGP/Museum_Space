using System.Collections;
using System.Collections.Generic;
using Cinemachine; 
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public GameObject wideAngleCamera;


    private void Start()
    {
        // Ensure that a virtual camera is assigned
        if (wideAngleCamera == null)
        {
            Debug.LogError("Room Camera is not assigned to the CameraPriorityChanger script!");
        }

        wideAngleCamera.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wideAngleCamera.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wideAngleCamera.SetActive(false);
        }
    }
}
