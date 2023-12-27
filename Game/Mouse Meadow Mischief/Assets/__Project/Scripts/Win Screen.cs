using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using LootLocker.Requests;

public class WinScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI previousScoresText;
    public Button progressToNextLevelButton;
    public int numberOfPreviousScoresToShow = 5;

    public string mainMenuSceneName;

    private int leaderboardID = 19075; // Leaderboard ID

    private void Start()
    {
        int newScore = Score.GetCurrentPoints();

        if (IsPlayerLoggedIn())
        {
            GetAndUploadScores(newScore);
            progressToNextLevelButton.gameObject.SetActive(true);
            FetchLeaderboardScores();
        }
        else
        {
            progressToNextLevelButton.gameObject.SetActive(false);
            previousScoresText.text = "Please log in to view leaderboard scores.";
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("OldScores");
        // Load the scene with the name assigned in the Unity Editor
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuReturn()
    {
        Time.timeScale = 1; //Run time again
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("OldScores");
        // Load the scene with the name assigned in the Unity Editor
        SceneManager.LoadScene(mainMenuSceneName);
    }

    bool IsPlayerLoggedIn()
    {
        return GameManager.IsPlayerLoggedIn;
    }

    void GetAndUploadScores(int newScore)
    {
        int playerId = PlayerPrefs.GetInt("PlayerID");

        LootLockerSDKManager.SubmitScore(playerId.ToString(), newScore, leaderboardID, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Score successfully uploaded to LootLocker.");
            }
            else
            {
                Debug.Log("Failed to upload score: " + response.Error);
            }
        });
    }

    void FetchLeaderboardScores()
    {
        LootLockerSDKManager.GetScoreList(leaderboardID, numberOfPreviousScoresToShow, (response) =>
        {
            if (response.statusCode == 200)
            {
                previousScoresText.text = "Leaderboard Scores:\n";
                foreach (var item in response.items)
                {
                    previousScoresText.text += $"{item.member_id}: {item.score}\n";
                }
            }
            else
            {
                Debug.Log("Failed to retrieve scores: " + response.Error);
            }
        });
    }
}
