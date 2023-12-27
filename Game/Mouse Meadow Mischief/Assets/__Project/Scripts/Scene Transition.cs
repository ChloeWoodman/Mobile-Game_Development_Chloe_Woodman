using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneNameToLoad;

    // Call this method to load the scene specified in the sceneNameToLoad variable
    public void LoadSceneByName()
    {
        LoadScene(sceneNameToLoad);
        Time.timeScale = 1f;
    }

    // Overloaded method to load a scene by name passed as a parameter
    public void LoadSceneByName(string sceneName)
    {
        LoadScene(sceneName);
        Time.timeScale = 1f;
    }

    private void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not set or is empty.");
        }
    }
}
