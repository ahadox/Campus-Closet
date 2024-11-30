using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.IO;


namespace Campuscloset.Pages
{
    public partial class SignUpPage : ContentPage
    {

        public SignUpPage()
        {
            InitializeComponent();
        }
        private void AlreadyRegisteredButton(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
        private void SignUpButton(object sender, EventArgs e)
        {
            string email = txtNewEmail.Text;
            string name = txtNewName.Text;
            string password = txtPassword.Text;


            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(password))
            {
                if (IsEmailAlreadyRegistered(email))
                {
                    // Show error message if the email is already registered
                    lblError.Text = "This email is already registered. Please use a different email.";
                    lblError.IsVisible = true;
                }
                else
                {
                    if (ValidateEmail(email))
                    {
                        int otpCode = GenerateOtp();
                        EmailSender.SendVerificationEmail(email, otpCode); // Send the OTP email
                        if (EmailSender.emailsent)
                        { Navigation.PushAsync(new OTPPage(otpCode, name, email, password)); }
                        else
                        {
                            failedotp.IsVisible = true;
                        }
                    }
                    else
                    { lblError.IsVisible = true; }
                }

            }
            else
            {
                // All fields are not filled
                lblMissingInfo.IsVisible = true; // Show the error label

            }
        }
        static bool ValidateEmail(string email)
        {
            string usmDomain = "@usm.edu";
            return email.EndsWith(usmDomain, StringComparison.OrdinalIgnoreCase);
        }

        static int GenerateOtp()
        {
            Random random = new Random();
            int otp = random.Next(100000, 1000000); // Generates a random number between 100000 and 999999
            return otp;
        }

        private bool IsEmailAlreadyRegistered(string email)
        {
            string filePath = @"C:\Users\w10171527\Documents\CSC 317\Confidentials.txt";

            try
            {
                // Check if the file exists
                if (File.Exists(filePath))
                {
                    // Read all lines from the file
                    string[] lines = File.ReadAllLines(filePath);

                    foreach (string line in lines)
                    {
                        if (line.StartsWith("Email: "))
                        {
                            string storedEmail = line.Substring("Email: ".Length).Trim();

                            if (storedEmail == email)
                            {
                                return true; // Email is already registered
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking email: {ex.Message}");
            }

            return false; // Email is not registered
        }

    }
}



