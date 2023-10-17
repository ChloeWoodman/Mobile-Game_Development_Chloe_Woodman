using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;    // Maximum lives of the player
    private int currentLives;   // Current lives of the player

    public UnityEngine.UI.Image[] heartImages; // An array of UI images representing the hearts

    void Start()
    {
        currentLives = maxLives;   // Initialize current lives to max lives
        UpdateUI();
    }

    public void TakeDamage(int damageAmount)
    {
        currentLives--;
        UnityEngine.Debug.Log("Player took damage. Remaining lives: " + currentLives);

        if (currentLives >= 0)
        {
            UnityEngine.Debug.Log("Player lost a life. Current lives: " + currentLives);
            UpdateUI();
        }
        else
        {
            UnityEngine.Debug.Log("Player is out of lives. Game over!");
            // Implement game over or other end-of-game logic here
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentLives)
            {
                heartImages[i].enabled = true; // Show the hearts
            }
            else
            {
                heartImages[i].enabled = false; // Hide the remaining hearts
            }
        }
    }
}
