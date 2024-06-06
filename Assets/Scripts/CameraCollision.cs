using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public LayerMask collisionMask; // Mask to determine which layers should be considered for collision detection
    public float minDistance = 1f; // Minimum distance between camera and player
    public float maxDistance = 5f; // Maximum distance between camera and player
    public float smoothSpeed = 5f; // Speed of camera movement

    private Camera mainCamera;

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            // Calculate the direction from the camera to the player
            Vector3 direction = target.position - transform.position;

            // Calculate the distance from the camera to the player
            float distance = direction.magnitude;

            // Raycast from the camera towards the player to detect obstacles
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, maxDistance, collisionMask))
            {
                // If there's an obstacle, adjust the camera position
                float targetDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
                Vector3 targetPosition = target.position - transform.forward * targetDistance;
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            }
            else
            {
                // If there's no obstacle, maintain the default distance from the player
                Vector3 targetPosition = target.position - transform.forward * maxDistance;
                transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
            }
        }
    }
}

