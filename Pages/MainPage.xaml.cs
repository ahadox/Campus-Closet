using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Campuscloset.Models;

namespace Campuscloset.Pages
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Item> allItems;

        public ObservableCollection<Item> AllItems
        {
            get => allItems;
            set
            {
                allItems = value;
                OnPropertyChanged(nameof(AllItems));
            }
        }

        public Command NavigateToUploadCommand { get; }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            // Get the bound item for the clicked button
            var button = sender as Button;
            var item = button.BindingContext as Item;

            if (item != null)
            {
                // Navigate to the DetailsPage with the selected item
                await Navigation.PushAsync(new DetailsPage(item));
            }
        }
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

           


            // Load items on page initialization
            LoadAllItems();
        }

        private async void LoadAllItems()
        {
            // Fetch all items from the database
            var items = await App.Database.GetItemsAsync();

            // Convert items to ObservableCollection for UI binding
            AllItems = new ObservableCollection<Item>(items);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//UploadANewItem");
        }

     
    }
}
