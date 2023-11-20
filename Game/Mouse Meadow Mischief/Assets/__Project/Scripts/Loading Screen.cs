using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressBar;
    public TextMeshProUGUI progressText;

    public string sceneToLoad; // Add this line

    private void Start()
    {
        LoadScene(sceneToLoad); // Updated to use the sceneToLoad variable
    }

    // Call this method to load a scene
    public void LoadScene(string MainScene)
    {
        Debug.Log("LoadScene called for: " + MainScene);
        StartCoroutine(LoadAsynchronously(MainScene));
    }

    IEnumerator LoadAsynchronously(string MainScene)
    {
        Debug.Log("Starting to load scene: " + MainScene);
        AsyncOperation operation = SceneManager.LoadSceneAsync(MainScene);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            progressText.text = (int)(progress * 100f) + "%";

            yield return null;
        }
    }
}
