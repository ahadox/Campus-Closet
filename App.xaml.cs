using System.Net;
using Campuscloset.Pages;

namespace Campuscloset
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new  AppShell();
            if (Application.Current.Resources.ContainsKey("BackgroundColorLight") &&
                Application.Current.Resources.ContainsKey("TextColorLight"))
            {
                Application.Current.Resources["DynamicBackgroundColor"] = Application.Current.Resources["BackgroundColorLight"];
                Application.Current.Resources["DynamicTextColor"] = Application.Current.Resources["TextColorLight"];
            }
            else
            {
                Console.WriteLine("One or more resource keys are missing.");
            }

            foreach (var key in Application.Current.Resources.Keys)
            {
                Console.WriteLine($"Resource Key: {key}");
            }




        }
    }
}
