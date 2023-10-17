using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int startingPoints = 10000;
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

            // Stop the point decrease logic.
            CancelInvoke("DecreasePointsOverTime");
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
