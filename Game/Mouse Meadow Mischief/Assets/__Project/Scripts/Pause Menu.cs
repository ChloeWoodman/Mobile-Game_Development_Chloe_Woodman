using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUICanvas;

    private void Start()
    {
        // Make sure the pause menu is not visible at the start.
        //pauseMenuUICanvas.SetActive(false);
    }

    public void ResumeGame()
    {
        pauseMenuUICanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        pauseMenuUICanvas.SetActive(true);
        Time.timeScale = 0f; // This freezes the game.
    }

    // Function to exit the game (this will return to the main menu)
    public void ExitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
