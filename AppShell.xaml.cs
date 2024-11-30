namespace Campuscloset;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        
    }

    private async void OnHomeTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void OnSettingsTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//Settings");
    }

    private async void OnListingsTapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//ListingsPage");
    }
}
