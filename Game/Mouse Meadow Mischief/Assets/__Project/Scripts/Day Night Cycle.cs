using UnityEngine;
using UnityEngine.InputSystem;

public class DayNightCycle : MonoBehaviour
{
    public Material daySkyboxMaterial;
    public Material nightSkyboxMaterial;
    public Light sceneLight;
    private LightSensor lightSensor;
    private float ambientLightThreshold = 0.5f;

    void Start()
    {
        if (LightSensor.current != null)
        {
            lightSensor = LightSensor.current;
            InputSystem.EnableDevice(lightSensor);
        }
    }

    void Update()
    {
        if (lightSensor != null)
        {
            float ambientLightLevel = lightSensor.lightLevel.ReadValue();

            if (ambientLightLevel > ambientLightThreshold)
            {
                // Daytime settings
                RenderSettings.skybox = daySkyboxMaterial;
                sceneLight.intensity = 1.0f;
            }
            else
            {
                // Nighttime settings
                RenderSettings.skybox = nightSkyboxMaterial;
                sceneLight.intensity = 0.5f; // Dim the light to simulate night
            }

            DynamicGI.UpdateEnvironment();
        }
    }
}
