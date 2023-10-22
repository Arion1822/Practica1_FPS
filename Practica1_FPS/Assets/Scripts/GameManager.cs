using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    private int score = 0;
    private int maxScore = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI countdownText;
    public GameObject dartboards;
    public GameObject galleryInfoCanvas;
    private bool playingGalleryGame;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI shieldText;
    public TextMeshProUGUI currentAmmoText;
    public GameObject gameOver;


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
        maxScore = Mathf.Max(maxScore, score);
        UpdateScoreText();
    }
    
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void StartGalleryGame()
    {
        if (dartboards != null && !playingGalleryGame)
        {
            playingGalleryGame = true;
            dartboards.SetActive(true);
            galleryInfoCanvas.SetActive(true);
            score = 0;
            UpdateScoreText();
            StartCoroutine(CountdownCoroutine(30));
        }
    }

    public void StopGalleryGame()
    {
        if (dartboards != null && playingGalleryGame)
        {
            playingGalleryGame = false;
            dartboards.SetActive(false);
            galleryInfoCanvas.SetActive(false);
        }
    }

    public void UpdateLifeAndShieldText(int life, int shield)
    {
        lifeText.SetText(life.ToString());
        shieldText.SetText(shield.ToString());
        
    }

    public void UpdateAmmoText(int currentMagazineAmmo, int currentTotalAmmo)
    {
        currentAmmoText.SetText(currentMagazineAmmo+"/"+currentTotalAmmo);
    }
    
    private IEnumerator CountdownCoroutine(int countdownDuration)
    {
        int timeRemaining = countdownDuration;

        while (timeRemaining > 0)
        {
            if (countdownText != null)
            {
                countdownText.text = "Time: " + timeRemaining + "s";
            }

            yield return new WaitForSeconds(1f);

            timeRemaining--;
        }

        StopGalleryGame();
    }

    public void GoToFollowingScene()
    {
        if (maxScore > 300)
        {
            SceneManager.LoadScene(1);
        }
        
    }
    
    private void OnDestroy()
    {
        if (this == instance)
        {
            instance = null;
        }
    }

    public void PlayerDied()
    {
        gameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Restart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Cursor.lockState = CursorLockMode.Locked;
    }
}