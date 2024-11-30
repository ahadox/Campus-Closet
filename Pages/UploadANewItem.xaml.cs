using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.Windows.Input;
using Campuscloset.Models;

namespace Campuscloset.Pages
{
    public partial class UploadANewItem : ContentPage
    {
        private string itemName;
        private string price;
        private string email;
        private string itemCondition;
        private string description;
        private ImageSource itemImage;
        private bool isUploadFormVisible = false;
        private bool isUploadButtonVisible = true;

        public string ItemName
        {
            get => itemName;
            set
            {
                itemName = value;
                OnPropertyChanged(nameof(ItemName));
            }
        }

        public string Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string ItemCondition
        {
            get => itemCondition;
            set
            {
                itemCondition = value;
                OnPropertyChanged(nameof(ItemCondition));
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public ImageSource ItemImage
        {
            get => itemImage;
            set
            {
                itemImage = value;
                OnPropertyChanged(nameof(ItemImage));
                IsUploadButtonVisible = itemImage == null; // Hide button if image is set
            }
        }

        public bool IsUploadFormVisible
        {
            get => isUploadFormVisible;
            set
            {
                isUploadFormVisible = value;
                OnPropertyChanged(nameof(IsUploadFormVisible));
            }
        }

        public bool IsUploadButtonVisible
        {
            get => isUploadButtonVisible;
            set
            {
                isUploadButtonVisible = value;
                OnPropertyChanged(nameof(IsUploadButtonVisible));
            }
        }

        public ICommand AddImageCommand => new Command(OnAddImage);
        public ICommand ConfirmUploadCommand => new Command(OnConfirmUpload);

        public ICommand NavigateToUploadPageCommand => new Command(() => IsUploadFormVisible = true);

        public UploadANewItem()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void OnAddImage()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select a photo",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
            {
                ItemImage = ImageSource.FromFile(result.FullPath);
            }
        }

        private async void OnConfirmUpload()
        {

            // Debug output to check the state of all fields
            System.Diagnostics.Debug.WriteLine($"ItemName: {ItemName}");
            System.Diagnostics.Debug.WriteLine($"Price: {Price}");
            System.Diagnostics.Debug.WriteLine($"Email: {Email}");
            System.Diagnostics.Debug.WriteLine($"ItemCondition: {ItemCondition}");
            System.Diagnostics.Debug.WriteLine($"Description: {Description}");
            System.Diagnostics.Debug.WriteLine($"ItemImage: {ItemImage}");

            // Validate all fields, including the image
            if (string.IsNullOrWhiteSpace(ItemName))
            {
                await DisplayAlert("Error", "Item Name is required.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Price))
            {
                await DisplayAlert("Error", "Price is required.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                await DisplayAlert("Error", "Email is required.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(ItemCondition))
            {
                await DisplayAlert("Error", "Item Condition is required.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Description))
            {
                await DisplayAlert("Error", "Description is required.", "OK");
                return;
            }

            if (ItemImage == null)
            {
                await DisplayAlert("Error", "Please upload an image.", "OK");
                return;
            }


            // Proceed with creating the new item and saving it
            var newItem = new Item
            {
                Name = ItemName,
                Price = Price,
                Email = Email,
                Condition = ItemCondition,
                Description = Description,
                ImagePath = ((FileImageSource)ItemImage)?.File
            };

            await App.Database.AddItemAsync(newItem); // Save to database

            // Immediately query the database to verify
            var items = await App.Database.GetItemsAsync();
            foreach (var item in items)
            {
                System.Diagnostics.Debug.WriteLine($"Item in Database - Name: {item.Name}, Price: {item.Price}");
            }

            await DisplayAlert("Success", "Item uploaded successfully.", "OK");

            // Reset fields after upload
            ItemName = string.Empty;
            Price = string.Empty;
            Email = string.Empty;
            ItemCondition = string.Empty;
            Description = string.Empty;
            ItemImage = null;
            IsUploadFormVisible = false;
        }



        private void OnConditionSelected(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                var radioButton = sender as RadioButton;
                ItemCondition = radioButton?.Content.ToString();
            }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }
    }
}