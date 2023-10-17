using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI element.

    private void Start()
    {
        // Retrieve the score from the Score script using the static method.
        int score = Score.GetCurrentPoints();

        // Display the score in the TextMeshProUGUI element.
        scoreText.text = "Score: " + score.ToString();
    }

    // Function to start the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // Function to start the game
    public void MainMenuReturn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
