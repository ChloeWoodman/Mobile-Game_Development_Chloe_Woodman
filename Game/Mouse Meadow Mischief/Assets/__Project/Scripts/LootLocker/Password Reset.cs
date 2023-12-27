using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro
using LootLocker.Requests;

public class PasswordReset : MonoBehaviour
{
    public TMP_InputField emailInputField; // Assign in Inspector
    public TextMeshProUGUI messageText; // Assign in Inspector

    // This method should be called when the user clicks the reset password button.
    public void OnResetPasswordButtonClicked()
    {
        string email = emailInputField.text;

        LootLockerSDKManager.WhiteLabelRequestPassword(email, (response) =>
        {
            if (!response.success)
            {
                // Displaying error message using TextMeshPro
                messageText.text = "Error requesting password reset";
                Debug.Log("Error requesting password reset");

                return;
            }

            // Optionally, clear the message or display a success message
            messageText.text = "Requested password reset successfully";
            Debug.Log("Requested password reset successfully");
        });
    }
}
