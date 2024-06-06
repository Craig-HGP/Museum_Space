using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float transitionDuration = 1.0f;
    public float finalFOV = 60f;
    public Vector3 finalFollowOffset = new Vector3(0f, 1.5f, -5f);

    private float initialFOV;
    private Vector3 initialFollowOffset;
    private bool isTransitioning = false;

    void Start()
    {
        // Store initial camera settings
        initialFOV = virtualCamera.m_Lens.FieldOfView;
        initialFollowOffset = virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    void Update()
    {
        // If a transition is in progress, update camera properties over time
        if (isTransitioning)
        {
            float t = Mathf.Clamp01(Time.deltaTime / transitionDuration);
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, finalFOV, t);

            CinemachineTransposer transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, finalFollowOffset, t);

            // Check if the transition is complete
            if (Mathf.Approximately(virtualCamera.m_Lens.FieldOfView, finalFOV) &&
                transposer.m_FollowOffset == finalFollowOffset)
            {
                isTransitioning = false;
            }
        }
    }

    public void TransitionToWideAngle()
    {
        // Start camera transition to wider angle
        isTransitioning = true;
    }

    public void TransitionToSidescrollAngle()
    {
        // Start camera transition to wider angle
        isTransitioning = true;
    }

    public void ResetCamera()
    {
        // Start camera transition back to initial settings
        finalFOV = initialFOV;
        finalFollowOffset = initialFollowOffset;
        isTransitioning = true;
    }
}
