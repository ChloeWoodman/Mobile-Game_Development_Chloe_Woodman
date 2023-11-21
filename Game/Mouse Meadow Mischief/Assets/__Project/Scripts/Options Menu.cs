using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;

public class OptionsMenu : MonoBehaviour
{
    public Slider sensitivitySlider;
    public TMP_Dropdown qualityDropdown; // Dropdown for graphics quality
    public Slider volumeSlider; // Slider for volume control

    private string sensitivityKey = "TouchSensitivity";
    private string graphicsQualityKey = "GraphicsQuality";
    private string volumeKey = "Volume";

    // Removed the TouchMovement reference

    public RenderPipelineAsset[] qualityLevels; // Array of URP quality settings

    public static OptionsMenu Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional, only if you want to persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Load sensitivity setting from PlayerPrefs
        InitializeTouchSensitivity();

        // Load graphics quality setting from PlayerPrefs
        InitializeGraphicsQuality();

        // Load volume setting from PlayerPrefs
        InitializeVolume();
    }

    private void InitializeTouchSensitivity()
    {
        sensitivitySlider.value = PlayerPrefs.GetFloat(sensitivityKey, 1.0f); // Default to 1.0f if not set
        // Removed the direct sensitivity application
    }

    private void InitializeGraphicsQuality()
    {
        qualityDropdown.value = PlayerPrefs.GetInt(graphicsQualityKey, 0); // Default to 0 (Low) if not set
        ApplyGraphicsQuality(); // Apply quality at start in case it's been changed in the PlayerPrefs
    }

    private void InitializeVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(volumeKey, 1.0f); // Default to 1.0f if not set
        ApplyVolume(); // Apply volume at start in case it's been changed in the PlayerPrefs
    }

    public void ApplySensitivity()
    {
        float sensitivity = sensitivitySlider.value;
        PlayerPrefs.SetFloat(sensitivityKey, sensitivity);
        // Apply sensitivity when the actual game starts, not here
    }
    public float GetCurrentSensitivity()
    {
        if (sensitivitySlider != null)
        {
            return sensitivitySlider.value;
        }
        return 1.0f; // Default sensitivity if not yet initialized
    }


    public void ApplyGraphicsQuality()
    {
        int qualityLevel = qualityDropdown.value;
        if (qualityLevels != null && qualityLevel < qualityLevels.Length)
        {
            GraphicsSettings.renderPipelineAsset = qualityLevels[qualityLevel];
        }
        PlayerPrefs.SetInt(graphicsQualityKey, qualityLevel);
    }

    public void ApplyVolume()
    {
        float volume = volumeSlider.value;
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat(volumeKey, volume);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene"); // Replace "GameScene" with your actual game scene name
    }
}
