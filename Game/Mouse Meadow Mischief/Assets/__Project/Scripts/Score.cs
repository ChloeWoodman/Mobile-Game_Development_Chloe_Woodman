using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    //public TextMeshProUGUI oldScoreText;
    public int startingPoints = 1000;
    public int decreaseRate = 10; // Points decrease per second
    public int goalPoints = 0; // The goal points you want to reach

    private static int currentPoints; // Make the score variable static
    private List<int> oldScores = new List<int>(); // Store old scores

    void Start()
    {
        LoadOldScores();
        currentPoints = startingPoints;
        UpdateScoreText();
        //UpdateOldScoreText();

        // Call a method to start decreasing points over time
        InvokeRepeating("DecreasePointsOverTime", 1f, 1f);
    }

    void DecreasePointsOverTime()
    {
        currentPoints -= decreaseRate;
        UpdateScoreText();

        if (currentPoints <= goalPoints)
        {
            // You've reached the goal! Save the score and add it to the old scores list.
            oldScores.Add(currentPoints);
            SaveOldScores();
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

    //void UpdateOldScoreText()
    //{
    //    string oldScoreString = "Old Scores: ";
    //    foreach (int score in oldScores)
    //    {
    //        oldScoreString += score + " ";
    //    }
    //    oldScoreText.text = oldScoreString;
    //}

    // Add a static method to access the currentPoints value
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
