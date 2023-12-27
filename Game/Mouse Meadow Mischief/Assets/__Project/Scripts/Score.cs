using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI oldScoreText;
    public int startingPoints = 1000;
    public int decreaseRate = 10; // Points decrease per second
    public int goalPoints = 0; // The goal points you want to reach
    public Canvas gameOverCanvas;
    public Canvas currentGameplayCanvas;
    private static int currentPoints; // Make the score variable static
    private List<int> oldScores = new List<int>(); // Store old scores

    void Start()
    {
        currentPoints = startingPoints; // Reset current points to starting value
        LoadOldScores();
        UpdateScoreText();
        // If there's a paused decrease points coroutine, make sure to stop it
        CancelInvoke("DecreasePointsOverTime");
        // Start decreasing points over time again
        InvokeRepeating("DecreasePointsOverTime", 1f, 1f);
    }

    void DecreasePointsOverTime()
    {
        currentPoints -= decreaseRate;
        UpdateScoreText();

        // Continuously check player's health
        if (PlayerHealth.GetCurrentLives() <= 0)
        {
            // Player's health is 0, move to the game over scene
            gameOverCanvas.gameObject.SetActive(true);
            currentGameplayCanvas.gameObject.SetActive(false);
            Time.timeScale = 0; // Pause the game
            CancelInvoke("DecreasePointsOverTime"); // Stop decreasing points
            return;
        }

        if (currentPoints <= goalPoints)
        {
            // Points have reached the goal
            oldScores.Add(currentPoints);
            SaveOldScores();
            PlayerPrefs.SetInt("Score", currentPoints);
            PlayerPrefs.Save();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Points: " + currentPoints.ToString();
    }

    public static int GetCurrentPoints()
    {
        return currentPoints;
    }

    // Save and load old scores
    private void SaveOldScores()
    {
        string oldScoresString = string.Join(",", oldScores.ConvertAll(score => score.ToString()));
        PlayerPrefs.SetString("OldScores", oldScoresString);
        PlayerPrefs.Save();
    }

    private void LoadOldScores()
    {
        string oldScoresString = PlayerPrefs.GetString("OldScores", "");
        if (!string.IsNullOrEmpty(oldScoresString))
        {
            string[] scores = oldScoresString.Split(',');
            oldScores = new List<int>(scores.Length);
            foreach (string score in scores)
            {
                if (int.TryParse(score, out int parsedScore))
                {
                    oldScores.Add(parsedScore);
                }
            }
        }
    }
}
