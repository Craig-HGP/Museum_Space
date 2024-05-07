using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f; // Adjust the speed as needed
    public float rotationSpeed = 7f; // Adjust the rotation speed as needed
    public float pitchSpeed = 7f; // Adjust the pitch (up and down) speed as needed
    public float pitchRange = 80f; // Adjust the maximum pitch angle (in degrees) as needed
    private Rigidbody rb;
    private float pitch = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Lock rotation along X and Z axes
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Increase interpolation to smooth out movement
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from arrow keys or WASD keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Get rotation input from trackpad
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");

        // Calculate movement direction
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Normalize movement vector to ensure consistent speed diagonally
        movement.Normalize();

        // Rotate the player based on trackpad input
        transform.Rotate(Vector3.up * rotateHorizontal * rotationSpeed);

        // Adjust the pitch (up and down) based on mouse input
        pitch -= rotateVertical * pitchSpeed;
        pitch = Mathf.Clamp(pitch, -pitchRange, pitchRange); // Clamp pitch to avoid flipping upside down

        // Apply rotation to camera (or player object if you want)
        Camera.main.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // Move the player
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
