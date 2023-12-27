using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro
using LootLocker.Requests;

public class Register : MonoBehaviour
{
    public TMP_InputField emailInputField; // Assign in Inspector
    public TMP_InputField passwordInputField; // Assign in Inspector
    public TextMeshProUGUI messageText; // Assign in Inspector

    // This method should be called when the user clicks the sign-up button.
    public void OnSignUpButtonClicked()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;

        LootLockerSDKManager.WhiteLabelSignUp(email, password, (response) =>
        {
            if (!response.success)
            {
                // Displaying error message using TextMeshPro
                messageText.text = "Error while creating user: " + response.Error;
                Debug.Log("Error while creating user: " + response.Error);

                return;
            }

            // Optionally, clear the message or display a success message
            messageText.text = "User created successfully";
            Debug.Log("User created successfully");
        });
    }
}
