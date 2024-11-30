using System.Net;
using Campuscloset.Pages;
using Campuscloset.Services;
using Campuscloset.Models;

namespace Campuscloset
{
    public partial class App : Application
    {
        private readonly JsonStorageService _storageService;

        public App()
        {
            InitializeComponent();

            // Initialize the storage service
            _storageService = new JsonStorageService();

            // Load settings when the app starts
            var settings = _storageService.LoadUserSettings();

            // Apply the theme based on saved settings
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

            // Set the main page
            MainPage = new AppShell();

            // Debug: Check all loaded resource keys (optional)
            foreach (var key in Application.Current.Resources.Keys)
            {
                Console.WriteLine($"Resource Key: {key}");
            }
        }
    }
}
