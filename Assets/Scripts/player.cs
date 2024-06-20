using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform cameraTransform; // Reference to the camera transform
    private CharacterController controller; // Reference to the CharacterController component
    public LayerMask groundLayer; // Layer to check if the player is grounded
    public float speed = 10f; // Movement speed
    public float rotationSpeed = 7f; // Rotation speed
    private float pitch = 0f; // Current pitch rotation of the camera
    public float pitchSpeed = 7f; // Pitch speed for looking up and down
    public float pitchRange = 45f; // Pitch range limit
    private bool isGrounded; // Whether the player is on the ground

    public float groundCheckDist = 1.0f; // Check if grounded by distance
    public float jumpForce = 5f; // Jump force
    public float gravity = -9.81f; // Gravity constant value
    public float gravityScale = 3f; // Adjust the gravity scale as needed
    private Vector3 velocity; // Player's current velocity

    void Start()
    {
        // Get the CharacterController component attached to this GameObject
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Handle player movement based on input
        MovePlayer();

        // Handle looking around with the mouse/trackpad
        LookAround();

        // Apply gravity to the player
        ApplyGravity();

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void MovePlayer()
    {
        // Get input from WASD keys or arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate movement direction relative to the camera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ensure the movement direction is horizontal
        forward.y = 0f;
        right.y = 0f;

        // Normalize directions to ensure consistent speed
        forward.Normalize();
        right.Normalize();

        // Calculate movement direction
        Vector3 movement = forward * moveVertical + right * moveHorizontal;

        // Move the player using the CharacterController
        controller.Move(movement * speed * Time.deltaTime);
    }

    void LookAround()
    {
        // Get mouse input for rotation
        float rotateVertical = Input.GetAxis("Mouse Y");
        float rotateHorizontal = Input.GetAxis("Mouse X");
        
        Debug.Log("Pitch value: " + Input.GetAxis("Mouse Y"));

        // Rotate the player around the Y-axis (horizontal rotation)
        transform.Rotate(Vector3.up * rotateHorizontal * rotationSpeed);

        // Adjust the pitch (vertical rotation) of the camera
        pitch -= rotateVertical * pitchSpeed;
        pitch = Mathf.Clamp(pitch, -pitchRange, pitchRange); // Clamp pitch to avoid flipping over

        // Apply the pitch rotation to the camera
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void Jump()
    {
        // Set the velocity for the jump
        velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity * gravityScale);
    }

    void ApplyGravity()
    {
        // Check if the player is grounded
        isGrounded = controller.isGrounded || IsGrounded();

        // If grounded and descending, set a small negative value to keep the player grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Apply gravity to the velocity
        velocity.y += gravity * gravityScale * Time.deltaTime;

        // Move the player using the CharacterController
        controller.Move(velocity * Time.deltaTime);
    }

    bool IsGrounded()
    {
        // Perform a Raycast downward to check if there's ground beneath the player
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDist, groundLayer);
    }
}