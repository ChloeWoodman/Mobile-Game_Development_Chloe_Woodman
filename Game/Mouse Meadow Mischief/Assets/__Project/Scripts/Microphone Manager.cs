using UnityEngine;
using UnityEngine.Android;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneManager : MonoBehaviour
{
    public int sampleRate = 44100;

    private AudioSource audioSource;
    private float[] audioData; // Array to store audio data

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioData = new float[256]; // You can adjust the array size as needed

        RequestMicrophonePermission(); // Request permission

        string selectedMicrophone = GetSelectedMicrophone();
        if (!string.IsNullOrEmpty(selectedMicrophone))
        {
            Debug.Log("Selected Microphone: " + selectedMicrophone);

            audioSource.clip = Microphone.Start(selectedMicrophone, true, 10, sampleRate);
            audioSource.loop = true;
            audioSource.mute = true;

            while (Microphone.GetPosition(selectedMicrophone) <= 0) { }
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No microphones are available on this device.");
        }
    }

    private string GetSelectedMicrophone()
    {
        string[] availableDevices = Microphone.devices;
        if (availableDevices.Length > 0)
        {
            // Select the first available microphone
            return availableDevices[0];
        }
        return null;
    }

    public bool HasMicrophonePermission => Permission.HasUserAuthorizedPermission(Permission.Microphone);

    public void RequestMicrophonePermission()
    {
        if (!HasMicrophonePermission)
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
    }

    public float GetMicrophoneInputLevel()
    {
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing.");
            return 0;
        }

        audioSource.GetOutputData(audioData, 0);
        float microphoneLevel = 0;

        for (int i = 0; i < audioData.Length; i++)
        {
            microphoneLevel += Mathf.Abs(audioData[i]);
        }

        return microphoneLevel / audioData.Length;
    }
}
