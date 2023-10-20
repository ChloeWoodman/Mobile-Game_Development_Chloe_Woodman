using UnityEngine;
using UnityEngine.Android;

public class MicrophoneManager : MonoBehaviour
{
    public bool HasMicrophonePermission => Permission.HasUserAuthorizedPermission(Permission.Microphone);

    public void RequestMicrophonePermission()
    {
        if (!HasMicrophonePermission)
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
    }

    public float GetMicrophoneInputLevel(AudioSource audioSource, float[] audioData)
    {
        audioSource.GetOutputData(audioData, 0);
        float microphoneLevel = 0;

        for (int i = 0; i < audioData.Length; i++)
        {
            microphoneLevel += Mathf.Abs(audioData[i]);
        }

        return microphoneLevel / audioData.Length;
    }
}
