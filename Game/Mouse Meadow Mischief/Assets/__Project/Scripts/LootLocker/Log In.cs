using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro
using LootLocker.Requests;

public class LogIn : MonoBehaviour
{
    public TMP_InputField emailInputField; // Assign in Inspector
    public TMP_InputField passwordInputField; // Assign in Inspector
    public TextMeshProUGUI messageText; // Assign in Inspector
    public bool rememberMe = true; // Set this based on your UI or preferences

    // This method should be called when the user clicks the login button.
    public void OnLoginButtonClicked()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;

        LootLockerSDKManager.WhiteLabelLogin(email, password, rememberMe, (loginResponse) =>
        {
            if (!loginResponse.success)
            {
                string errorMessage = "Error while logging in.";
                messageText.text = errorMessage;
                Debug.Log(errorMessage);
                GameManager.IsPlayerLoggedIn = false;
                return;
            }
            // Assuming the session token is correctly named and accessible
            StartGameSession(); // Adjust this line as per the actual response structure
        });
    }

    private void StartGameSession()
    {
        // Use the session token to start a game session
        LootLockerSDKManager.StartWhiteLabelSession((sessionResponse) =>
        {
            if (sessionResponse.success)
            {
                // Assuming player_id is correctly named and accessible
                int playerId = sessionResponse.player_id; // Adjust this line as per the actual response structure
                Debug.Log("Player ID: " + playerId);
                PlayerPrefs.SetInt("PlayerID", playerId);
                messageText.text = "Login and session start successful";
                GameManager.IsPlayerLoggedIn = true;
            }
            else
            {
                string errorMessage = "Error while starting game session.";
                messageText.text = errorMessage;
            }
        });
    }
}
