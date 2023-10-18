using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance; // Singleton instance

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private int score = 0; // Score variable
    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        score = 0;
    }

    // Method to add score points
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    // Method to retrieve the current score
    public int GetScore()
    {
        return score;
    }
    
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString(); // Update the TMP text with the current score
        }
    }

    // OnDestroy is called when the object is being destroyed
    private void OnDestroy()
    {
        if (this == instance)
        {
            instance = null;
        }
    }
}