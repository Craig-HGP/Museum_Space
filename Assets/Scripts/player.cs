using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f; // Adjust the speed as needed
    public float rotationSpeed = 7f; // Adjust the rotation speed as needed
    public float pitchSpeed = 7f; // Adjust the pitch (up and down) speed as needed
    public float pitchRange = 50f; // Adjust the maximum pitch angle (in degrees) as needed
    private Rigidbody rb;
    private float pitch = 0f;
    public float jumpForce = 100f; // Adjust the jump force as needed
    public float groundCheckDistance = 0.1f; // Distance to check for ground
    public LayerMask groundLayer; // Layer mask for the ground objects
    public float mass = 100f; // Adjust the gravity scale as needed
    private bool isGrounded;
    public Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Lock rotation along X and Z axes
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Adjust Rigidbody properties
        rb.drag = 0f; // Adjust drag for more realistic falling
        rb.angularDrag = 0.5f; // Adjust angular drag as needed

        // Set the mass
        rb.mass = mass;

        // Set collision detection mode
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Set physics material to reduce bounciness
        Collider collider = GetComponent<Collider>();
        collider.material = new PhysicMaterial()
        {
            bounciness = 0,
            frictionCombine = PhysicMaterialCombine.Multiply,
            bounceCombine = PhysicMaterialCombine.Multiply
        };
    }

    void Update()
    {
        // Get input from arrow keys or WASD keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Get rotation input from mouse
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");

        // Calculate the movement direction relative to the camera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ensure the forward vector is horizontal
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 movement = forward * moveVertical + right * moveHorizontal;

        // Normalize movement vector to ensure consistent speed diagonally
        movement.Normalize();

        // Rotate the player based on mouse input
        transform.Rotate(Vector3.up * rotateHorizontal * rotationSpeed);

        // Adjust the pitch (up and down) based on mouse input
        pitch -= rotateVertical * pitchSpeed;
        pitch = Mathf.Clamp(pitch, -pitchRange, pitchRange); // Clamp pitch to avoid flipping upside down

        // Move the player
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Apply rotation to player object
        transform.localRotation = Quaternion.Euler(pitch, transform.localEulerAngles.y, 0f);

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }
    }

    void Jump()
    {
        // Apply jump force if grounded
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Check if the player is grounded using raycast
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        // Apply gravity manually if needed
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * mass * Physics.gravity.y);
        }
    }

    bool IsGrounded()
    {
        // Perform a Raycast downward to check if there's ground beneath the player
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }
}
