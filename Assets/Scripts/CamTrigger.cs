using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class CamTrigger : MonoBehaviour
{
    public GameObject wideAngleCamera;
    public GameObject sidescrollAngleCameraLeft;
    public GameObject sidescrollAngleCameraRight;
    public GameObject tightAngleCamera;


    private void Start()
    {
        tightAngleCamera.SetActive(true);
        
        wideAngleCamera.SetActive(false);

        sidescrollAngleCameraLeft.SetActive(false);

        sidescrollAngleCameraRight.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (CompareTag("RoomTrigger") && other.CompareTag("Player"))
        {
            wideAngleCamera.SetActive(true);
            sidescrollAngleCameraLeft.SetActive(false);
            sidescrollAngleCameraRight.SetActive(false);
            tightAngleCamera.SetActive(false);
        }
        else if (CompareTag("LeftPlatformTrigger") && other.CompareTag("Player"))
        {
            wideAngleCamera.SetActive(false);
            sidescrollAngleCameraLeft.SetActive(true);
            sidescrollAngleCameraRight.SetActive(false);
        }
        else if (CompareTag("RightPlatformTrigger") && other.CompareTag("Player"))
        {
            wideAngleCamera.SetActive(false);
            sidescrollAngleCameraLeft.SetActive(false);
            sidescrollAngleCameraRight.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CompareTag("RoomTrigger") && other.CompareTag("Player"))
        {
            wideAngleCamera.SetActive(false);
            tightAngleCamera.SetActive(true);
        }
        else if (CompareTag("LeftPlatformTrigger") && other.CompareTag("Player"))
        {
            sidescrollAngleCameraLeft.SetActive(false);
            wideAngleCamera.SetActive(true);
        }
        else if (CompareTag("RightPlatformTrigger") && other.CompareTag("Player"))
        {
            sidescrollAngleCameraRight.SetActive(false);
            wideAngleCamera.SetActive(true);
        }
    }
}
