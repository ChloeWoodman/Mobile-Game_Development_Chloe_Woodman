using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int startingPoints = 1000;
    public int decreaseRate = 10; // Points decrease per second
    public int goalPoints = 0; // The goal points you want to reach

    private static int currentPoints; // Make the score variable static

    void Start()
    {
        currentPoints = startingPoints;
        UpdateScoreText();

        // Call a method to start decreasing points over time
        InvokeRepeating("DecreasePointsOverTime", 1f, 1f);
    }

    void DecreasePointsOverTime()
    {
        currentPoints -= decreaseRate;
        UpdateScoreText();

        if (currentPoints <= goalPoints)
        {
            // You've reached the goal! Save the score.
            PlayerPrefs.SetInt("Score", currentPoints);
            PlayerPrefs.Save();

            // Check if the player's health is also 0 before transitioning to the game over scene
            if (PlayerHealth.GetCurrentLives() <= 0)
            {
                // Player's health is 0, move to the game over scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }
        }

        // Add this block to change the scene when currentPoints reaches 0
        if (currentPoints <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Points: " + currentPoints.ToString();
    }

    // Add a static method to access the currentPoints value
    public static int GetCurrentPoints()
    {
        return currentPoints;
    }
}
