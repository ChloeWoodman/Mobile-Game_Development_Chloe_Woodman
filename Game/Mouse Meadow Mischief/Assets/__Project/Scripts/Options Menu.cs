using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Slider sensitivitySlider;
    public TMP_Dropdown qualityDropdown; // Dropdown for graphics quality
    public Slider volumeSlider; // Slider for volume control

    private string sensitivityKey = "TouchSensitivity";
    private string graphicsQualityKey = "GraphicsQuality";
    private string volumeKey = "Volume";

    private TouchMovement touchMovement; // TouchMovement script reference

    private void Start()
    {
        // Load sensitivity setting from PlayerPrefs
        if (PlayerPrefs.HasKey(sensitivityKey))
        {
            float sensitivity = PlayerPrefs.GetFloat(sensitivityKey);
            sensitivitySlider.value = sensitivity;
        }

        // Load graphics quality setting from PlayerPrefs
        if (PlayerPrefs.HasKey(graphicsQualityKey))
        {
            int graphicsQuality = PlayerPrefs.GetInt(graphicsQualityKey);
            qualityDropdown.value = graphicsQuality;
            ApplyGraphicsQuality();
        }

        // Load volume setting from PlayerPrefs
        if (PlayerPrefs.HasKey(volumeKey))
        {
            float volume = PlayerPrefs.GetFloat(volumeKey);
            volumeSlider.value = volume;
            ApplyVolume();
        }

        // Find and reference the TouchMovement script
        touchMovement = FindObjectOfType<TouchMovement>();
    }

    public void ApplySensitivity()
    {
        // Get the sensitivity value from the slider
        float sensitivity = sensitivitySlider.value;

        // Apply sensitivity to the TouchMovement script
        touchMovement.SetSensitivity(sensitivity);

        // Save sensitivity to PlayerPrefs for future sessions
        PlayerPrefs.SetFloat(sensitivityKey, sensitivity);
    }

    public void ApplyGraphicsQuality()
    {
        // Get the selected graphics quality from the dropdown
        int qualityLevel = qualityDropdown.value;

        // Apply the selected graphics quality
        QualitySettings.SetQualityLevel(qualityLevel, true);

        // Save graphics quality to PlayerPrefs
        PlayerPrefs.SetInt(graphicsQualityKey, qualityLevel);
    }

    public void ApplyVolume()
    {
        // Get the volume setting from the slider
        float volume = volumeSlider.value;

        // Apply volume settings to the game
        AudioListener.volume = volume;

        // Save volume setting to PlayerPrefs
        PlayerPrefs.SetFloat(volumeKey, volume);
    }

    public void LoadGameScene()
    {
        // Add logic to load your game scene here
        // For example, if your game scene is named "GameScene":
        SceneManager.LoadScene("GameScene");
    }
}
