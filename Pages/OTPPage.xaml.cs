using System.Reflection;
using System.IO;

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
        // Extract the embedded resource name
        string resourcePath = "Campuscloset.Resources.NewFolder.credentials.txt";
        string destinationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "credentials.txt");

        // Copy the embedded resource to a writable location if it doesn't already exist
        if (!File.Exists(destinationPath))
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath))
            {
                if (stream == null) throw new FileNotFoundException("Embedded resource not found.");
                using (FileStream fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                {
                    stream.CopyTo(fileStream);
                }
            }
        }

        // Append user data to the copied file
        using (StreamWriter writer = new StreamWriter(destinationPath, append: true))
        {
            writer.WriteLine($"Name: {_userName}");
            writer.WriteLine($"Email: {_userEmail}");
            writer.WriteLine($"Password: {_userPassword}");
            writer.WriteLine(); // Add a blank line
        }
    }
}
