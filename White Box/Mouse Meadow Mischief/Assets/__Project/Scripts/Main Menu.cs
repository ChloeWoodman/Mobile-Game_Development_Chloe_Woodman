using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class StartMenu : MonoBehaviour
{
    // Function to start the game
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Function to open options (you can customize this)
    public void OpenSettings()
    {
        // Add code to handle options here
    }

    // Function to exit the game (only works in standalone builds, not in the Unity editor)
    public void ExitGame()
    {
        UnityEngine.Application.Quit(); // This function won't work in the Unity Editor
    }
}