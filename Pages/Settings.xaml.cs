using System;
using Microsoft.Maui.Controls;

namespace Campuscloset
{
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        // Event handler for Theme selection
        private void OnThemeChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value) // If this RadioButton is selected
            {
                var selectedTheme = ((RadioButton)sender).Content.ToString();

                if (selectedTheme == "LIGHT")
                {
                    Application.Current.Resources["DynamicBackgroundColor"] = Application.Current.Resources["BackgroundColorLight"];
                    Application.Current.Resources["DynamicTextColor"] = Application.Current.Resources["TextColorLight"];
                }
                else if (selectedTheme == "DARK")
                {
                    Application.Current.Resources["DynamicBackgroundColor"] = Application.Current.Resources["BackgroundColorDark"];
                    Application.Current.Resources["DynamicTextColor"] = Application.Current.Resources["TextColorDark"];
                }
            }
        }



        // Event handler for Notification toggle
        private void OnNotificationChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value) // If this RadioButton is selected
            {
                var selectedNotification = ((RadioButton)sender).Content.ToString();
                if (selectedNotification == "ON")
                {
                    // Enable notifications (logic here depends on your app's notification system)
                    DisplayAlert("Notification", "Notifications Enabled", "OK");
                }
                else if (selectedNotification == "OFF")
                {
                    // Disable notifications
                    DisplayAlert("Notification", "Notifications Disabled", "OK");
                }
            }
        }

        // Event handler for Save button
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // Get input values
            var name = NameEntry.Text;
            var email = EmailEntry.Text;

            // Perform validation if needed
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                await DisplayAlert("Error", "Name and Email cannot be empty.", "OK");
                return;
            }

            // Save the name and email
            SaveSettings(name, email);

            await DisplayAlert("Success", "Settings saved successfully.", "OK");
        }

        private void SaveSettings(string name, string email)
        {
            // Example: Save to local preferences or database
            Preferences.Set("UserName", name);
            Preferences.Set("UserEmail", email);
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
                // Example: Save photo path or process it further
                var filePath = result.FullPath;

                // Update ImageButton Source to display the new photo
                ProfilePhotoButton.Source = ImageSource.FromFile(filePath);

                // Save photo path if needed
                Preferences.Set("UserPhotoPath", filePath);
            }
        }
    }
}
