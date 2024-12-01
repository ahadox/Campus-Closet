using Campuscloset.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Campuscloset.Pages
{
    public partial class ListingsPage : ContentPage
    {
        private const int ItemsPerRow = 2; // Number of items per row in the grid

        public ListingsPage()
        {
            InitializeComponent();
            Task.Run(async () => await LoadItemsAsync());
        }

        private async Task LoadItemsAsync()
        {
            // Fetch items from the database
            List<Item> items = await App.Database.GetItemsAsync();

            // Clear existing content in the grid
            ItemsGrid.Children.Clear();
            ItemsGrid.ColumnDefinitions.Clear();
            ItemsGrid.RowDefinitions.Clear();

            // Define the number of columns
            for (int i = 0; i < ItemsPerRow; i++)
            {
                ItemsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            }

            // Populate grid with items
            int rowIndex = 0;
            int columnIndex = 0;

            foreach (var item in items)
            {
                // Create a card for each item
                var itemCard = CreateItemCard(item);

                // Add the item to the grid
                ItemsGrid.Children.Add(itemCard);
                Grid.SetColumn(itemCard, columnIndex);
                Grid.SetRow(itemCard, rowIndex);

                // Update column and row indices
                columnIndex++;
                if (columnIndex >= ItemsPerRow)
                {
                    columnIndex = 0;
                    rowIndex++;
                }
            }

            // Add row definitions for each row
            for (int i = 0; i <= rowIndex; i++)
            {
                ItemsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }
        }

        private View CreateItemCard(Item item)
        {
            // Create an image for the item
            var itemImage = new Image
            {
                Source = string.IsNullOrWhiteSpace(item.ImagePath) ? "placeholder.png" : item.ImagePath,
                HeightRequest = 200,
                WidthRequest = 200,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center
            };

            // Create a label for the item name
            var nameLabel = new Label
            {
                Text = item.Name,
                FontAttributes = FontAttributes.Bold,
                FontSize = 16,
                TextColor = Colors.Black,
                HorizontalTextAlignment = TextAlignment.Center
            };

            // Create a label for the item price
            var priceLabel = new Label
            {
                Text = $"Price: ${item.Price}",
                FontSize = 14,
                TextColor = Colors.Black,
                HorizontalTextAlignment = TextAlignment.Center
            };

            // Create the "Mark as Sold" button
            var markAsSoldButton = new Button
            {
                Text = "Mark as Sold",
                BackgroundColor = Colors.Red,
                TextColor = Colors.White,
                CornerRadius = 5,
                HeightRequest = 40,
                FontSize = 12,
                HorizontalOptions = LayoutOptions.Center
            };

            // Set the button click event handler
            markAsSoldButton.Clicked += async (sender, e) => await OnMarkAsSoldClicked(item);

            // Arrange the image, labels, and button in a vertical stack
            var cardLayout = new StackLayout
            {
                Spacing = 5,
                Padding = new Thickness(5),
                Children = { itemImage, nameLabel, priceLabel, markAsSoldButton }
            };

            // Wrap the layout in a frame to create a card effect
            return new Frame
            {
                CornerRadius = 8,
                HasShadow = true,
                Padding = new Thickness(5),
                Content = cardLayout,
                BackgroundColor = Colors.Beige
            };
        }

        private async Task OnMarkAsSoldClicked(Item item)
        {
            // Remove the item from the database
            await App.Database.DeleteItemAsync(item);

            // Reload the items after the item is deleted
            await LoadItemsAsync();

            // Display a confirmation message
            await DisplayAlert("Success", "Item marked as sold and removed from your listings.", "OK");
        }
    }
}
