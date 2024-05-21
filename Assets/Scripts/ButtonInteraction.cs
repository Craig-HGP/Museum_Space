using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip doorSound; // Sound effect to play when door opens
    public GameObject door; // Assign the door GameObject in the inspector
    public TextMeshProUGUI prompt; // add the TextMeshPro object that will be shown when in the trigger zone
    private bool isPlayerNear = false;
    public float slideDistance = 11f; // Distance the door will slide down
    public float slideDuration = 2f; // Duration of the slide in seconds
    private bool isDoorOpen = false; // To ensure the door only opens once

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        doorSound = audioSource.clip;
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && !isDoorOpen)
        {
            isDoorOpen = true;
            StartCoroutine(SlideDoorDown());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            // Optionally, display a UI prompt here to press F to interact
            prompt.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            // Optionally, hide the UI prompt here
            prompt.enabled = false;
        }
    }

    private IEnumerator SlideDoorDown()
    {
        // Play sound effect
        audioSource.Play();

        Vector3 startPosition = door.transform.position;
        Vector3 endPosition = startPosition - new Vector3(0, slideDistance, 0);
        float elapsedTime = 0;

        while (elapsedTime < slideDuration)
        {
            door.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / slideDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        door.transform.position = endPosition;
    }
}
