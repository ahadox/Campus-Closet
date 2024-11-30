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

        private async void NavigateToDetails(Item selectedItem)
        {
            await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
    {
        { "SelectedItem", selectedItem }
    });
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

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            // Get the selected item from the BindingContext of the button
            var button = sender as Button;
            var selectedItem = button.BindingContext as Item;

            // Navigate to the details page and pass the item data
            if (selectedItem != null)
            {
                await Shell.Current.GoToAsync("//DetailsPage");
            }

        }
    }
}
