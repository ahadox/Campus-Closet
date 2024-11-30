

namespace Campuscloset.Pages;

public partial class OTPPage : ContentPage
{
    private int _otpCode; // Stores the generated OTP code
    private string _userName;
    private string _userEmail;
    private string _userPassword;

    public OTPPage(int otpCode, string name, string email, string password)
    {
        InitializeComponent();
        _otpCode = otpCode; // Assign the OTP code received from the previous page
        _userName = name;
        _userEmail = email;
        _userPassword = password;
    }

    private void VerifyOtpButton(object sender, EventArgs e)
    {
        // Get the OTP entered by the user
        string enteredOtp = txtOtpCode.Text;

        // Validate the entered OTP
        if (int.TryParse(enteredOtp, out int userOtp) && userOtp == _otpCode)
        {
            SaveUserData();
            Navigation.PushAsync(new LoginPage());
        }
        else
        {
            // Show error message if OTP is incorrect
            lblOtpError.IsVisible = true;
        }

    }
    private void SaveUserData()
    {
        // Creates a file path
        string filePath = @"C:\Users\w10171527\Documents\CSC 317\Confidentials.txt";
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            // Write data
            writer.WriteLine($"Name: {_userName}");
            writer.WriteLine($"Email: {_userEmail}");
            writer.WriteLine($"Password: {_userPassword}");

            // Add a blank line
            writer.WriteLine();
        }

    }

}