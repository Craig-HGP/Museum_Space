using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public CameraController cameraController; // Reference to the CameraController script

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger camera transition when player enters the trigger area
            cameraController.TransitionToWideAngle();
        }
    }
}
