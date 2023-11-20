using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3; // Maximum lives of the player
    private static int currentLives; // Current lives of the player
    public Image[] heartImages; // An array of UI images representing the hearts
    public CameraShake cameraShake; // Reference to the CameraShake script

    void Start()
    {
        currentLives = maxLives; // Initialize current lives to max lives
        UpdateUI();
    }

    public void TakeDamage(int damageAmount)
    {
        currentLives -= damageAmount;
        Debug.Log("Player took damage. Remaining lives: " + currentLives);

        // Trigger camera shake with desired intensity and duration
        if (cameraShake != null)
        {
            cameraShake.ShakeCamera(1.0f, 0.5f);
        }

        if (currentLives > 0)
        {
            Debug.Log("Player lost a life. Current lives: " + currentLives);
            UpdateUI();
        }
        else
        {
            Debug.Log("Player is out of lives. Game over!");
            // Move to the game over scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i < currentLives;
        }
    }

    // Add a static method to access the currentLives value
    public static int GetCurrentLives()
    {
        return currentLives;
    }
}
