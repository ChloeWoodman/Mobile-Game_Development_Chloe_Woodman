using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;

    void Start()
    {
        // Get the noise component on the virtual camera
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float time)
    {
        // Set the amplitude of the noise
        noise.m_AmplitudeGain = intensity;

        // Stop the shake after the duration
        Invoke(nameof(StopShake), time);
    }

    private void StopShake()
    {
        // Reset the amplitude to 0 to stop the shake
        noise.m_AmplitudeGain = 0f;
    }
}
