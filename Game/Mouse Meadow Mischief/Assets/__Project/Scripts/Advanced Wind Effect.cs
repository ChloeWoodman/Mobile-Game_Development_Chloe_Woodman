using UnityEngine;
using Cinemachine;

public class AdvancedWindEffect : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public AnimationCurve intensityOverTime; // Create a more complex curve in the editor
    public float maxIntensity = 1.0f;
    public float noiseFrequency = 0.2f;

    private CinemachineBasicMultiChannelPerlin noise;
    private float elapsedTime = 0f;

    void Start()
    {
        if (virtualCamera != null)
        {
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        else
        {
            Debug.LogError("Virtual Camera not assigned!");
        }
    }

    void Update()
    {
        if (noise != null)
        {
            elapsedTime += Time.deltaTime;

            // Evaluate intensity based on an Animation Curve without resetting
            float currentIntensity = intensityOverTime.Evaluate(elapsedTime) * maxIntensity;

            // Apply the evaluated intensity to the noise parameters
            noise.m_AmplitudeGain = currentIntensity;
            noise.m_FrequencyGain = noiseFrequency;
        }
    }
}
