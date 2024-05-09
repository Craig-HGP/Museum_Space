using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coinSound; // Sound effect to play when collecting the coin
    public int pointValue = 1; // Number of points to add to the player
    private Renderer render;
    private AudioSource audioSource;
    private bool hasBeenCollected = false;
    public float rotationSpeed = 100f; // Adjust the rotation speed as needed

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        coinSound = audioSource.clip;
        render = GetComponent<Renderer>();
    }
    void Update()
    {
        // Rotate the coin around its up axis
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        // Check if the coin hasn't been collected yet and the player collects it
        if (!hasBeenCollected && other.CompareTag("Player"))
        {
            CollectCoin();
        }
    }

    void CollectCoin()
    {
        // Add points to the player
        GameManager.Instance.AddPoints(pointValue);

        // Play sound effect
        audioSource.Play();

        // Disable renderer to make the coin invisible while the sound is playing
        render.enabled = false;

        // Mark the coin as collected
        hasBeenCollected = true;

        // Destroy the coin object after the sound effect finishes playing
        Destroy(gameObject, coinSound.length);
    }
}

