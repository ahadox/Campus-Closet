using System.Net;
using Campuscloset.Pages;
using Campuscloset.Services;
using Campuscloset.Models;
using System.IO;


namespace Campuscloset
{
    public partial class App : Application
    {
        private readonly JsonStorageService _storageService;
        public static DatabaseService Database { get; private set; }
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
                if (Application.Current.Resources.ContainsKey("BackgroundColorDark"))
                {
                    Application.Current.Resources["DynamicBackgroundColor"] = Application.Current.Resources["BackgroundColorDark"];
                }
                else
                {
                    Console.WriteLine("BackgroundColorDark is missing.");
                }

                if (Application.Current.Resources.ContainsKey("TextColorDark"))
                {
                    Application.Current.Resources["DynamicTextColor"] = Application.Current.Resources["TextColorDark"];
                }
                else
                {
                    Console.WriteLine("TextColorDark is missing.");
                }
            }
            else
            {
                if (Application.Current.Resources.ContainsKey("BackgroundColorLight"))
                {
                    Application.Current.Resources["DynamicBackgroundColor"] = Application.Current.Resources["BackgroundColorLight"];
                }
                else
                {
                    Console.WriteLine("BackgroundColorLight is missing.");
                }

                if (Application.Current.Resources.ContainsKey("TextColorLight"))
                {
                    Application.Current.Resources["DynamicTextColor"] = Application.Current.Resources["TextColorLight"];
                }
                else
                {
                    Console.WriteLine("TextColorLight is missing.");
                }
            }


            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "CampusCloset.db3");
            Database = new DatabaseService(dbPath);

         
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
