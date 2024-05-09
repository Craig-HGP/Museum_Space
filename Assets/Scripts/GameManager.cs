using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int score = 0;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Initialize the UI Text component reference
        scoreText.text = "Score: " + score.ToString();
    }

    public void AddPoints(int points)
    {
        score += points;
        // Update the UI Text component with the current score
        scoreText.text = "Score: " + score.ToString();
    }
}

