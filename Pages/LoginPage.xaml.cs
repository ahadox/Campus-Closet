namespace Campuscloset.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void SignUpButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUpPage());
    }

    private async void LoginButton_Clicked(object sender, EventArgs e)
    {
        string enteredEmail = txtmail.Text;
        string enteredPassword = txtword.Text;
        if (ValidateCredentials(enteredEmail, enteredPassword))
        {
            // Navigate to a welcome page or home page upon successful login
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            // Show an error message for incorrect credentials
            lblLoginError.IsVisible = true;
        }
    }

    private bool ValidateCredentials(string enteredEmail, string enteredPassword)
    {
        string destinationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "credentials.txt");

        try
        {
            // Check if the file exists
            if (File.Exists(destinationPath))
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(destinationPath);

                foreach (string line in lines)
                {
                    // Each user record is stored in the following format:
                    // Name: John Doe
                    // Email: johndoe@example.com
                    // Password: hashed_password

                    // Split the lines and extract email and password
                    if (line.StartsWith("Email: "))
                    {
                        string storedEmail = line.Substring("Email: ".Length).Trim();

                        // Get the next line for the password
                        int index = Array.IndexOf(lines, line);
                        if (index + 1 < lines.Length && lines[index + 1].StartsWith("Password: "))
                        {
                            string storedPassword = lines[index + 1].Substring("Password: ".Length).Trim();

                            // Compare the email and password
                            if (storedEmail == enteredEmail && storedPassword == enteredPassword)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }

        return false;
    }
}
