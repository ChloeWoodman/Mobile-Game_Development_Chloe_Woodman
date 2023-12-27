using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public Canvas winCanvas;
    public Canvas currentGameplayCanvas;

    void Start()
    {
        winCanvas.gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UnityEngine.Debug.Log("You win");
            // Show the win canvas and pause the game
            winCanvas.gameObject.SetActive(true);
            currentGameplayCanvas.gameObject.SetActive(false);
            Time.timeScale = 0; // Pause the game
        }
    }
}
