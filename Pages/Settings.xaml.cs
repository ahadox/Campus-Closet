namespace Campuscloset;

public partial class Settings : ContentPage
{
	public Settings()
	{
		InitializeComponent();
	}
    private async void OnChangePhotoClicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Select a photo"
        });

        if (result != null)
        {
            // Handle the selected file (e.g., save it or display it in the app)
            // Example: Set it as the source for the ImageButton
            ((ImageButton)sender).Source = ImageSource.FromFile(result.FullPath);
        }
    }

}