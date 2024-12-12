using System;
using System.IO;
using Microsoft.Maui.Controls;
using Campuscloset.Services;
using Campuscloset.Models;

namespace Campuscloset.Pages
{
    public partial class Settings : ContentPage
    {
        private string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "credentials.txt");

        public Settings()
        {
            InitializeComponent();
            LoadUserSettings();
        }

        private void LoadUserSettings()
        {
            try
            {
                // Check if the file exists
                if (File.Exists(filePath))
                {
                    string name = string.Empty;
                    string email = string.Empty;

                    // Read data from the file
                    string[] lines = File.ReadAllLines(filePath);
                    foreach (var line in lines)
                    {
                        if (line.StartsWith("Name: "))
                            name = line.Substring("Name: ".Length).Trim();

                        if (line.StartsWith("Email: "))
                            email = line.Substring("Email: ".Length).Trim();
                    }

                    // Prepopulate settings fields
                    NameEntry.Text = name;
                    EmailEntry.Text = email;
                }
                else
                {
                    Console.WriteLine("User data file not found. Creating a new one after saving.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading user settings: {ex.Message}");
            }
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // Get input values
            string name = NameEntry.Text;
            string email = EmailEntry.Text;

            // Perform validation
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Error", "Name and Email cannot be empty.", "OK");
                return;
            }

            // Save the updated user data to the file
            try
            {
                string[] lines = File.Exists(filePath) ? File.ReadAllLines(filePath) : new string[0];

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (string line in lines)
                    {
                        if (line.StartsWith("Name: "))
                            writer.WriteLine($"Name: {name}");
                        else if (line.StartsWith("Email: "))
                            writer.WriteLine($"Email: {email}");
                        else
                            writer.WriteLine(line); // Retain other lines (like password)
                    }

                    // If the file is new or missing the keys, add them
                    if (!lines.ToString().Contains("Name: "))
                        writer.WriteLine($"Name: {name}");
                    if (!lines.ToString().Contains("Email: "))
                        writer.WriteLine($"Email: {email}");
                }

                await DisplayAlert("Success", "Settings updated successfully.", "OK");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving user settings: {ex.Message}");
                await DisplayAlert("Error", "Failed to save settings.", "OK");
            }
        }

        private void OnThemeChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value) // If the RadioButton is selected
            {
                string selectedTheme = ((RadioButton)sender).Value.ToString();
                if (selectedTheme == "Light")
                {
                    Application.Current.Resources["DynamicBackgroundColor"] = Application.Current.Resources["BackgroundColorLight"];
                    Application.Current.Resources["DynamicTextColor"] = Application.Current.Resources["TextColorLight"];
                }
                else if (selectedTheme == "Dark")
                {
                    Application.Current.Resources["DynamicBackgroundColor"] = Application.Current.Resources["BackgroundColorDark"];
                    Application.Current.Resources["DynamicTextColor"] = Application.Current.Resources["TextColorDark"];
                }
            }
        }

        private void OnNotificationChanged(object sender, CheckedChangedEventArgs e)
        {
            bool notificationsEnabled = ((RadioButton)sender).Value.ToString() == "On";
            DisplayAlert("Notification", notificationsEnabled ? "Notifications Enabled" : "Notifications Disabled", "OK");
        }

        private async void OnChangePhotoClicked(object sender, EventArgs e)
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select a Profile Photo",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                try
                {
                    // Save photo path to user settings
                    var filePath = result.FullPath;

                    // Update UI
                    ProfilePhotoButton.Source = ImageSource.FromFile(filePath);

                    // Save photo path to the file
                    string[] lines = File.Exists(filePath) ? File.ReadAllLines(filePath) : new string[0];
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        foreach (var line in lines)
                        {
                            if (line.StartsWith("ProfilePhotoPath: "))
                                writer.WriteLine($"ProfilePhotoPath: {filePath}");
                            else
                                writer.WriteLine(line);
                        }

                        if (!lines.ToString().Contains("ProfilePhotoPath: "))
                            writer.WriteLine($"ProfilePhotoPath: {filePath}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving profile photo: {ex.Message}");
                    await DisplayAlert("Error", "Failed to save profile photo.", "OK");
                }
            }
        }
    }
}
