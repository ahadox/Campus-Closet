using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Input;

namespace Campuscloset
{
    public partial class UploadANewItem : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
            if (string.IsNullOrEmpty(ItemName) || string.IsNullOrEmpty(Price) || string.IsNullOrEmpty(Email) ||
                string.IsNullOrEmpty(ItemCondition) || string.IsNullOrEmpty(Description) || ItemImage == null)
            {
                await DisplayAlert("Error", "Please fill in all fields, including an image.", "OK");
                return;
            }

            await DisplayAlert("Success", "Item uploaded successfully.", "OK");

            // Reset fields after upload
            ItemName = string.Empty;
            Price = string.Empty;
            Email = string.Empty;
            ItemCondition = string.Empty;
            Description = string.Empty;
            ItemImage = null;
            IsUploadFormVisible = false; // Hide the form after upload
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
