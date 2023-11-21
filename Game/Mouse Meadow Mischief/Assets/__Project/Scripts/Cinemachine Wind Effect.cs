using System.Collections;
using UnityEngine;
using Cinemachine;

public class CinemachineWindEffect : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float minAmplitudeGain = 0.1f;
    public float maxAmplitudeGain = 0.5f;
    public float minFrequencyGain = 0.1f;
    public float maxFrequencyGain = 0.5f;
    public float minChangeInterval = 1f;
    public float maxChangeInterval = 5f;

    private CinemachineBasicMultiChannelPerlin noise;

    void Start()
    {
        // Ensure the virtual camera is assigned
        if (virtualCamera == null)
        {
            Debug.LogError("Virtual Camera not assigned in CinemachineWindEffect script.");
            return;
        }

        // Get the noise component on the virtual camera
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        if (noise == null)
        {
            Debug.LogError("No CinemachineBasicMultiChannelPerlin component found on virtual camera.");
            return;
        }

        StartCoroutine(WindEffectCoroutine());
    }

    IEnumerator WindEffectCoroutine()
    {
        while (true)
        {
            // Randomly adjust the amplitude and frequency for the wind effect
            float amplitude = Random.Range(minAmplitudeGain, maxAmplitudeGain);
            float frequency = Random.Range(minFrequencyGain, maxFrequencyGain);
            noise.m_AmplitudeGain = amplitude;
            noise.m_FrequencyGain = frequency;

            // Wait for a random interval before changing the wind simulation again
            float waitTime = Random.Range(minChangeInterval, maxChangeInterval);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
