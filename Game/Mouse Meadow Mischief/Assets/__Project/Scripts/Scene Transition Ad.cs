using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionAd : MonoBehaviour
{
    public InterstitialAd interstitialAd; // Reference to your InterstitialAd script

    private int currentSceneIndex;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene previousScene, Scene newScene)
    {
        int newSceneIndex = newScene.buildIndex;

        if (currentSceneIndex == 1 && newSceneIndex > currentSceneIndex)
        {
            // Transition from MainScene to a higher scene (e.g., WinScene, GameOverScene)
            interstitialAd.LoadAd(); // Load the ad
            interstitialAd.ShowAd(); // Show the ad
        }

        currentSceneIndex = newSceneIndex;
    }
}
