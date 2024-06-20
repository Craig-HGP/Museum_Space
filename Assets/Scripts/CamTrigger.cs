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
    public GameObject tightAngleCamera;


    private void Start()
    {
        tightAngleCamera.SetActive(true);
        
        wideAngleCamera.SetActive(false);

        sidescrollAngleCamera.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (CompareTag("RoomTrigger") && other.CompareTag("Player"))
        {
            wideAngleCamera.SetActive(true);
            sidescrollAngleCamera.SetActive(false);
            tightAngleCamera.SetActive(false);
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
            tightAngleCamera.SetActive(true);
        }
        else if (CompareTag("PlatformTrigger") && other.CompareTag("Player"))
        {
            sidescrollAngleCamera.SetActive(false);
            wideAngleCamera.SetActive(true);
        }
    }
}
