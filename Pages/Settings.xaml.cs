using System;
using Microsoft.Maui.Controls;
using Campuscloset.Services;
using Campuscloset.Models;

namespace Campuscloset.Pages
{
    public partial class Settings : ContentPage
    {
        private readonly JsonStorageService _storageService = new JsonStorageService();

        public Settings()
        {
            InitializeComponent();

            // Load saved settings
            var settings = _storageService.LoadUserSettings();

            // Display saved settings on the page
            NameEntry.Text = settings.UserName;
            EmailEntry.Text = settings.UserEmail;

            // Set the theme
            if (settings.Theme == "Dark")
            {
                Application.Current.Resources["DynamicBackgroundColor"] = Application.Current.Resources["BackgroundColorDark"];
                Application.Current.Resources["DynamicTextColor"] = Application.Current.Resources["TextColorDark"];
            }
            else
            {
                Application.Current.Resources["DynamicBackgroundColor"] = Application.Current.Resources["BackgroundColorLight"];
                Application.Current.Resources["DynamicTextColor"] = Application.Current.Resources["TextColorLight"];
            }

            // Preselect the theme RadioButtons
            if (settings.Theme == "Dark")
            {
                ((RadioButton)this.FindByName("DarkRadioButton")).IsChecked = true;
            }
            else
            {
                ((RadioButton)this.FindByName("LightRadioButton")).IsChecked = true;
            }

            // Preselect the notification RadioButtons
            if (settings.Notifications)
            {
                ((RadioButton)this.FindByName("OnRadioButton")).IsChecked = true;
            }
            else
            {
                ((RadioButton)this.FindByName("OffRadioButton")).IsChecked = true;
            }
        }

        // Event handler for Theme selection
        private void OnThemeChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value) // If this RadioButton is selected
            {
                var selectedTheme = ((RadioButton)sender).Value.ToString();

                if (selectedTheme == "Light")
                {
                    // Update to Light Theme
                    Application.Current.Resources["DynamicBackgroundColor"] = Application.Current.Resources["BackgroundColorLight"];
                    Application.Current.Resources["DynamicTextColor"] = Application.Current.Resources["TextColorLight"];
                }
                else if (selectedTheme == "Dark")
                {
                    // Update to Dark Theme
                    Application.Current.Resources["DynamicBackgroundColor"] = Application.Current.Resources["BackgroundColorDark"];
                    Application.Current.Resources["DynamicTextColor"] = Application.Current.Resources["TextColorDark"];
                }
            }
        }

        // Event handler for Notifications toggle
        private void OnNotificationChanged(object sender, CheckedChangedEventArgs e)
        {
            // Notifications toggle logic
            bool isToggled = ((RadioButton)sender).Value.ToString() == "On";
            DisplayAlert("Notification", isToggled ? "Notifications Enabled" : "Notifications Disabled", "OK");
        }

        // Event handler for Save button
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // Get input values
            var name = NameEntry.Text;
            var email = EmailEntry.Text;

            // Perform validation
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Error", "Name and Email cannot be empty.", "OK");
                return;
            }

            // Save to JSON using UserSettings
            var settings = new UserSettings
            {
                UserName = name,
                UserEmail = email,
                Theme = ((RadioButton)this.FindByName("DarkRadioButton")).IsChecked ? "Dark" : "Light",
                Notifications = ((RadioButton)this.FindByName("OnRadioButton")).IsChecked
            };

            // Use the JSON storage service to save the settings
            _storageService.SaveUserSettings(settings);

            // Provide feedback to the user
            await DisplayAlert("Success", "Settings saved successfully.", "OK");
        }

        // Event handler for Photo change
        private async void OnChangePhotoClicked(object sender, EventArgs e)
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select a photo"
            });

            if (result != null)
            {
                // Save photo path or process it further
                var filePath = result.FullPath;

                // Update ImageButton Source to display the new photo
                ProfilePhotoButton.Source = ImageSource.FromFile(filePath);

                // Save photo path if needed
                Preferences.Set("UserPhotoPath", filePath);
            }
        }
    }
}
