using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class WinScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI previousScoresText;
    public int numberOfPreviousScoresToShow = 5;

    private void Start()
    {
        // Retrieve the new score from the Score script using the static method.
        int newScore = Score.GetCurrentPoints();

        // Save the new score and update the previous scores list.
        SaveScore(newScore);

        // Display the new score and the previous scores.
        DisplayScores();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void MainMenuReturn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    void SaveScore(int newScore)
    {
        List<int> previousScores = new List<int>();

        // Load the previous scores from PlayerPrefs, if any.
        if (PlayerPrefs.HasKey("PreviousScores"))
        {
            string scoresString = PlayerPrefs.GetString("PreviousScores");
            previousScores = new List<int>(Array.ConvertAll(scoresString.Split(','), int.Parse));
        }

        // Add the new score to the list of previous scores.
        previousScores.Add(newScore);

        // Keep only the latest 'numberOfPreviousScoresToShow' scores.
        if (previousScores.Count > numberOfPreviousScoresToShow)
        {
            previousScores.RemoveAt(0);
        }

        // Save the updated list of previous scores back to PlayerPrefs.
        string updatedScoresString = string.Join(",", previousScores.ConvertAll(i => i.ToString()).ToArray());
        PlayerPrefs.SetString("PreviousScores", updatedScoresString);
        PlayerPrefs.Save();
    }

    void DisplayScores()
    {
        // Retrieve the new score from the Score script using the static method.
        int newScore = Score.GetCurrentPoints();

        // Display the new score in the TextMeshProUGUI element.
        scoreText.text = "Score: " + newScore.ToString();

        // Display the previous scores.
        string previousScoresString = PlayerPrefs.GetString("PreviousScores", "");
        List<int> previousScores = new List<int>();

        if (!string.IsNullOrEmpty(previousScoresString))
        {
            previousScores = new List<int>(Array.ConvertAll(previousScoresString.Split(','), int.Parse));
        }

        // Limit the number of previous scores displayed to 'numberOfPreviousScoresToShow'.
        if (previousScores.Count > numberOfPreviousScoresToShow)
        {
            previousScores.RemoveRange(0, previousScores.Count - numberOfPreviousScoresToShow);
        }

        previousScoresText.text = "Previous Scores:\n";
        foreach (int score in previousScores)
        {
            previousScoresText.text += score + "\n";
        }
    }
}
