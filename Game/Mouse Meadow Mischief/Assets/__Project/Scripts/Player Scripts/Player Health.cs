using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private static int currentLives; 
    public Image[] heartImages; // An array of UI images representing the hearts
    public Canvas gameOverCanvas; 
    public Canvas currentGameplayCanvas; 

    void Start()
    {
        currentLives = maxLives;
        UpdateUI();
        gameOverCanvas.gameObject.SetActive(false);
    }

    public void TakeDamage(int damageAmount)
    {
        currentLives -= damageAmount;
        Debug.Log("Player took damage. Remaining lives: " + currentLives);

        if (currentLives > 0)
        {
            Debug.Log("Player lost a life. Current lives: " + currentLives);
            UpdateUI();
        }
        else
        {
            Debug.Log("Player is out of lives. Game over!");
            // Show the game over canvas and pause the game
            gameOverCanvas.gameObject.SetActive(true);
            currentGameplayCanvas.gameObject.SetActive(false);
            Time.timeScale = 0; // Pause the game
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
