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


        }
    }
}
