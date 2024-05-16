using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f; // Adjust the speed as needed
    public float rotationSpeed = 7f; // Adjust the rotation speed as needed
    public float pitchSpeed = 7f; // Adjust the pitch (up and down) speed as needed
    public float pitchRange = 80f; // Adjust the maximum pitch angle (in degrees) as needed
    private Rigidbody rb;
    private float pitch = 0f;
    public float jumpForce = 20f; // Adjust the jump force as needed
    public float groundCheckDistance = 0.1f; // Distance to check for ground
    public LayerMask groundLayer; // Layer mask for the ground objects
    public float mass = 3f; // Adjust the gravity scale as needed


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Lock rotation along X and Z axes
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Adjust Rigidbody properties
        rb.drag = 0f; // Leave as 0 to make it drop realistically
        rb.angularDrag = 5f; // Adjust angular drag as needed

        // Set the gravity scale
        rb.mass = mass;

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
        //transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        float currentRotationX = transform.localEulerAngles.x;
        transform.localRotation = Quaternion.Euler(pitch, transform.localEulerAngles.y, 0f);

        // Move the player
        transform.Translate(movement * speed * Time.deltaTime);
    
        // Jump input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    void Jump()
    {
        // Check if the player is grounded before jumping (optional)
        // You can use Raycasting or a Grounded check to determine if the player is grounded

        // Apply jump force
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    bool IsGrounded()
    {
        // Perform a Raycast downward to check if there's ground beneath the player
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }

}
