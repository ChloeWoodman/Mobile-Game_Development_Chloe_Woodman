using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUICanvas;

    private void Start()
    {
        
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
}
